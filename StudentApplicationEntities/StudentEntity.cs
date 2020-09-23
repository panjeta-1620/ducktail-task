using System;
using System.Collections.Generic;
using System.Text;

namespace StudentApplicationEntities
{
   public class StudentEntity
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ClassID { get; set; }
        public List<int> SubjectsIDs { get; set; }
        public List<decimal> Marks { get; set; }
    }
}
