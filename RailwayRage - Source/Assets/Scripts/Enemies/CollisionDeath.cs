using UnityEngine;
using System.Collections;

public class CollisionDeath : MonoBehaviour 
{
	// I was going to use this script for something, but things changed
	
	public GameObject parentTrain;
	
	public GameObject explosion;
	
	//private EnemySpawning parentTrainHandler;
	
	// Use this for initialization
	void Awake () 
	{
		//if(parentTrain)
			//parentTrainHandler = parentTrain.gameObject.GetComponent<EnemySpawning>() as EnemySpawning;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Temporary make sure you die functionality here
		if(this.transform.position.y <= -20)
		{
			Destroy(this.gameObject);
		}
	}
}
