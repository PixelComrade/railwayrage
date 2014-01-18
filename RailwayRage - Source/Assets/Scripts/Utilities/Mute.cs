using UnityEngine;
using System.Collections;

public class Mute : MonoBehaviour 
{
	public AudioListener al;
	
	public float rectSizeX = 80.0f;
	public float rectSizeY = 40.0f;
	
	// Use this for initialization
	void Awake () 
	{
		if(!al)
			al = this.gameObject.GetComponent<AudioListener>() as AudioListener;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnGUI()
	{
		if(GUI.Button(new Rect(
			Screen.width - Screen.width * 0.06f - rectSizeX * 2, Screen.height * 0.02f, 
			rectSizeX, rectSizeY), "Mute"))
			al.enabled = !al.enabled;
	}
}
