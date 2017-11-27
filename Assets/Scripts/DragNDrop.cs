﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Solitaire {
public class DragNDrop : MonoBehaviour
{
	private bool _mouseState;
	private GameObject target;
	private GameObject[] targets;
	public Vector3 screenSpace;
	public Vector3 offset;
	public Vector3[] offsets;
	public Vector3[] originPositions;
	private Quaternion[] originRotations;
	private Vector3[] originScales;
	
	


	private Card carringCard;
		public Pile stockPile;

	// Use this for initialization
	void Start()
	{
			targets = new GameObject[52];
			offsets = new Vector3[52];
			originPositions = new Vector3[52];
			originRotations = new Quaternion[52];
			originScales = new Vector3[52];
	}

	// Update is called once per frame
	void Update()
	{


		// Debug.Log(_mouseState);
		if (Input.GetMouseButtonDown(0)|| (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began))
		{
			//origin = transform.localPosition;
			RaycastHit hitInfo;
			target = GetClickedObject(out hitInfo);
			if (target != null)
			{
			    //origin = target.transform.localPosition;
				//originRotation = target.transform.localRotation;
				//originScale = target.transform.localScale;
				Card card = target.GetComponent<Card>();
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
									i++;
								}
							} else {
								targets [0] = card.gameObject;
								originPositions [0] = targets [0].transform.localPosition;
								originRotations [0] = targets [0].transform.localRotation;
								originScales [0] = targets [0].transform.localScale;

							}
				
							_mouseState = true;
							screenSpace = Camera.main.WorldToScreenPoint (target.transform.position);

							offset = target.transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
							int ii = 0;
							while (targets [ii] != null) {
								offsets[ii] = targets [ii].transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
								ii++;
							}
							Collider col = target.GetComponent<Collider> ();
							carringCard = card;
							if (col != null) {
								col.enabled = false;
							}
						}
				}
			}
		}
			if (_mouseState) {
				if (Input.GetMouseButtonUp (0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended)) {
					_mouseState = false;
					RaycastHit hitInfo;
					target = GetClickedObject (out hitInfo);
					if (target != null && carringCard != null) {
						Pile destPile = target.GetComponentInParent<Pile> ();
						Pile srcPile = carringCard.GetComponentInParent<Pile> ();
				
						Card card = carringCard;
						if (destPile != null && card != null && srcPile != null) {
							bool handled = false;
							// Foundation Piles
							FoundationPile fp = destPile.GetComponent<FoundationPile> ();
							if (fp != null) {
								if (fp.AddCard (card)) {
									srcPile.RemoveCardFromTop ();

									carringCard = null;
								} else {
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
									handled = true;
									Collider col = card.GetComponent<Collider> ();
									if (col != null) {
										col.enabled = true;
									}
								} else if (tp.GetSize () > 0) {
									Card topCard = tp.GetCardFromTop ();
									if (topCard.GetColor () != card.GetColor () &&
										((int)topCard.rank) -1 == ((int)card.rank)) {

										tp.AddPile (srcPile.SplitPileAtCard (card));

										handled = true;
										Collider col = card.GetComponent<Collider> ();
										if (col != null) {
											col.enabled = true;
										}
									}
								}

							}

							if (!handled) {
								ReturnToPreviousPosition (carringCard.gameObject);
								carringCard = null;
							}


						} else {
							ReturnToPreviousPosition (carringCard.gameObject);
							carringCard = null;
						}
				
						target = null;
						for (int i = 0; i < 52; i++) {
							targets [i] = null;
							offsets [i] = new Vector3();
							originPositions[i] = new Vector3();
							originRotations[i] = new Quaternion();
							originScales[i] = new Vector3();
						}

					} else if (carringCard != null) {
						ReturnToPreviousPosition (carringCard.gameObject);
						carringCard = null;
					}

				}
			} else {
				// No Card just a tap
				if (Input.GetMouseButtonUp (0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended)) {
					RaycastHit hitInfo;
					target = GetClickedObject (out hitInfo);
					if (target != null) {
						Deck deck = target.GetComponentInParent<Deck>();
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
			//keep track of the mouse position
			var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);

			//convert the screen mouse position to world point and adjust with offset
			var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;

				if (target != null) {
					//update the position of the object in the world
					target.transform.position = curPosition;
				}
				for (int i = 0; i <52; i++) {
					if (targets[i] != null) {
						var curPosition2 = Camera.main.ScreenToWorldPoint(curScreenSpace) + offsets[i];
						targets [i].transform.position = curPosition2;
					}
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
		//	target.transform.localPosition = origin;
		//	target.transform.localRotation = originRotation;
		//	target.transform.localScale = originScale;
				int ii = 0;
				while (targets [ii] != null) {
					targets [ii].transform.localPosition = originPositions [ii];
					targets [ii].transform.localRotation = originRotations [ii];
					targets [ii].transform.localScale = originScales [ii];
					ii++;
				}
				Collider col = target.GetComponent<Collider> ();
				if (col != null) {
					col.enabled = true;
				}
			}
		
		}
}
}