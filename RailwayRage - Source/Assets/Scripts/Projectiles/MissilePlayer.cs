using UnityEngine;
using System.Collections;

public class MissilePlayer : MissileBehaviour 
{
	// Use this for initialization
	void Awake () 
	{
		InitBehaviour();
	}
	
	// Fixed update should be used when dealing with rigidbodies
	void FixedUpdate() 
	{		
		this.transform.up = Vector3.Lerp(this.transform.up, this.rigidbody.velocity, Time.deltaTime);
		
		GeneralMovement();
    }
	
	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.layer != 8 && collision.gameObject.layer != 9)
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
					else if(hit.gameObject.layer == 12) // Hit a person
					{
						hit.rigidbody.AddExplosionForce(expPower, explosionPos, expRadius, 0.5f);
							
						info.AddScore(info.rewardPerson);
						info.AddAmmo(info.restockPerson);
						
						HitPerson(hit.gameObject);
					}
					else if(hit.gameObject.layer == 13) // Hit an enemy missile
					{
						hit.rigidbody.AddExplosionForce(expPower, explosionPos, expRadius, 0.5f);
						
						info.AddScore(info.rewardMissile);
						info.AddAmmo(info.restockMissile);
					}
					else if(hit.gameObject.layer == 14) // Hit a friendly shield
					{
						ShieldBehaviour sb = hit.gameObject.GetComponent<ShieldBehaviour>() as ShieldBehaviour;
						sb.ExplodeMe(); // Destroy friendly shield
						info.AddAmmo(1); // Replenish ammo
					}
				}
	        }
			
			Destroy(this.gameObject);
		}
	}
}
