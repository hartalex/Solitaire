using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Solitaire;

public class Explosion : MonoBehaviour {

	public float radius = 5.0F;
	public float power = 300.0F;
	public bool isExploded = false;
	public GameState gamestate;

	void Update()
	{
		if (gamestate.initialized && !isExploded) {
			isExploded = true;
			Explode ();
		}
	}

	void Explode() {
		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere (explosionPos, radius);
		foreach (Collider hit in colliders) {
			Rigidbody rb = hit.GetComponent<Rigidbody> ();
			Card card = hit.GetComponent<Card> ();
			if (card != null) {
				card.isMoving = true;
			}
			if (rb != null) {
				hit.transform.parent = null;
				rb.freezeRotation = false;
				rb.AddExplosionForce (power, explosionPos, radius, 3.0F);
			}
		}
	}
}
