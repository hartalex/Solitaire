using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Card : ICard, IComparable<ICard>
    {
        private Suit mySuit;
        private Rank myRank;
        private bool myFacingUp;

        public Card(Suit suit, Rank rank, bool facingUp = false)
        {
            this.suit = suit;
            this.rank = rank;
            this.myFacingUp = facingUp;
        }

        public int CompareTo(ICard other)
        {
            if (other == null) return 1;
            int ret = this.suit.CompareTo(other.suit);
            if (ret == 0)
            {
                ret = this.rank.CompareTo(other.rank);
            }
            return ret;
        }

        public Suit suit
        {
            get
            {
                return mySuit;
            }
            set
            {
                mySuit = value;
            }
        }

        public Rank rank
        {
            get
            {
                return myRank;
            }
            set
            {
                myRank = value;
            }
        }

        public override String ToString()
        {
            String retval = "**** - **** ";
            if (myFacingUp) {
                retval = suit.ToString() + " - " + rank.ToString();
            }
            return retval;
        }

        public bool facingUp
        {
            get
            {
                return myFacingUp;
            }
            set
            {
                myFacingUp = value;
            }
        }

        public void Flip()
        {
            this.myFacingUp = !this.myFacingUp;
        }
    }
}
