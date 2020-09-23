using System;
using System.Collections.Generic;
using System.Text;

namespace StudentApplicationEntities
{
   public class MarksItem
    {
        public int ItemIndex { get; set; }

        public int? SubjectID { get; set; }

        public decimal Marks { get; set; }
    }
}
