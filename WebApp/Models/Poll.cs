

using System.Collections.Generic;

namespace WebApp.Models
{
    public class Poll
    {
        public Poll()
        {
            this.options = new List<PollOption>();
        }

        public int poll_id { get; set; }
        public string poll_description { get; set; }
        public IList<PollOption> options { get; set; }
    }
}
