

namespace ConsoleApp.Models
{
    public class PollOption
    {
        public PollOption()
        {
            this.option_id = 0;
        }

        public int option_id { get; set; }
        public int poll_id { get; set; }
        public string option_description { get; set; }
    }
}
