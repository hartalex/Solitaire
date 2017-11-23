using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Solitaire {
public class DragNDrop : MonoBehaviour
{
	private bool _mouseState;
	private GameObject target;
	public Vector3 screenSpace;
	public Vector3 offset;
	public Vector3 origin;
	private Quaternion originRotation;
	private Vector3 originScale;
	private Card carringCard;
		public Pile stockPile;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{


		// Debug.Log(_mouseState);
			if (Input.GetMouseButtonDown(0)|| (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began))
		{
			origin = transform.localPosition;
			RaycastHit hitInfo;
			target = GetClickedObject(out hitInfo);
			if (target != null)
			{
			    origin = target.transform.localPosition;
				originRotation = target.transform.localRotation;
				originScale = target.transform.localScale;
				Card card = target.GetComponent<Card>();
				if (card != null)
				{
						if (card.facingUp) { // only pickup face up cards
				
							_mouseState = true;
							screenSpace = Camera.main.WorldToScreenPoint (target.transform.position);

							offset = target.transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
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
						Pile destPile = target.GetComponent<Pile> ();
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
									srcPile.RemoveCardFromTop ();
									tp.AddCardToTop (card);
									handled = true;
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
			target.transform.localPosition = origin;
			target.transform.localRotation = originRotation;
			target.transform.localScale = originScale;

				Collider col = target.GetComponent<Collider> ();
				if (col != null) {
					col.enabled = true;
				}
			}
		
		}
}
}