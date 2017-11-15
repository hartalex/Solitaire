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
			updateTiling ();
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
				updateTiling ();
            }
        }

		private void updateTiling() {
			float suitOffset = 0;
			float rankOffset = 0;
			float suitTileSize = 0.25f;
			float rankTileSize = 0.07692f;

			if (this.suit == Suit.Heart) {
				suitOffset = 3 * suitTileSize;
			} else if (this.suit == Suit.Spade) {
				suitOffset = 2 * suitTileSize;
			} else if (this.suit == Suit.Diamond) {
				suitOffset = 1 * suitTileSize;
			} else {
				suitOffset = 0 * suitTileSize;
			}
			rankOffset = (int)this.rank * rankTileSize;

			this.gameObject.GetComponent<MeshRenderer> ().material.SetTextureOffset ("_MainTex", new Vector2 (rankOffset, suitOffset));
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
				updateTiling ();
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
