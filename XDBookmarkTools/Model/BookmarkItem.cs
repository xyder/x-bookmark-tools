using System;

namespace XDBookmarkTools.Model
{
    public class BookmarkItem
    {
        public long Id, Parent, DateAdded, LastModified;
        public int Type;
        public string Title, Description, Location;

        public DateTime DateAddedDt
        {
            get { return ConvertUnixMicrosToDateTime(DateAdded); }
            set { DateAdded = ConvertDateTimeToUnixMicros(value); }
        }

        public DateTime LastModifiedDt
        {
            get { return ConvertUnixMicrosToDateTime(LastModified); }
            set { LastModified = ConvertDateTimeToUnixMicros(value); }
        }

        public bool IsBookmark
        {
            get { return Type == 1; }
        }

        public bool IsDirectory
        {
            get { return Type == 2; }
        }

        public bool IsSeparator
        {
            get { return Type == 3; }
        }

        public static long ConvertDateTimeToUnixMicros(DateTime val)
        {
            return (val.Ticks - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks)/10;
        }

        public static DateTime ConvertUnixMicrosToDateTime(long val)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddTicks(val*10);
        }
    }
}