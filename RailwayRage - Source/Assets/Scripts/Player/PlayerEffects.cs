using UnityEngine;
using System.Collections;

public class PlayerEffects : MonoBehaviour 
{
	public float rotateSpeed = 5.0f;
	
	// Use this for initialization
	void Awake () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.transform.Rotate(Vector3.forward * -rotateSpeed * Time.deltaTime);
	}
}
