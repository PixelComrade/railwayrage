using UnityEngine;
using System.Collections;

public class MissileBehaviour : MonoBehaviour 
{
	// This is the base class for all missile objects
	
	public GameObject eventHandler;
	
	public float expRadius = 5.0F;
    public float expPower = 1000.0F;
	
	public float startingFuel = 5.0f;
	public float fuelForce = 300.0f;
	
	public GameObject explosion;
	
	protected float fuel;
	
	protected GameInfo info;
	
	// Update is called once per frame
	void Update () 
	{
		if(this.renderer.isVisible == false)
			Destroy(this.gameObject);
	}
	
	protected void InitBehaviour()
	{
		if(!eventHandler)
			eventHandler = GameObject.Find("EventHandler");
		
		if(eventHandler)
			info = eventHandler.GetComponent<GameInfo>() as GameInfo;
		
		fuel = startingFuel;
	}
	
	protected void GeneralMovement()
	{
		if(fuel > 0)
		{
			this.rigidbody.AddForce(this.transform.up * fuel * fuelForce); // Needs work
			fuel--;
		}
	}
	
	protected void HitTrain(GameObject t, Vector3 explosionPos, float expPower, float expRadius)
	{
		TrainBehaviour tb = t.gameObject.GetComponent<TrainBehaviour>() as TrainBehaviour;
		
		if(tb)
		{
			if(tb.CheckHit() == false)
			{
				tb.SetHit(true);
				t.gameObject.GetComponent<TrainBehaviour>().GeneralCollisionEvents();
                t.rigidbody.AddExplosionForce(expPower, explosionPos, expRadius, 0.5f);
				
				if(this.gameObject.layer == 9) // Only add a score if this is a friendly missile
				{
					info.AddScore(info.rewardTrain);
					info.AddAmmo(info.restockTrain);
				}
			}
		}
	}
	
	protected void HitPerson(GameObject p)
	{
		//EnemyDeath ed = p.AddComponent<EnemyDeath>() as EnemyDeath;
		//ed.life = 10.0f;
	}
	
	protected void CreateParticles(Vector3 explosionPos)
	{
		GameObject exp = Instantiate(explosion, explosionPos, this.transform.rotation) as GameObject;
		exp.rigidbody.velocity = this.rigidbody.velocity;
		//ParticleSystem particleSystem = exp.GetComponentInChildren<ParticleSystem>() as ParticleSystem;
	}
}
