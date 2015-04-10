using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using XDBookmarkTools.Controllers;
using XDBookmarkTools.Model;

namespace XDBookmarkTools.Views
{
    public partial class MainView : Form
    {
        private readonly Logger _logger;
        private string _dbfilename;
        private readonly DbController _dbc;
        private readonly DuplicatesView _duplicatesView;
        private ContextMenuStrip _cmenu;
        private Dictionary<string, ToolStripItem> _cmenuItems;
        private readonly EditPanel _mainEditPanel;
        private readonly InfoPanel _mainInfoPanel;
        private IList _backupSelectedListContentView;
        private Object _backupSelectedObjectTreeView;
        private bool _ignoreContentSelect, _ignoreTreeSelect;
        private long _idToSelectInContentView;

        /// <summary>
        /// Constructor for a MainView form object.
        /// </summary>
        /// <param name="logger">Message handler for all messages.</param>
        public MainView(Logger logger)
        {
            //TODO: split this into bits and see for code duplicate with duplicateview
            InitializeComponent();

            //initialize logger
            _logger = logger;
            _logger.MessageHandler += LogEventHandler;

            //OVERRIDE
            _dbfilename = "..\\places.sqlite";
            //OVERRIDE

            _duplicatesView = new DuplicatesView(_dbfilename);
            _duplicatesView.PathSelectionEvent += DuplicatesView_pathSelectionEvent;

            _dbc = new DbController(_dbfilename);
            _dbc.RefreshFoldersList();

            _mainEditPanel = new EditPanel(_dbc);
            _mainInfoPanel = new InfoPanel(_dbc);
            Controls.Add(_mainEditPanel);
            Controls.Add(_mainInfoPanel);
            _mainEditPanel.Parent = editContainerPanel;
            _mainEditPanel.Dock = DockStyle.Fill;
            _mainInfoPanel.Parent = editContainerPanel;
            _mainInfoPanel.Dock = DockStyle.Fill;

            //set up delegates for mainTreeView
            mainTreeView.CanExpandGetter = x => _dbc.HasChildrenBookmarks(((BookmarkItem) x).Id);
            mainTreeView.ChildrenGetter = x => _dbc.GetBookmarksChildren(((BookmarkItem) x).Id, 2);

            //cell tooltip initialization delegate
            Action<ToolTipControl> initCellToolTip = delegate(ToolTipControl control)
            {
                control.IsBalloon = true;
                control.SetMaxWidth(400);
                control.StandardIcon = ToolTipControl.StandardIcons.InfoLarge;
                control.BackColor = Color.AliceBlue;
                control.ForeColor = Color.IndianRed;
                control.AutoPopDelay = 15000;
                control.InitialDelay = 750;
                control.ReshowDelay = 750;
                control.Font = new Font("Tahoma", 10.0f);
            };

            initCellToolTip(mainContentsView.CellToolTip);
            initCellToolTip(mainTreeView.CellToolTip);
            initCellToolTip(_duplicatesView.mainTreeView.CellToolTip);

            EventHandler<ToolTipShowingEventArgs> tooltipDelegate = delegate(object x, ToolTipShowingEventArgs e)
            {
                var bi = (BookmarkItem) e.Model;
                if (bi.IsSeparator) return;
                e.Title = "INFO";
                const string dateFormat = "dd/MM/yyyy HH:mm:ss";
                e.Text = "ID:\t\t" + bi.Id + "\r\n"
                         + (bi.Title != "" ? "Title:\t\t" + bi.Title + "\r\n" : "")
                         + (bi.Location != "" ? "Location:\t" + bi.Location + "\r\n" : "")
                         + (bi.Description != "" ? "Description:\t" + bi.Description + "\r\n" : "")
                         + (bi.DateAdded != 0 ? "Date Created:\t" + bi.DateAddedDt.ToString(dateFormat) + "\r\n" : "")
                         + (bi.LastModified != 0
                             ? "Last Modified:\t" + bi.LastModifiedDt.ToString(dateFormat) + "\r\n"
                             : "");
            };
            mainTreeView.CellToolTipShowing += tooltipDelegate;
            mainContentsView.CellToolTipShowing += tooltipDelegate;

            ImageGetterDelegate dg = delegate(object x)
            {
                if (!((BookmarkItem) x).IsSeparator)
                {
                    return ((BookmarkItem) x).IsDirectory ? 0 : 1; //0 - folder; 1 - bookmark
                }
                return -1;
            };

            titleColMCV.ImageGetter = dg;
            titleColMTV.ImageGetter = dg;

            //TODO: OPT - find a way to display a separator graphically
            const string separator = "----------";
            titleColMCV.AspectGetter = x => ((BookmarkItem) x).Type != 3 ? ((BookmarkItem) x).Title : separator;
            locationColMCV.AspectGetter = x => ((BookmarkItem) x).Type != 3 ? ((BookmarkItem) x).Location : separator;

            InitContextMenuStrip();


            //OVERRIDE
            mainTreeView.Roots = _dbc.GetBookmarksRoots();
            if (mainTreeView.Items.Count > 0)
            {
                mainTreeView.SelectedIndex = 0;
            }
            _logger.LogInfo("Database connected.");
            UpdateInfoLabel();
            //OVERRIDE
        }

