using UnityEngine;
using System.Collections;

public class TrainDeath : Life 
{
	public GameObject explosion;
	
	// Use this for initialization
	void Awake () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		life -= 1 * Time.deltaTime;
		if(life <= 0)
		{
			if(explosion) Instantiate(explosion, this.transform.position, Quaternion.identity);
			
			// Handle the people
			TrainBehaviour tb = this.gameObject.GetComponent<TrainBehaviour>() as TrainBehaviour;
			tb.DetachPeople();
			
			Destroy(this.gameObject);
		}
	}
}
