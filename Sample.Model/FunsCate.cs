using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Model
{
    public class FunsCate
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int ParentID { get; set; }

        public int GroupIndex { get; set; }

        public DateTime? Intime { get; set; }

        public List<Funs> Funs { get; set; }
    }
}
