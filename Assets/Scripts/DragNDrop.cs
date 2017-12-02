using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Solitaire {
public class DragNDrop : MonoBehaviour
{
	private bool _mouseState;
	private GameObject[] targets;
	public Vector3[] offsets;
	public Vector3[] screenSpaces;
	public Vector3[] originPositions;
	private Quaternion[] originRotations;
	private Vector3[] originScales;
	
	
		private static int instanceCount = 0;

	private Card carringCard;
		public Pile stockPile;

	// Use this for initialization
	void Start()
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
	void Update()
	{


		// Debug.Log(_mouseState);
		if (Input.GetMouseButtonDown(0)|| (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began))
		{
			//origin = transform.localPosition;
			RaycastHit hitInfo;
			targets[0] = GetClickedObject(out hitInfo);
				if (targets[0] != null)
			{
					Card card = targets[0].GetComponent<Card>();
				if (card != null)
				{
						if (card.facingUp) { // only pickup face up cards

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
								Vector3 heldPosition2 = targets[ii].transform.position;
								if (!_mouseState) {
									heldPosition2.z -= 0.5f;
								}
								screenSpaces[ii] = Camera.main.WorldToScreenPoint(heldPosition2);
								offsets[ii] = heldPosition2 - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenSpaces[ii].z));
								// offsets[ii].z = -0.5f;
								Collider col = targets[ii].GetComponent<Collider> ();
								if (col != null) {
									col.enabled = false;
								}
								ii++;
							}
							carringCard = card;

							_mouseState = true;
						}
				}
			}
		}
			if (_mouseState) {
				if (Input.GetMouseButtonUp (0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended)) {
					_mouseState = false;
					RaycastHit hitInfo;
					GameObject droptarget = GetClickedObject (out hitInfo);
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
								if (tp.GetSize () == 0 && card.rank == Rank.King) {
									tp.AddPile (srcPile.SplitPileAtCard (card));
									card.Particle ();
									card.Dropped ();
									handled = true;
									int ii = 0;
									while (targets [ii] != null) {
										Card cardTarget = targets [ii].GetComponent<Card> ();
										if (cardTarget != null) {
											cardTarget.Dropped();
										}
										Collider col = targets[ii].GetComponent<Collider> ();
										if (col != null) {
											col.enabled = true;
										}
										ii++;
									}
								} else if (tp.GetSize () > 0) {
									Card topCard = tp.GetCardFromTop ();
									if (topCard.GetColor () != card.GetColor () &&
										((int)topCard.rank) -1 == ((int)card.rank)) {

										tp.AddPile (srcPile.SplitPileAtCard (card));
										card.Particle ();
										card.Dropped ();

										handled = true;
											int ii = 0;
											while (targets [ii] != null) {
												Card cardTarget = targets [ii].GetComponent<Card> ();
												if (cardTarget != null) {
													cardTarget.Dropped();
												}
												Collider col = targets[ii].GetComponent<Collider> ();
												if (col != null) {
													col.enabled = true;
												}
												ii++;
											}
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
							offsets [i] = new Vector3();
							originPositions[i] = new Vector3();
							originRotations[i] = new Quaternion();
							originScales[i] = new Vector3();
							screenSpaces [i] = new Vector3();
						}

					} else if (carringCard != null) {
						carringCard.Dropped ();
						ReturnToPreviousPosition (carringCard.gameObject);
						carringCard = null;

					}

				}
			} else {
				// No Card just a tap
				if (Input.GetMouseButtonUp (0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended)) {
					RaycastHit hitInfo;
					GameObject taptarget = GetClickedObject (out hitInfo);
					if (taptarget != null) {
						Deck deck = taptarget.GetComponentInParent<Deck>();
						if (deck != null) {
							Card deckCard = deck.RemoveCardFromTop ();
							if (deckCard == null) {
								// Move Stock Pile to Deck
								Card stockCard = stockPile.RemoveCardFromTop ();
								while(stockCard != null) {
									stockCard.facingUp = false;
									deck.AddCardToTop (stockCard);
									stockCard = stockPile.RemoveCardFromTop ();
								}
							} else {
								deckCard.facingUp = true;
								stockPile.AddCardToTop (deckCard);
							}
						}
					}
				}
			}
		if (_mouseState)
		{

				int i = 0;
				while (targets [i] != null) {
					
						Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpaces[i].z);
						Vector3 worldPoint = Camera.main.ScreenToWorldPoint(curScreenSpace);
						
						Vector3 curPosition = worldPoint + offsets[i];
						targets [i].transform.position = curPosition;
					i++;
				}
		}
	}


	GameObject GetClickedObject(out RaycastHit hit)
	{
		GameObject target = null;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
		{
			target = hit.collider.gameObject;
		}

		return target;
	}

		private void ReturnToPreviousPosition(GameObject target) {
			if (target != null) {
				int ii = 0;
				while (targets [ii] != null) {
					Card cardTarget = targets [ii].GetComponent<Card> ();
					if (cardTarget != null) {
						cardTarget.Dropped();
						cardTarget.MoveBackTo(originPositions [ii]);
					}
					targets [ii].transform.localRotation = originRotations [ii];
					targets [ii].transform.localScale = originScales [ii];
					ii++;
				}

			}
		
		}
}
}