using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication
{
    public static class old_files
    {
        static string file_path;
        static List<string> name_files = new List<string>();
        static List<int> version_files;
        static List<DateTime> date_modified_files;

        public static string path
        {
            get
            {
                return file_path;
            }
            set
            {
                file_path = value;
            }
        }
        public static List<string> names
        {
            get
            {
                return name_files;
            }
            set
            {
                name_files = value;
            }
        }

        public static List<DateTime> dates
        {
            get
            {
                return date_modified_files;
            }
            set
            {
                date_modified_files = value;
            }
        }

        public static List<int> versions
        {
            get
            {
                return version_files;
            }
            set
            {
                version_files = value;
            }
        }
    }

}