using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models
{
    public class ModelView
    {
        public ModelView()
        {
            this.votes = new List<ModelViewItem>();
        }

        public int views { get; set; }
        public IList<ModelViewItem> votes { get; set; }

    }

    public class ModelViewItem
    {
        public int option_id { get; set; }
        public int qty { get; set; }
    }
}
