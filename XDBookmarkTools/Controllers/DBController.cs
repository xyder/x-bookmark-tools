using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using XDBookmarkTools.Model;

namespace XDBookmarkTools.Controllers
{
    public class DbController
    {
        private SQLiteConnection _conn;
        private ArrayList _folders;
        public string Filename;

        public DbController(string filename)
        {
            Filename = filename;
        }

        private void SetConnection()
        {
            //TODO: check if file exists or at least if directory exists for new databases
            _conn = new SQLiteConnection("Data Source=" + Filename + ";Version=3;");
        }

        public SQLiteCommand CreateCommand(string cmdtext, SQLiteParameter[] parameters = null)
        {
            SetConnection();
            _conn.Open();

            //override to prevent automatic indexing
            var pragma = _conn.CreateCommand();
            pragma.CommandText = "PRAGMA automatic_index=off;";
            pragma.ExecuteNonQuery();

            var cmd = _conn.CreateCommand();
            cmd.CommandText = cmdtext;
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            return cmd;
        }

        /// <summary>
        /// Executes the sql query and returns a SqliteDataReader.
        /// The connection must be closed manually after data processing is finished.
        /// </summary>
        /// <param name="cmdtext">the sql command to be executed.</param>
        /// <param name="parameters">used in parametrized sql commands.</param>
        /// <returns></returns>
        public SQLiteDataReader ExecuteQuery(string cmdtext, SQLiteParameter[] parameters = null)
        {
            var cmd = CreateCommand(cmdtext, parameters);
            var ret = cmd.ExecuteReader();
            return ret;
        }

        public bool ExecuteNonQuery(string cmdtext, SQLiteParameter[] parameters = null)
        {
            var cmd = CreateCommand(cmdtext, parameters);
            var ret = cmd.ExecuteNonQuery();
            _conn.Close();
            return ret != 0;
        }

        public long ExecuteScalar(string cmdtext, SQLiteParameter[] parameters = null)
        {
            var cmd = CreateCommand(cmdtext, parameters);
            var ret = (long) cmd.ExecuteScalar();
            _conn.Close();
            return ret;
        }

        public bool HasChildrenBookmarks(long idparent)
        {
            var ret = _folders.Contains(idparent);
            return ret;
        }

        public Dictionary<string, long> CountChildren(long idparent)
        {
            var reader = ExecuteQuery("SELECT type, COUNT(*) AS nr FROM moz_bookmarks"
                                      + " WHERE parent = @idparent GROUP BY type",
                new[] {new SQLiteParameter("idparent", idparent)});
            var ret = BookmarksCountToDictionary(reader);
            _conn.Close();
            return ret;
        }

        public Dictionary<string, long> CountChildrenRecursive(long idparent)
        {
            var reader = ExecuteQuery("SELECT type, COUNT(*) AS nr FROM moz_bookmarks WHERE parent IN"
                                      + " (WITH RECURSIVE cte(id) AS"
                                      + " (SELECT id FROM moz_bookmarks WHERE id = @idparent"
                                      + " UNION ALL"
                                      +
                                      " SELECT mb.id FROM moz_bookmarks AS mb, cte WHERE mb.type=2 AND cte.id = mb.parent)"
                                      + " SELECT id FROM cte) GROUP BY type",
                new[] {new SQLiteParameter("idparent", idparent)});

            var ret = BookmarksCountToDictionary(reader);
            _conn.Close();
            return ret;
        }

        public Dictionary<string, long> CountBookmarks()
        {
            var ret = new Dictionary<string, long>();
            var bkmMenuDict = CountChildrenRecursive(2);
            var bkmToolbDict = CountChildrenRecursive(3);
            var bkmUnsortedDict = CountChildrenRecursive(5);
            ret["nr.bookmarks"] =
                (bkmMenuDict.ContainsKey("nr.bookmarks") ? bkmMenuDict["nr.bookmarks"] : 0)
                + (bkmToolbDict.ContainsKey("nr.bookmarks") ? bkmToolbDict["nr.bookmarks"] : 0)
                + (bkmUnsortedDict.ContainsKey("nr.bookmarks") ? bkmUnsortedDict["nr.bookmarks"] : 0);
            ret["nr.folders"] =
                (bkmMenuDict.ContainsKey("nr.folders") ? bkmMenuDict["nr.folders"] : 0)
                + (bkmToolbDict.ContainsKey("nr.folders") ? bkmToolbDict["nr.folders"] : 0)
                + (bkmUnsortedDict.ContainsKey("nr.folders") ? bkmUnsortedDict["nr.folders"] : 0);
            ret["nr.separators"] =
                (bkmMenuDict.ContainsKey("nr.separators") ? bkmMenuDict["nr.separators"] : 0)
                + (bkmToolbDict.ContainsKey("nr.separators") ? bkmToolbDict["nr.separators"] : 0)
                + (bkmUnsortedDict.ContainsKey("nr.separators") ? bkmUnsortedDict["nr.separators"] : 0);
            ret["nr.total"] =
                (bkmMenuDict.ContainsKey("nr.total") ? bkmMenuDict["nr.total"] : 0)
                + (bkmToolbDict.ContainsKey("nr.total") ? bkmToolbDict["nr.total"] : 0)
                + (bkmUnsortedDict.ContainsKey("nr.total") ? bkmUnsortedDict["nr.total"] : 0);
            return ret;
        }