        private void InitContextMenuStrip()
        {
            _cmenuItems = new Dictionary<string, ToolStripItem>();
            _cmenu = new ContextMenuStrip();

            //TODO: implement this
            EventHandler delegateNotImplemented = delegate
            {
                _logger.LogError("The triggered feature is not implemented yet.");
                MessageBox.Show(@"Feature not implemented yet.", @"Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            _cmenuItems["OpenAll"] = new ToolStripMenuItem("Open All in Browser", null, delegateNotImplemented);
            _cmenuItems["Open"] = new ToolStripMenuItem("Open in Browser", null, delegateNotImplemented);
            _cmenuItems["Sep.OpenNew"] = new ToolStripSeparator();
            _cmenuItems["New"] = new ToolStripMenuItem("New");
            _cmenuItems["Sep.NewCut"] = new ToolStripSeparator();
            _cmenuItems["Cut"] = new ToolStripMenuItem("Cut", null, delegateNotImplemented);
            _cmenuItems["Copy"] = new ToolStripMenuItem("Copy", null, delegateNotImplemented);
            _cmenuItems["Paste"] = new ToolStripMenuItem("Paste", null, delegateNotImplemented);
            _cmenuItems["Sep.PasteDelete"] = new ToolStripSeparator();
            _cmenuItems["Delete"] = new ToolStripMenuItem("Delete", null, delegateNotImplemented);
            _cmenuItems["Sep.DelSort"] = new ToolStripSeparator();
            _cmenuItems["SortName"] = new ToolStripMenuItem("Sort By Name", null, delegateNotImplemented);

            foreach (var pair in _cmenuItems)
            {
                _cmenu.Items.Add(pair.Value);
            }

            _cmenuItems["New.Bookmark"] = new ToolStripMenuItem("Bookmark", null, delegateNotImplemented);
            _cmenuItems["New.Folder"] = new ToolStripMenuItem("Folder", null, delegateNotImplemented);
            _cmenuItems["New.Separator"] = new ToolStripMenuItem("Separator", null, delegateNotImplemented);

            ((ToolStripMenuItem) _cmenuItems["New"]).DropDownItems.Add(_cmenuItems["New.Bookmark"]);
            ((ToolStripMenuItem) _cmenuItems["New"]).DropDownItems.Add(_cmenuItems["New.Folder"]);
            ((ToolStripMenuItem) _cmenuItems["New"]).DropDownItems.Add(_cmenuItems["New.Separator"]);
        }

        private void UpdateInfoLabel()
        {
            var bkmCountDict = _dbc.CountBookmarks();
            var dupesNr = _dbc.CountDuplicates();
            //TODO: replace this with a disabled textfield for basic formatting
            infoLabel.Text =
                string.Format("No. of Bookmarks:\t\t{0}\r\n"
                              + "No. of Duplicates:\t\t{1}\r\n"
                              + "No. of Uniques:\t\t{2}\r\n"
                              + "No. of Folders:\t\t{3}\r\n"
                              + "No. of Separators:\t\t{4}",
                    bkmCountDict["nr.bookmarks"],
                    dupesNr,
                    (bkmCountDict["nr.bookmarks"] - dupesNr),
                    bkmCountDict["nr.folders"],
                    bkmCountDict["nr.separators"]);
        }

        /// <summary>
        /// Path Selection Event Handler for DuplicatesView
        /// </summary>
        /// <param name="di">Duplicate item to be selected in the main views.</param>
        private void DuplicatesView_pathSelectionEvent(DuplicateItem di)
        {
            SelectContentItem(di.PathNodes, di.Id);
        }

        private void SelectContentItem(IList nodes, long id = -1)
        {
            Activate();
            if (nodes == null) return;
            var leaf = ExpandPath(mainTreeView.Objects, nodes);
            mainTreeView.SelectObject(leaf);
            mainTreeView.EnsureModelVisible(leaf);
            _idToSelectInContentView = id == -1 ? -1 : id;
        }

        private BookmarkItem ExpandPath(IEnumerable branch, IList path, int pathIndex = 0)
        {
            BookmarkItem foundbi = null;
            foreach (var bi in branch.Cast<BookmarkItem>().Where(bi => bi.Id == ((BookmarkItem) path[pathIndex]).Id))
            {
                mainTreeView.Expand(bi);
                if (pathIndex + 1 < path.Count)
                {
                    return ExpandPath(mainTreeView.GetChildren(bi), path, pathIndex + 1);
                }
                foundbi = bi;
            }
            return foundbi;
        }

        public void LogEventHandler(Logger l, Logger.LogEventArgs e)
        {
            logBox.AppendText(e.ToString());
            logBox.ScrollToCaret();
        }

        private void OpenDataBaseButton_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                InitialDirectory = ".",
                Filter = @"SQLite files (.sqlite)|*.sqlite|All files (*.*)|*.*",
                FilterIndex = 2,
                Multiselect = false
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _dbfilename = ofd.FileName;
                mainTreeView.Roots = _dbc.GetBookmarksRoots();
            }
            UpdateInfoLabel();
        }

