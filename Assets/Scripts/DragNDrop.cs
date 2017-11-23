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
					_mouseState = true;
					screenSpace = Camera.main.WorldToScreenPoint(target.transform.position);
					offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
					Collider col = target.GetComponent<Collider>();
					carringCard = card;
					if (col != null)
					{
						col.enabled = false;
					}
				}
			}
		}
			if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended))
		{
			_mouseState = false;
			RaycastHit hitInfo;
			target = GetClickedObject(out hitInfo);
				if (target != null) {
					Pile destPile = target.GetComponent<Pile> ();
					Pile srcPile = target.GetComponentInParent<Pile> ();
				
					Card card = GetComponent<Card> ();
					if (destPile != null && card != null) {
						FoundationPile fp = destPile.GetComponent<FoundationPile> ();
						if (fp != null) {
							if (fp.AddCard (card)) {
								if (srcPile != null) {
									srcPile.RemoveCardFromTop ();
								}
								carringCard = null;
							} else {
								ReturnToPreviousPosition (target);
							}
						}

						// TableauPiles
					} else {
						ReturnToPreviousPosition (target);
					}
				
					target = null;
				} else if (carringCard != null) {
					ReturnToPreviousPosition (carringCard.gameObject);
					carringCard = null;
				}
			
			Collider col = GetComponent<Collider>();
			if (col != null)
			{
				col.enabled = true;
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
			target.transform.localPosition = origin;
			target.transform.localRotation = originRotation;
			target.transform.localScale = originScale;
		
		}
}
}