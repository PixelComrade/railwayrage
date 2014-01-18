using UnityEngine;
using System.Collections;

public class ShieldBehaviour : MonoBehaviour 
{
	public GameObject explosion;
	
	public int startingHealth = 1;
	
	private int health = 1;
	
	// Use this for initialization
	void Awake () 
	{
		health = startingHealth;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnCollisionEnter(Collision collision)
	{
		health--;
		
		if(health <= 0)
		{
			ExplodeMe();
		}
	}
	
	public void ExplodeMe()
	{
		if(explosion) Instantiate(explosion, this.transform.position, Quaternion.identity);
		Destroy(this.gameObject);
	}
}
