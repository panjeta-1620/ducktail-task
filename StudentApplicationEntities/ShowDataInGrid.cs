using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApplicationEntities
{
    [Serializable]
    public class ShowDatainGrid
    {
        public int StudentID
        {
            get;
            set;
        }
        public string StudentName
        {
            get;
            set;

        }
        public string FirstName
        {
            get;
            set;
        }
        public string LastName
        {
            get;
            set;
        }
        public string Class
        {
            get;
            set;
        }
        public string Subject
        {
            get;
            set;
        }
        public List<string> SubjectNames
        {
            get;
            set;
        }
     
        public int? pageIndex { get; set; }

        public int PageSize { get; set; }

        public List<int> SubjectsIDs { get; set; }
        public int SubjectID
        {
            get;
            set;
        }
        public string SubjectName { get; set; }

        public int ClassID { get; set; }

        public string Keyword { get; set; }
    }
}