        private static Dictionary<string, long> BookmarksCountToDictionary(IDataReader reader)
        {
            var ret = new Dictionary<string, long>();
            ret["nr.total"] = 0;
            while (reader.Read())
            {
                var key = "default";
                switch ((long) reader["type"])
                {
                    case 1:
                        key = "nr.bookmarks";
                        break;
                    case 2:
                        key = "nr.folders";
                        break;
                    case 3:
                        key = "nr.separators";
                        break;
                }
                ret[key] = (long) reader["nr"];
                if (key != "default")
                {
                    ret["nr.total"] += (long) reader["nr"];
                }
            }
            return ret;
        }

        public long CountDuplicates()
        {
            return ExecuteScalar("select sum(dupe_nr) FROM (SELECT title, fk, count(*)-1 AS dupe_nr"
                                 + " FROM moz_bookmarks WHERE type = 1 GROUP BY fk)"
                                 + " INNER JOIN moz_places ON fk=moz_places.id");
        }

        public void RefreshFoldersList()
        {
            //TODO: refresh empty folders on every refresh/add/delete
            var reader = ExecuteQuery(
                "SELECT * FROM moz_bookmarks WHERE id IN"
                + " (SELECT parent FROM moz_bookmarks WHERE type=2 GROUP BY parent)");

            _folders = new ArrayList();
            while (reader.Read())
            {
                _folders.Add((long) reader["id"]);
            }
            _conn.Close();
        }

        public ArrayList GetBookmarksRoots()
        {
            var roots = GetBookmarksChildren(1, 2);
            var ret = new ArrayList();

            //NOTE: this may break with localization or if ids don't match
            //TODO: further test and fine tune bookmarks roots filtering
            //whitelist:
            long[] ids = {2, 3, 5}; //ids for "Bookmarks Menu", "Bookmarks Toolbar" and "Unsorted Bookmarks"
            foreach (var bi in roots.Cast<BookmarkItem>()
                .Where(bi => ids.Contains(bi.Id)))
            {
                ret.Add(bi);
            }
            return ret;
        }

        public ArrayList GetBookmarksChildren(long idparent, long type = -1)
        {
            //TODO: check if title is empty and fetch the one from moz_places if necessary
            var reader = ExecuteQuery(
                "SELECT moz_bookmarks.id AS id, moz_bookmarks.title AS title, parent, type, url, content, dateAdded, lastModified"
                + " FROM (moz_bookmarks LEFT JOIN moz_places ON moz_bookmarks.fk=moz_places.id)"
                + " LEFT JOIN "
                + "(SELECT item_id, anno_attribute_id, content FROM moz_items_annos WHERE anno_attribute_id=2) mia"
                + " ON moz_bookmarks.id=mia.item_id"
                + " WHERE parent = @idparent"
                + (type == -1 ? "" : " and type = " + type)
                + " ORDER BY position ASC",
                new[] {new SQLiteParameter("idparent", idparent)});

            var ret = new ArrayList();
            while (reader.Read())
            {
                var bkm = new BookmarkItem
                {
                    Id = (long) reader["id"],
                    Parent = (long) reader["parent"],
                    Title = reader["title"].ToString(),
                    Type = reader.GetInt32(reader.GetOrdinal("type")),
                    Location = reader["url"].ToString(),
                    Description = reader["content"].ToString(),
                    LastModified = (long) reader["lastModified"],
                    DateAdded = (long) reader["dateAdded"]
                };
                ret.Add(bkm);
            }
            _conn.Close();
            return ret;
        }

        public ArrayList GetDuplicatesRoots()
        {
            //TODO: fish out title if places title is empty. if mbkm title is empty too .. try fetching from the other duplicates
            var reader = ExecuteQuery(
                "select mbkm.title as bkm_title, moz_places.title as places_title, url, fk, dupe_nr FROM ("
                + "SELECT title, fk, count(*) AS dupe_nr FROM moz_bookmarks WHERE fk NOT NULL GROUP BY fk"
                + ") mbkm INNER JOIN moz_places ON fk=moz_places.id WHERE dupe_nr > 1");

            var ret = new ArrayList();
            while (reader.Read())
            {
                var di = new DuplicateItem
                {
                    Title = reader["places_title"].ToString(),
                    PathString = reader["url"].ToString(),
                    Fk = (long) reader["fk"],
                    DupeNr = (long) reader["dupe_nr"],
                    IsRoot = true
                };
                ret.Add(di);
            }
            _conn.Close();
            return ret;
        }

        public ArrayList GetDuplicatesChildren(DuplicateItem uniqueItem)
        {
            var reader = ExecuteQuery("select * from moz_bookmarks where fk = " + uniqueItem.Fk);
            var ret = new ArrayList();
            while (reader.Read())
            {
                var di = new DuplicateItem
                {
                    Title = reader["title"].ToString(),
                    Parent = (long) reader["parent"],
                    Id = (long) reader["id"],
                    LastModified = (long) reader["lastModified"],
                    DateAdded = (long) reader["dateAdded"]
                };
                di.PathNodes = GetPath(di.Parent);
                di.IsRoot = false;
                ret.Add(di);
            }
            _conn.Close();
            return ret;
        }

        public ArrayList GetPath(long parent)
        {
            var reader = ExecuteQuery("WITH RECURSIVE cte(title, id, parent) AS"
                                      + " (SELECT title, id, parent FROM moz_bookmarks WHERE id = " + parent
                                      + " UNION ALL SELECT mb.title, mb.id, mb.parent FROM moz_bookmarks AS mb, cte "
                                      + "WHERE cte.parent = mb.id AND mb.id > 1)"
                                      + " SELECT * FROM cte");
            var ret = new ArrayList();

            while (reader.Read())
            {
                var bi = new BookmarkItem
                {
                    Id = (long) reader["id"],
                    Parent = (long) reader["parent"],
                    Title = reader["title"].ToString()
                };
                ret.Insert(0, bi);
            }
            _conn.Close();
            return ret;
        }
    }
}