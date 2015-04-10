using System.Collections;

namespace XDBookmarkTools.Model
{
    public class DuplicateItem : BookmarkItem
    {
        private string _pathstr;
        public long Fk, DupeNr;
        public bool IsRoot;
        public ArrayList PathNodes;

        public string PathString
        {
            get
            {
                if (PathNodes == null || PathNodes.Count <= 0) return _pathstr;
                _pathstr = "";
                const string sep = " > ";
                foreach (BookmarkItem bi in PathNodes)
                {
                    _pathstr += bi.Title + sep;
                }

                _pathstr = _pathstr.Substring(0, _pathstr.Length - sep.Length);
                return _pathstr;
            }
            set { _pathstr = value; }
        }
    }
}