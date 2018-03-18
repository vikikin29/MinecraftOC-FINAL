using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftOC
{
    class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return Name + "(" + Value + ")";
        }
    }
}
