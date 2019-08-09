using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DataModel
{
    public class Player
    {
        public Guid GUID { get; }
        public string Name { get; set; }

        public Player()
        {
            GUID = Guid.NewGuid();
            Name = "Unnamed";
        }
    }
}
