using System;
using System.Collections.Generic;
using System.Text;

namespace StudentApplicationEntities
{
   public class StudentSubjectMarks
    {
        public int StudentSubjectMarksID { get; set; }
        public int StudentID { get; set; }
        public int SubjectID { get; set; }
        public float SubjectMarks { get; set; }
    }
}
