using System;
using System.Windows.Forms;
using BrightIdeasSoftware;
using XDBookmarkTools.Controllers;
using XDBookmarkTools.Model;

namespace XDBookmarkTools.Views
{
    public partial class DuplicatesView : Form
    {
        private readonly string _dbfilename;
        public bool AppIsClosing = false;

        public delegate void PathSelectionEventHandler(DuplicateItem di);

        public event PathSelectionEventHandler PathSelectionEvent;

        public DuplicatesView(string dbfilename)
        {
            _dbfilename = dbfilename;
            InitializeComponent();

            mainTreeView.CanExpandGetter = x => ((DuplicateItem) x).IsRoot;
            mainTreeView.ChildrenGetter =
                x => (new DbController(_dbfilename)).GetDuplicatesChildren((DuplicateItem) x);
            dupeNrColMTV.AspectToStringConverter = x => (long) x > 0 ? x.ToString() : "";

            //TODO: expand tooltips to contain more data
            mainTreeView.CellToolTipShowing += delegate(object x, ToolTipShowingEventArgs e)
            {
                var di = (DuplicateItem) e.Model;
                const string timeFormat = "dd/MM/yyyy HH:mm:ss";
                e.Title = "INFO";
                e.Text = "Title:\t" + di.Title + "\r\n"
                         + "Path:\t" + di.PathString + "\r\n"
                         + (di.DateAdded != 0 ? "Date Created:\t" + di.DateAddedDt.ToString(timeFormat) + "\r\n" : "")
                         + (di.LastModified != 0
                             ? "Last Modified:\t" + di.LastModifiedDt.ToString(timeFormat) + "\r\n"
                             : "");
            };

            mainTreeView.Roots = (new DbController(_dbfilename)).GetDuplicatesRoots();
        }

        private void DuplicatesView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AppIsClosing) return;
            Visible = false;
            e.Cancel = true;
        }

        private void mainTreeView_HyperlinkClicked(object sender, HyperlinkClickedEventArgs e)
        {
            var di = ((DuplicateItem) e.Item.RowObject);
            if (di.IsRoot) return;
            e.Handled = true;
            OnPathSelectionEvent(di);
        }

        /// <summary>
        /// Invokes the pathSelectionEvent
        /// </summary>
        /// <param name="di">The selected item.</param>
        protected virtual void OnPathSelectionEvent(DuplicateItem di)
        {
            if (PathSelectionEvent != null)
            {
                PathSelectionEvent(di);
            }
        }

        private void mainTreeView_ItemActivate(object sender, EventArgs e)
        {
            var col = mainTreeView.SelectedIndices;
            if (col.Count <= 0) return;
            var di = (DuplicateItem) ((OLVListItem) mainTreeView.Items[col[0]]).RowObject;
            if (di.IsRoot)
            {
                mainTreeView.Expand(di);
            }
            else
            {
                OnPathSelectionEvent(di);
            }
        }
    }
}