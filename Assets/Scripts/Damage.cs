using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {
	
	public float damageAmount = 10.0f;
	
	public bool damageOnTrigger = true;
	public bool damageOnCollision = false;
	public bool continuousDamage = false;
	public float continuousTimeBetweenHits = 0;

	public bool destroySelfOnImpact = false;	// variables dealing with exploding on impact (area of effect)
	public float delayBeforeDestroy = 0.0f;
	public GameObject explosionPrefab;

	private float savedTime = 0;

	void OnTriggerEnter(Collider collision)						// Para balas en trigger
	{
		if (damageOnTrigger && collision.gameObject.tag!= "Environment") {
			Debug.Log ("On trigger Enter");
			if (this.tag == "PlayerBullet" && collision.gameObject.tag == "Player")	// Si el jugador se disparó con sus balas lo ignora
				return;
		
			if (collision.gameObject.GetComponent<Health> () != null) {	//Si el objeto tiene el Cmponente de Health, resta el daño
				collision.gameObject.GetComponent<Health> ().ApplyDamage (damageAmount);
		
				if (destroySelfOnImpact) {
					Destroy (gameObject, delayBeforeDestroy);	  // Destruye el objeto 
				}
			
				if (explosionPrefab != null) {
					Instantiate (explosionPrefab, transform.position, transform.rotation);
				}
			}
		}
	}


	void OnCollisionEnter(Collision collision) 						// para cosas que se destruyen en el impacto y no son triggers
	{	
		
		if (damageOnCollision && collision.gameObject.tag!= "Environment") {
			Debug.Log ("On Collision Enter");
			if (this.tag == "PlayerBullet" && collision.gameObject.tag == "Player")	
		
			if (collision.gameObject.GetComponent<Health> () != null) {	// 
				collision.gameObject.GetComponent<Health> ().ApplyDamage (damageAmount);
			
				if (destroySelfOnImpact) {
					Destroy (gameObject, delayBeforeDestroy);	  
				}
			
				if (explosionPrefab != null) {
					Instantiate (explosionPrefab, transform.position, transform.rotation);
				}
			}
		}
	}


	void OnCollisionStay(Collision collision) // para daños que progresan sobre el tiempo
	{
		if (continuousDamage) {
			if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Health> () != null) {
				Debug.Log ("On trigger Stay");	// Cuando colisiona con el jugador
				if (Time.time - savedTime >= continuousTimeBetweenHits) {
					savedTime = Time.time;
					collision.gameObject.GetComponent<Health> ().ApplyDamage (damageAmount);
				}
			}
		}
	}
	
}