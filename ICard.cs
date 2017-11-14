using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public interface ICard
    {
        Suit suit
        {
            get;
            set;
        }
        Rank rank
        {
            get;
            set;
        }
        bool facingUp
        {
            get;
            set;
        }
    }
}