        private void mainTreeView_SelectionChanged(object sender, EventArgs e)
        {
            if (_ignoreTreeSelect)
            {
                _ignoreTreeSelect = false;
                return;
            }

            var bkm = (BookmarkItem) mainTreeView.SelectedObject;
            if (bkm != null)
            {
                var objects = _dbc.GetBookmarksChildren(bkm.Id);
                mainContentsView.SetObjects(objects);
                mainContentsView.RefreshObjects(objects);
                if (objects.Count > 0)
                {
                    if (_idToSelectInContentView == -1)
                    {
                        mainContentsView.SelectedIndex = 0;
                        mainContentsView.EnsureVisible(0);
                        _ignoreContentSelect = true;
                    }
                    else
                    {
                        foreach (var bi in (mainContentsView.Objects).Cast<BookmarkItem>()
                            .Where(bi => bi.Id == _idToSelectInContentView))
                        {
                            mainContentsView.SelectObject(bi);
                            mainContentsView.EnsureModelVisible(bi);
                            break;
                        }
                        _idToSelectInContentView = -1;
                    }
                    mainContentsView.Focus();
                }
            }
            else
            {
                //disable deselection
                _ignoreTreeSelect = true;
                mainTreeView.SelectedObject = _backupSelectedObjectTreeView;
            }

            EnableEdit(true);
            _mainEditPanel.SelectedBookmark = (BookmarkItem) mainTreeView.SelectedObject;
        }

        private void mainContentsView_SelectionChanged(object sender, EventArgs e)
        {
            if (_ignoreContentSelect)
            {
                _ignoreContentSelect = false;
                return;
            }
            if (mainContentsView.SelectedObjects.Count > 0)
            {
                if (mainContentsView.SelectedObjects.Count == 1)
                {
                    //show edit
                    EnableEdit(true);
                    _mainEditPanel.SelectedBookmark = (BookmarkItem) mainContentsView.SelectedObject;
                }
                else
                {
                    //show some stats
                    EnableEdit(false);
                }
            }
            else
            {
                //disable deselection
                mainContentsView.SelectObjects(_backupSelectedListContentView);
            }
        }

        private void EnableEdit(bool val)
        {
            _mainEditPanel.Visible = val;
            _mainInfoPanel.Visible = !val;
        }

