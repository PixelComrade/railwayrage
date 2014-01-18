using UnityEngine;
using System.Collections;

public class FireEnemy : Fire 
{
	// This is the enemies missile firing script
	// Basic AI functions can be placed here for now
	
	public GameObject left;
	public GameObject right;
	
	public float chanceToFire = 0.1f;
	
	private bool fired = false;
	private Material sprite;
	
	private float totalDistance = -1; // Total distance between firing areas
	private float desiredDistance = -1; // Prefered amount of distance travelled before firing

	// Use this for initialization
	void Awake () 
	{
		sprite = (this.GetComponent<MeshRenderer>() as MeshRenderer).material;
		
		left = GameObject.Find("CheckpointLeft");
		right = GameObject.Find("CheckpointRight");
		
		totalDistance = (right.transform.position - left.transform.position).magnitude;
		desiredDistance = Random.Range(0.01f, 0.99f) * totalDistance;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(fired == false && this.renderer.isVisible == true)
		{
			if(this.transform.position.x >= left.transform.position.x + desiredDistance &&
				this.transform.position.x <= right.transform.position.x)
			{
				if(Random.Range(0.0f, 1.0f) < chanceToFire)
					Fire();
			}
			else if(this.transform.position.x > right.transform.position.x)
				Fire();
		}
	}
	
	void Fire()
	{
		sprite.SetTextureOffset("_MainTex", new Vector2(0, 0));
					
		OpenFire();
		fired = true;
	}
}
