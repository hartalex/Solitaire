﻿using System;
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

		private Animator animator;
		private ParticleSystem myParticleSystem;
		private Vector3 targetPosition;
		private Boolean isMoving = false;

		private float journeyLength;

		public void Start() {
			
			animator = GetComponentInChildren<Animator> ();
			if (animator == null) {
				throw new MissingComponentException ("Animator");
			}
			myParticleSystem = GetComponentInChildren<ParticleSystem> ();
			if (myParticleSystem == null) {
				throw new MissingComponentException ("ParticleSystem");
			}
			ParticleSystemRenderer psr = myParticleSystem.GetComponent<ParticleSystemRenderer> ();

			SkinnedMeshRenderer mr = GetComponentInChildren<SkinnedMeshRenderer> ();
			psr.material = mr.materials[2];
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
				if (myFacingUp != value) {
					if (animator != null) {
						animator.SetTrigger ("SideFlip");
					}

					//this.transform.localRotation = Quaternion.Euler( new Vector3 (90,0,0));
				}
				myFacingUp = value;

            }
        }

        public void Flip()
        {
            this.myFacingUp = !this.myFacingUp;
        }

		public Color GetColor() {
			Color retval = Color.Black;
			if (suit == Suit.Diamond || suit == Suit.Heart) {
				retval = Color.Red;
			}
			return retval;
		}

		public void Particle() {
			if (myParticleSystem != null) {
				myParticleSystem.Play ();
			}
		}

		public void StopParticle() {
			if (myParticleSystem != null) {
				myParticleSystem.Stop ();
				myParticleSystem.Clear ();
			}
		}

		public void MoveTo(Vector3 position) {
			targetPosition = position;
		}

		public void Update() {
			if (!isMoving && transform.localPosition != targetPosition) {
				transform.localPosition = targetPosition;
			}
		}

		public void PickedUp() {
			StopParticle ();
			isMoving = true;
		}

		public void Dropped() {
			isMoving = false;
		}
    }
}
