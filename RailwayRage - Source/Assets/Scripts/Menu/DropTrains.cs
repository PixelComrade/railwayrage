using UnityEngine;
using System.Collections;

public class DropTrains : MonoBehaviour 
{
	public GameObject train;
	
	public float freq = 1.0f;
	
	private float counter;
	
	// Use this for initialization
	void Awake () 
	{
		counter = freq;
	}
	
	// Update is called once per frame
	void Update () 
	{
		counter -= 1 * Time.deltaTime;
		
		if(counter <= 0)
		{
			counter = freq;
			
			if(train)
			{
				Vector3 pos = new Vector3(
					this.transform.position.x + Random.Range(-10.0f, 10.0f), 
					this.transform.position.y,
					this.transform.position.z);
				GameObject t = Instantiate(train, pos, Quaternion.identity) as GameObject;
				Destroy(t.GetComponent<TrainBehaviour>());
				Destroy(t.GetComponent<EnemySpawning>());
				
				t.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
				t.GetComponent<TrainDeath>().enabled = true;
				t.GetComponent<TrainDeath>().life = 10.0f;
				
			}
		}
	}
}
