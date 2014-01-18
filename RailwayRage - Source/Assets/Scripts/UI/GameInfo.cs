using UnityEngine;
using System.Collections;

public class GameInfo : MonoBehaviour 
{
	// This script will handle all health and score counters for the player
	// This script will also handle the main GUI code...
	// (Probably best to move the GUI code into a new script...I'll do that later)
	
	public GameObject player;
	public GameObject playerAim;
	public GameObject playerAimExplosion;
	
	public Texture2D scoreTex;
	public Texture2D healthTex;
	public Texture2D ammoTex;
	
	public float rectSizeX = 80.0f;
	public float rectSizeY = 40.0f;
	
	public int rewardTrain = 20;
	public int rewardPerson = 10;
	public int rewardMissile = 5;
	public int restockTrain = 1;
	public int restockPerson = 0;
	public int restockMissile = 0;
	
	public int startingHealth = 10;
	public int startingAmmo = 15;
	
	private int score = 0;
	private int health = 3;
	private int ammo = 0;
	
	private bool gameOver = false;
	
	private float texX = 0.2f;
	private float texY = 0.04f;
	private float texXpos = 0.01f;
	private float texYpos = 0.01f;
	
	// Use this for initialization
	void Awake () 
	{
		if(!player)
			player = GameObject.Find("Player");
		
		health = startingHealth;
		ammo = startingAmmo;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(health <= 0 && gameOver == false)
		{
			SetGameOver();
		}
	}
	
	void OnGUI()
	{
		// A persistent restart button
		GUI.color = Color.white;
		if(GUI.Button(new Rect(
			Screen.width - Screen.width * 0.02f - rectSizeX, Screen.height * 0.02f, 
			rectSizeX, rectSizeY), "Restart"))
		{
			RestartGame();
		}
		
		GUI.color = Color.black;
		if(gameOver == false) GameGUI();
		else GameOverGUI();
	}
	
	void GameGUI()
	{
		GUI.Label(new Rect(Screen.width - rectSizeX - 5, Screen.height * 0.02f + rectSizeY + 5, rectSizeX, rectSizeY), "Score: " + score);
		GUI.Label(new Rect(Screen.width - rectSizeX - 5, Screen.height * 0.02f + rectSizeY * 2 + 5, rectSizeX, rectSizeY), "Health: " + health);
		GUI.Label(new Rect(Screen.width - rectSizeX - 5, Screen.height * 0.02f + rectSizeY * 3 + 5, rectSizeX, rectSizeY), "Ammo: " + ammo);
	}
	
	void GameOverGUI()
	{
		GUI.Label(new Rect(
			Screen.width / 2 - (rectSizeX / 2), (Screen.height / 2) - (rectSizeY / 2), 
			rectSizeX * 3, rectSizeY), "Game Over!");
		GUI.Label(new Rect(
			(Screen.width / 2) - (rectSizeX / 2), (Screen.height / 2) - (rectSizeY / 2) + rectSizeY + 5, 
			rectSizeX, rectSizeY), "Final Score: " + score);
	}
	
	void SetGameOver()
	{
		if(playerAim)
		{
			if(playerAimExplosion)
				Instantiate(playerAimExplosion, playerAim.transform.position, Quaternion.identity);
			
			playerAim.SetActive(false);
		}
		gameOver = true;
	}
	
	void RestartGame()
	{
		GameObject[] people = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject[] trains = GameObject.FindGameObjectsWithTag("Train");
		GameObject[] missiles = GameObject.FindGameObjectsWithTag("Player Missile");
		GameObject[] shields = GameObject.FindGameObjectsWithTag("Player Shield");
		GameObject[] enemyMissiles = GameObject.FindGameObjectsWithTag("Enemy Missile");
		
		ClearObjects(people);
		ClearObjects(trains);
		ClearObjects(missiles);
		ClearObjects(shields);
		ClearObjects(enemyMissiles);
		
		if(playerAim)
			playerAim.SetActive(true);
		
		this.gameObject.GetComponent<Spawning>().Reset();
		
		gameOver = false;
		score = 0;
		health = startingHealth;
		ammo = startingAmmo;
	}
		
	void ClearObjects(GameObject[] objects)
	{
		foreach(Object o in objects) 
		{
			Destroy(o);
		}
	}
	
	public void AddScore(int s)
	{
		score += s;
	}
	
	public void SubtractScore(int s)
	{
		score -= s;
	}
	
	public int ViewScore()
	{
		return score;
	}
	
	public void AddHealth(int h)
	{
		health += h;
	}
	
	public void SubtractHealth(int h)
	{
		health -= h;
	}
	
	public int ViewHealth()
	{
		return health;
	}
	
	public void AddAmmo(int a)
	{
		ammo += a;
	}
	
	public void SubtractAmmo(int a)
	{
		ammo -= a;
	}
	
	public int ViewAmmo()
	{
		return ammo;
	}
	
	public bool CheckGameState()
	{
		return gameOver;
	}
}
