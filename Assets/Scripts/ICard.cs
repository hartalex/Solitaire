using System;
using System.Collections.Generic;
using System.Text;


namespace Solitaire
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
