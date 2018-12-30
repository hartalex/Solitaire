using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Solitaire
{
	public class DragNDrop : MonoBehaviour
	{
		private bool _mouseState;
		private GameObject[] targets;
		public Vector3[] offsets;
		public Vector3[] screenSpaces;
		public Vector3[] originPositions;
		private Quaternion[] originRotations;
		private Vector3[] originScales;
	
		private float doubleTapTimeFrame = 1f;
		private float tapStartTime = -100.0f;
		private GameObject lastTapTarget = null;
		private static int instanceCount = 0;

		private Card carringCard;
		public Pile stockPile;
		public float tapRadius;
		public GameState gameState;

		// Use this for initialization
		void Start ()
		{
			instanceCount++;
			targets = new GameObject[52];
			offsets = new Vector3[52];
			originPositions = new Vector3[52];
			originRotations = new Quaternion[52];
			originScales = new Vector3[52];
			screenSpaces = new Vector3[52];
			if (instanceCount > 1) {
				throw new UnityException ("DragNDrop is in multiple objects");
			}
		}

		// Update is called once per frame
		void Update ()
		{
			float currentTime = Time.time - tapStartTime;
			// Debug.Log(_mouseState);
			if (Input.GetMouseButtonDown (0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began)) {
				//origin = transform.localPosition;
				RaycastHit hitInfo;
				targets [0] = GetNearestGameObject (out hitInfo, false);
				if (targets [0] != null) {
					Card card = targets[0].GetComponent<Card>();
					if (currentTime < doubleTapTimeFrame) {
						// double tap
						if (lastTapTarget == targets [0]) {
								
							if (!gameState.AutoPlaceCard (card)) {
								CardTouched (card);
							}

						} else {
							CardTouched (card);
						}
					} else {
						CardTouched (card);
					}


				}
			}
			if (_mouseState) {
				if (Input.GetMouseButtonUp (0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended)) {
					_mouseState = false;
					RaycastHit hitInfo;
					GameObject droptarget = GetNearestGameObject (out hitInfo, true);
					if (droptarget != null && carringCard != null) {
						Pile destPile = droptarget.GetComponentInParent<Pile> ();
						Pile srcPile = carringCard.GetComponentInParent<Pile> ();
				
						Card card = carringCard;
						if (destPile != null && card != null && srcPile != null) {
							bool handled = false;
							// Foundation Piles
							FoundationPile fp = destPile.GetComponent<FoundationPile> ();
							if (fp != null) {
								if (fp.AddCard (card)) {
									srcPile.RemoveCardFromTop ();
									card.Particle ();
									card.Dropped ();
									carringCard = null;
								} else {
									carringCard.Dropped ();
									ReturnToPreviousPosition (carringCard.gameObject);
									carringCard = null;
								}
								handled = true;
							}

							// TableauPiles
							TableauPile tp = destPile.GetComponent<TableauPile> ();
							if (tp != null) {
								if (tp.IsValidMove(card))
								{
									tp.AddPile(srcPile.SplitPileAtCard(card));
									card.Particle();
									card.Dropped();
									handled = true;
									int ii = 0;
									while (targets[ii] != null)
									{
										Card cardTarget = targets[ii].GetComponent<Card>();
										if (cardTarget != null)
										{
											cardTarget.Dropped();
										}
										Collider col = targets[ii].GetComponent<Collider>();
										if (col != null)
										{
											col.enabled = true;
										}
										ii++;
									}
								}
							}

							if (!handled) {
								carringCard.Dropped ();
								ReturnToPreviousPosition (carringCard.gameObject);
								carringCard = null;
							}


						} else {
							carringCard.Dropped ();
							ReturnToPreviousPosition (carringCard.gameObject);
							carringCard = null;
						}
				
						for (int i = 0; i < 52; i++) {
							targets [i] = null;
							offsets [i] = new Vector3 ();
							originPositions [i] = new Vector3 ();
							originRotations [i] = new Quaternion ();
							originScales [i] = new Vector3 ();
							screenSpaces [i] = new Vector3 ();
						}

					} else if (carringCard != null) {
						carringCard.Dropped ();
						ReturnToPreviousPosition (carringCard.gameObject);
						carringCard = null;

					}

				}
			}

			if (_mouseState) {

				int i = 0;
				while (targets [i] != null) {
					
					Vector3 curScreenSpace = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenSpaces [i].z);
					Vector3 worldPoint = Camera.main.ScreenToWorldPoint (curScreenSpace);
						
					Vector3 curPosition = worldPoint + offsets [i];
					targets [i].transform.position = curPosition;
					i++;
				}
			}
		}

		public void CardTouched (Card card)
		{

			// single tap
            if (card != null) {
				tapStartTime = Time.time;
				lastTapTarget = targets [0];
				if (card.facingUp) { // only pickup face up card
					TableauPile srcPile = card.GetComponentInParent<TableauPile> ();
					if (srcPile != null) {
						Card[] multipleCards = srcPile.GetPileAtCard (card);
						int i = 0;
						while (multipleCards [i] != null) {
							targets [i] = multipleCards [i].gameObject;
							originPositions [i] = targets [i].transform.localPosition;
							originRotations [i] = targets [i].transform.localRotation;
							originScales [i] = targets [i].transform.localScale;
							multipleCards [i].PickedUp ();
							i++;
						}
					} else {
						targets [0] = card.gameObject;
						originPositions [0] = targets [0].transform.localPosition;
						originRotations [0] = targets [0].transform.localRotation;
						originScales [0] = targets [0].transform.localScale;

					}

					card.PickedUp ();

					// offset.z = -0.5f;
					int ii = 0;
					while (targets [ii] != null) {
						Vector3 heldPosition2 = targets [ii].transform.position;
						if (!_mouseState) {
							heldPosition2.z -= 0.5f;
						}
						screenSpaces [ii] = Camera.main.WorldToScreenPoint (heldPosition2);
						offsets [ii] = heldPosition2 - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenSpaces [ii].z));
						// offsets[ii].z = -0.5f;
						Collider col = targets [ii].GetComponent<Collider> ();
						if (col != null) {
							col.enabled = false;
						}
						ii++;
					}
					carringCard = card;

					_mouseState = true;
				}
			}
			Deck deck = targets [0].GetComponentInParent<Deck> ();
			if (deck != null) {
				Card deckCard = deck.RemoveCardFromTop ();
				if (deckCard == null) {
					// Move Stock Pile to Deck
					Card stockCard = stockPile.RemoveCardFromTop ();
					while (stockCard != null) {
						stockCard.facingUp = false;
						deck.AddCardToTop (stockCard);
						stockCard = stockPile.RemoveCardFromTop ();
						tapStartTime = -100f;
						lastTapTarget = null;
					}
				} else {
					deckCard.facingUp = true;
					stockPile.AddCardToTop (deckCard);
					tapStartTime = -100f;
					lastTapTarget = null;
				}
			}
		}

		GameObject GetNearestGameObject(out RaycastHit hit, bool isDrop)
		{
			GameObject target = null;
            // Raycast
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
			{
				target = hit.collider.gameObject;

				if (target.layer != 9 && target.layer != 10)
				{
					Vector3 center = hit.point;

					// nearest object
					Collider[] hitColliders = Physics.OverlapSphere(center, tapRadius);
					int i = 0;
					float shortestLength = float.MaxValue;
					while (i < hitColliders.Length)
					{
						// determine best object
						GameObject go = hitColliders[i].gameObject;

						float distance = Vector3.Distance(go.transform.position, center);
						if (distance < shortestLength && (go.layer == 9 || (isDrop && go.layer == 10)))
						{
							shortestLength = distance;
							target = go;
							Debug.Log("target distance " + distance + " target: " + target);
						}
						i++;
					}
				}
			}
			else
			{
				Debug.Log("raycast target found " + target);
			}
			return target;
		}

		private void ReturnToPreviousPosition (GameObject target)
		{
			if (target != null) {
				int ii = 0;
				while (targets [ii] != null) {
					Card cardTarget = targets [ii].GetComponent<Card> ();
					if (cardTarget != null) {
						cardTarget.Dropped ();
						cardTarget.MoveBackTo (originPositions [ii]);
					}
					targets [ii].transform.localRotation = originRotations [ii];
					targets [ii].transform.localScale = originScales [ii];
					ii++;
				}

			}
		
		}
	}
}