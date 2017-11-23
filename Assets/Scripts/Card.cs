using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Solitaire
{
	[Serializable]
	public class Card : MonoBehaviour, ICard, IComparable<ICard>
    {
		[SerializeField]
        private Suit mySuit;
		[SerializeField]
        private Rank myRank;
		[SerializeField]
        private bool myFacingUp;


		public void Start() {
			
		}

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

		public static String[] shortSuits = {"D","C","H","S" };

		public static String[] shortRanks = {"A","2","3","4", "5","6","7", "8","9","X","J","Q","K" };

		public String ToShortName(){
			return shortRanks[(int)this.rank] + shortSuits[(int)this.suit];
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
				if (myFacingUp == true) {
					this.transform.localRotation = Quaternion.Euler( new Vector3 (0,0,0));
						} else {
					this.transform.localRotation = Quaternion.Euler( new Vector3 (0,180,0));
						}
            }
        }

        public void Flip()
        {
            this.myFacingUp = !this.myFacingUp;
        }
    }
}
