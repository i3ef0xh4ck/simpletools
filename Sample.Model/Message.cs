using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Model
{
    public class Message
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public string Code { get; set; }

        public int Status { get; set; }

        public DateTime Intime { get; set; }

        public string IntimeStr { get; set; }
    }
}
