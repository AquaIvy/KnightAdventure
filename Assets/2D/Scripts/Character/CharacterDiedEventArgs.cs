using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightAdventure
{
    public class CharacterDiedEventArgs : EventArgs
    {
        public Character Character { get; set; }
    }
}
