using UnityEngine;
using System.Collections;

public class TrainBehaviour : MonoBehaviour 
{	
	public float acceleration = 4500.0f;
	public float maxSpeed = 12.0f;
	
	public float hitThreshold = 6.0f;
	private bool wasHit = false;
	
	private float actualAcceleration;
	private float actualMaxSpeed;
	
	private EnemySpawning peopleHandler;
	
	// Use this for initialization
	void Awake () 
	{
		peopleHandler = this.gameObject.GetComponent<EnemySpawning>() as EnemySpawning;
		
		actualAcceleration = acceleration;
		actualMaxSpeed = maxSpeed;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	// Fixed update should be used when dealing with rigidbodies
	void FixedUpdate() 
	{
		if(this.rigidbody.velocity.magnitude <= actualMaxSpeed)
			this.rigidbody.AddForce(this.transform.right * actualAcceleration * Time.deltaTime);
		
		//Debug.Log("Velocity " + this.rigidbody.velocity.magnitude);
		
		if(this.transform.position.y <= -20)
		{
			Destroy(this.gameObject);
		}
		else if(this.transform.position.y > 10)
			StopMoving();
		else
			ResumeMoving();
    }
	
	void OnCollisionEnter(Collision collision)
	{
		// Collision with another train car
		/*if(collision.gameObject.layer == 10 && collision.relativeVelocity.magnitude > hitThreshold)
		{
			GeneralCollisionEvents();
		}*/
	}
	
	public void ResumeMoving()
	{
		actualAcceleration = acceleration;
		actualMaxSpeed = maxSpeed;
	}
	
	public void StopMoving()
	{
		actualAcceleration = 0.0f;
		actualMaxSpeed = 0.0f;
	}
	
	public void GeneralCollisionEvents()
	{
		acceleration = 0.0f;
		maxSpeed = 0.0f;
		
		this.rigidbody.constraints = RigidbodyConstraints.None;
		this.rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
		
		HingeJoint[] myHJs = this.gameObject.GetComponents<HingeJoint>();
		FixedJoint[] myFJs = this.gameObject.GetComponents<FixedJoint>();
		foreach(HingeJoint hj in myHJs) Destroy(hj);
		foreach(FixedJoint fj in myFJs) Destroy(fj);
		
		//this.gameObject.AddComponent<TrainDeath>();
		this.gameObject.GetComponent<TrainDeath>().enabled = true;
	}
	
	public void DetachPeople()
	{
		peopleHandler.AddDeathCounter();
		peopleHandler.ClearPeople();
	}
	
	public void SetHit(bool h)
	{
		wasHit = h;
	}
	
	public bool CheckHit()
	{
		return wasHit;
	}
}
