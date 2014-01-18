using UnityEngine;
using System.Collections;

public class EnemyDeath : Life 
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
			EndMe();
		}
	}
	
	void OnCollisionEnter(Collision collision)
	{
		EndMe();
	}
	
	void EndMe()
	{
		if(explosion) Instantiate(explosion, this.transform.position, Quaternion.identity);
		if(!explosion)
		{
			explosion = this.gameObject.GetComponent<CollisionDeath>().explosion;
			Instantiate(explosion, this.transform.position, Quaternion.identity);
		}
		Destroy(this.gameObject);
	}
}