        private void ButtonFindDupes_Click(object sender, EventArgs e)
        {
            if (!_duplicatesView.Visible)
            {
                _duplicatesView.Show();
            }
            else
            {
                _duplicatesView.Activate();
            }
        }

        private void MainView_FormClosing(object sender, FormClosingEventArgs e)
        {
            _duplicatesView.AppIsClosing = true;
            e.Cancel = false;
        }

        private void MainContentsView_ItemActivate(object sender, EventArgs e)
        {
            var indices = mainContentsView.SelectedIndices;
            if (indices.Count <= 0) return;
            //select the item with the first index from the selected indexes collection
            var bi = ((BookmarkItem) ((OLVListItem) mainContentsView.Items[indices[0]]).RowObject);
            if (bi.IsDirectory)
            {
                var pathNodes = _dbc.GetPath(bi.Id);
                SelectContentItem(pathNodes);
            }
            else
            {
                if (bi.IsBookmark)
                {
                    Process.Start(bi.Location);
                }
            }
        }

        private void mainTreeView_ItemActivate(object sender, EventArgs e)
        {
            var indices = mainTreeView.SelectedIndices;
            if (indices.Count > 0)
            {
                mainTreeView.Expand(((OLVListItem) mainTreeView.Items[indices[0]]).RowObject);
            }
        }

        private void MainView_DragDrop(object sender, DragEventArgs e)
        {
            //TODO: WIP
            Console.WriteLine(@"Something dropped");
            var formats = e.Data.GetFormats();
            foreach (var s in formats)
            {
                Console.WriteLine(@"format: " + s);
            }
            var ms = (MemoryStream) e.Data.GetData("DragContext");
            ms.Position = 0;
            var sr = new StreamReader(ms);
            var str = sr.ReadToEnd();
            Console.WriteLine(str);
        }

        private void MainView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void mainContentsView_CellRightClick(object sender, CellRightClickEventArgs e)
        {
            //reset
            _cmenuItems["OpenAll"].Visible = false;
            _cmenuItems["Open"].Visible = false;
            _cmenuItems["Sep.OpenNew"].Visible = false;
            _cmenuItems["SortName"].Visible = false;
            _cmenuItems["Sep.DelSort"].Visible = false;

            switch (mainContentsView.SelectedObjects.Count)
            {
                case 0:
                    //selected outside list
                    if (mainTreeView.SelectedObjects.Count == 1)
                    {
                        //processing of the selected directory in MainTreeView
                        _cmenuItems["OpenAll"].Visible = true;
                        _cmenuItems["Sep.OpenNew"].Visible = true;
                        _cmenuItems["SortName"].Visible = true;
                        _cmenuItems["Sep.DelSort"].Visible = true;
                    }
                    break;
                case 1:
                    var bi = (BookmarkItem) mainContentsView.SelectedObject;
                    if (bi.IsBookmark)
                    {
                        //processing the selected bookmark in MainContentView
                        _cmenuItems["Open"].Visible = true;
                        _cmenuItems["Sep.OpenNew"].Visible = true;
                    }
                    else
                    {
                        if (bi.IsDirectory)
                        {
                            //processing the selected directory in MainContentView
                            _cmenuItems["OpenAll"].Visible = true;
                            _cmenuItems["Sep.OpenNew"].Visible = true;
                            _cmenuItems["SortName"].Visible = true;
                            _cmenuItems["Sep.DelSort"].Visible = true;
                        }
                    }
                    break;
            }

            e.MenuStrip = _cmenu;
        }

        private void mainTreeView_CellRightClick(object sender, CellRightClickEventArgs e)
        {
            if (mainTreeView.SelectedObjects.Count == 0) return;
            _cmenuItems["Open"].Visible = false;

            _cmenuItems["OpenAll"].Visible = true;
            _cmenuItems["Sep.OpenNew"].Visible = true;
            _cmenuItems["SortName"].Visible = true;
            _cmenuItems["Sep.DelSort"].Visible = true;
            e.MenuStrip = _cmenu;
        }

        private void mainContentsView_MouseDown(object sender, MouseEventArgs e)
        {
            _backupSelectedListContentView = mainContentsView.SelectedObjects;
        }

        private void mainTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            _backupSelectedObjectTreeView = mainTreeView.SelectedObject;
        }
    }
}