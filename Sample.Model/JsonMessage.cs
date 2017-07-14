using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Model
{
    public class JsonMessage
    {
        public bool Flag { get; set; }

        public bool Error { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
