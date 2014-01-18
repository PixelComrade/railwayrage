using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
	public Texture2D titleTex;
	public Texture2D infoTex1;
	public Texture2D infoTex2;
	
	public Texture2D playButtonTex;
	public Texture2D helpButtonTex;
	public Texture2D exitButtonTex;
	
	private bool infoShown = false;
	
	private float Xsize = 0.25f;
	private float Ysize = 0.1f;
	
	// Use this for initialization
	void Awake () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void OnGUI()
	{
		// Title
		GUI.DrawTexture(new Rect(
			Screen.width / 2 - (Screen.width * Xsize), Screen.height * 0.01f, 
			Screen.width * Xsize * 2, Screen.height * Ysize), 
		titleTex, ScaleMode.StretchToFill);
		
		// Instructions on how to play
		if(infoShown == true)
		{
			GUI.DrawTexture(new Rect(
				Screen.width / 2 - (Screen.width * Xsize), Screen.height * 0.06f + Screen.height * Ysize * 3, 
				Screen.width * Xsize * 2, Screen.height * Ysize), 
			infoTex1, ScaleMode.StretchToFill);
			
			GUI.DrawTexture(new Rect(
				Screen.width / 2 - (Screen.width * Xsize), Screen.height * 0.06f + Screen.height * Ysize * 4, 
				Screen.width * Xsize * 2, Screen.height * Ysize), 
			infoTex2, ScaleMode.StretchToFill);
		}
		
		// Buttons, left to right
		if(GUI.Button(new Rect(
			Screen.width * 0.02f, Screen.height - (Screen.height * 0.06f + Screen.height * Ysize), 
			Screen.width * Xsize, Screen.height * Ysize), 
		exitButtonTex))
		{
			Application.Quit();
		}
		
		if(GUI.Button(new Rect(
			Screen.width / 2 - (Screen.width * Xsize) / 2, Screen.height - (Screen.height * 0.06f + Screen.height * Ysize), 
			Screen.width * Xsize, Screen.height * Ysize), 
		helpButtonTex))
		{
			infoShown = !infoShown;
		}
		
		if(GUI.Button(new Rect(
			Screen.width - (Screen.width * 0.02f + Screen.width * Xsize), Screen.height - (Screen.height * 0.06f + Screen.height * Ysize), 
			Screen.width * Xsize, Screen.height * Ysize), 
		playButtonTex))
		{
			Application.LoadLevel(1);
		}
	}
}
