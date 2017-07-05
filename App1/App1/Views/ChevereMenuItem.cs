using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Views
{

    public class ChevereMenuItem
    {

        //public string Title { get; set; }

        //public string IconSource { get; set; }

        //public Type TargetType { get; set; }

        public ChevereMenuItem()
        {
            TargetType = typeof(ChevereDetail);
        }
        public int Id { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}
