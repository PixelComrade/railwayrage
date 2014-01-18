using UnityEngine;
using System.Collections;

public class MissileEnemy : MissileBehaviour 
{
	public GameObject player;
	public float homingSpeed = 5.0f;
	
	// Use this for initialization
	void Awake () 
	{
		InitBehaviour();
		
		player = GameObject.Find("Player");
		
		fuelForce += Random.Range(-20.0f, 20.0f);
	}
	
	// Fixed update should be used when dealing with rigidbodies
	void FixedUpdate() 
	{
		Vector3 toPlayer = player.transform.position - this.transform.position;
		
		this.transform.up = Vector3.Lerp(this.transform.up, toPlayer, homingSpeed * Time.deltaTime);
			
		GeneralMovement();
    }
	
	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.layer != 13) // Can't collide against other missiles
		{
	        Vector3 explosionPos = this.transform.position;
			
			CreateParticles(explosionPos);
			
	        Collider[] colliders = Physics.OverlapSphere(explosionPos, expRadius);
			
	        foreach(Collider hit in colliders) 
			{	
	            if(hit.rigidbody)
				{
					//Debug.Log("Rigidbody hit: " + hit.gameObject.name);
					
					// Hit a train
					if(hit.gameObject.layer == 10)
					{
						HitTrain(hit.gameObject, explosionPos, expPower, expRadius);
					}
					else
					{
						hit.rigidbody.AddExplosionForce(expPower, explosionPos, expRadius, 0.0f);
						
						if(hit.gameObject.layer == 12) // Hit a person
							HitPerson(hit.gameObject);
					}
				}
				
				if(hit.gameObject.name == "Player")
					info.SubtractHealth(1);
	        }
			
			Destroy(this.gameObject);
		}
	}
}
