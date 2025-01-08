using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diploma.Models
{
    public partial class TimeTableModel
    {
        public TimeTableModel(DateTime dateTime, string span)
        {
            this.DateTime = dateTime;
            this.Span = span;
        }

        public DateTime DateTime { get; set; }
        public int Index { get; set; }
        public string Span { get; set; }

    }
}

