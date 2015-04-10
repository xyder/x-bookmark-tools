using System.Windows.Forms;
using XDBookmarkTools.Controllers;
using XDBookmarkTools.Model;

namespace XDBookmarkTools.Views
{
    public partial class EditPanel : UserControl
    {
        public DbController Dbc;

        public string InfoText
        {
            set { infoLabel.Text = value; }
        }

        private BookmarkItem _selectedBi;

        public BookmarkItem SelectedBookmark
        {
            get { return _selectedBi; }
            set
            {
                _selectedBi = value;
                if (_selectedBi == null)
                {
                    return;
                }
                titleTextBox.Text = _selectedBi.Title;
                LocationTextBox.Text = _selectedBi.Location;
                descriptionTextBox.Text = _selectedBi.Description;

                if (_selectedBi.IsDirectory)
                {
                    locationLabel.Visible = false;
                    LocationTextBox.Visible = false;
                    infoLabel.Visible = true;

                    var cntRec = Dbc.CountChildrenRecursive(_selectedBi.Id);
                    var cntNRec = Dbc.CountChildren(_selectedBi.Id);
                    infoLabel.Text = string.Format(
                        "Items, in this folder: {0} in total {1}{2}{3}\n"
                        + "Items, including all subfolders: {4} in total {5}{6}{7}",
                        cntNRec["nr.total"],
                        (cntNRec.ContainsKey("nr.bookmarks") ? " [" + cntNRec["nr.bookmarks"] + " bookmarks] " : ""),
                        (cntNRec.ContainsKey("nr.folders") ? " [" + cntNRec["nr.folders"] + " folders] " : ""),
                        (cntNRec.ContainsKey("nr.separators") ? " [" + cntNRec["nr.separators"] + " separators] " : ""),
                        cntRec["nr.total"],
                        (cntRec.ContainsKey("nr.bookmarks") ? " [" + cntRec["nr.bookmarks"] + " bookmarks] " : ""),
                        (cntRec.ContainsKey("nr.folders") ? " [" + cntRec["nr.folders"] + " folders] " : ""),
                        (cntRec.ContainsKey("nr.separators") ? " [" + cntRec["nr.separators"] + " separators] " : ""));
                }
                else
                {
                    locationLabel.Visible = true;
                    LocationTextBox.Visible = true;
                    infoLabel.Visible = false;
                }
            }
        }

        public EditPanel(DbController dbc)
        {
            InitializeComponent();

            Dbc = dbc;
        }
    }
}