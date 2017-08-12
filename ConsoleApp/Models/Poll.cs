using System.Collections.Generic;


namespace ConsoleApp.Models
{
    public class Poll
    {
        public Poll()
        {
            this.poll_id = 0;
            this.options = new List<PollOption>();
        }

        public int poll_id { get; set; }
        public string poll_description { get; set; }
        public IList<PollOption> options { get; set; }
    }
}
