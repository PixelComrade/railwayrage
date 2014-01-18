using UnityEngine;
using System.Collections;

public class Aim : MonoBehaviour 
{
	public GameObject player;
	public GameObject crosshair;
	public Camera cam;
	
	public float speed = 3.0f;
	
	// Use this for initialization
	void Awake () 
	{
		if(!player)
			player = GameObject.Find("Player");
		if(!crosshair)
			crosshair = GameObject.Find("Crosshair");
		if(!cam)
			cam = GameObject.Find("Main Camera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
		pos.z = 0.0f;
		//Debug.Log(pos);
		
		Vector3 finalPos = pos - crosshair.transform.position;
		
		crosshair.transform.up = Vector3.Lerp(crosshair.transform.up, finalPos, speed * Time.deltaTime);
		
		// There's a weird bug where sometimes the texture will flip
		// This happens when the crosshair is aimed directly down
		// What happens is the left and right get reversed and opposite side of the cube is displayed
	}
}
