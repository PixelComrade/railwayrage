using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour 
{
	public float life = 2.0f;
	
	// Use this for initialization
	void Awake () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		life -= 1 * Time.deltaTime;
		if(life <= 0)
			Destroy(this.gameObject);
	}
}
