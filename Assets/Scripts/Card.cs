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

		private Animator animator;
		private ParticleSystem myParticleSystem;
		private Vector3 targetPosition;
		private Boolean isMoving = false;

		private float startTime;
		private float journeyLength;
		private Vector3 startPosition;
		private float movementSpeed = 4.0f;

		public Boolean disableColliders = true; // handles tableflip cards

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

			MeshRenderer mr = GetComponentInChildren<MeshRenderer> ();
			psr.material = mr.materials[2];
			movementSpeed = 4.0f;
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
						Vector3 currentRotation = transform.eulerAngles;
						Vector3 targetRotation = currentRotation;
						targetRotation.y = (currentRotation.y + 180);
						transform.rotation = Quaternion.Euler (targetRotation); 
					}
					myFacingUp = value;
				}
            }
        }

		public bool facingUpNoAnimation {
			get
			{
				return myFacingUp;
			}
			set
			{
				if (myFacingUp != value) {
					if (animator != null) {
						//animator.SetTrigger ("SideFlip");
						Vector3 currentRotation = transform.eulerAngles;
						Vector3 targetRotation = currentRotation;
						targetRotation.y = (currentRotation.y + 180);
						transform.rotation = Quaternion.Euler (targetRotation); 
					}
					myFacingUp = value;
				}
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
			movementSpeed = 10.0f;
			targetPosition = position;
			startTime = Time.time;
			startPosition = transform.localPosition;
			journeyLength = Vector3.Distance (startPosition, targetPosition);
			if (disableColliders) {
				Collider col = GetComponent<Collider> ();
				if (col != null) {
					col.enabled = false;
				}
			}
		}

		public void MoveBackTo(Vector3 position) {
			movementSpeed = 20.0f;
			targetPosition = position;
			startTime = Time.time;
			startPosition = transform.localPosition;
			journeyLength = Vector3.Distance (startPosition, targetPosition);
			if (disableColliders) {
				Collider col = GetComponent<Collider> ();
				if (col != null) {
					col.enabled = false;
				}
			}
		}



		public void Update() {
			if (!isMoving && transform.localPosition != targetPosition) {
				float distCovered = (Time.time - startTime) * movementSpeed;
				float fracJourney = distCovered / journeyLength;
				transform.localPosition = Vector3.Lerp (startPosition, targetPosition, fracJourney);
			} else if (transform.localPosition == targetPosition) {
				if (disableColliders) {
					Collider col = GetComponent<Collider> ();
					if (col != null) {
						col.enabled = true;
					}
				}
			}
		}

		public void PickedUp() {
			StopParticle ();
			isMoving = true;
		}

		public void Dropped() {
			isMoving = false;
		}

		public void SetPosition(Vector3 position) {
			this.targetPosition = position;
			transform.localPosition = position;
			if (disableColliders) {
				Collider col = GetComponent<Collider> ();
				if (col != null) {
					col.enabled = false;
				}
			}
		}
    }
}
