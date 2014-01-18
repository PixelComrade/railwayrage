using UnityEngine;
using System.Collections;

public class FirePlayer : Fire 
{
	// This is the players missile firing script
	
	public GameObject playerShield; // This should go into the "Fire" script eventually
	public Camera cam;
	
	public GameObject eventHandler;
	
	private GameInfo info;
	
	// Use this for initialization
	void Awake () 
	{
		if(!eventHandler)
			eventHandler = GameObject.Find("EventHandler");
		
		if(eventHandler)
			info = eventHandler.GetComponent<GameInfo>() as GameInfo;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(missilePrefab && missileSpawn)
		{
			if(info.CheckGameState() == false) // It isn't game over
			{
				if(Input.GetButtonDown("Fire") && info.ViewAmmo() > 0)
				{
					OpenFire();
					info.SubtractAmmo(1);
				}
				
				if(Input.GetButtonDown("Shield") && info.ViewAmmo() > 0)
				{
					CreateShield();
				}
			}
		}
	}
	
	void CreateShield()
	{
		Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
		pos.z = 0.0f;
		//Debug.Log(pos);
		
		// Check that this isn't on top of another shield
		GameObject testShield = FindClosestShield();
		if(testShield != null)
		{
			if((testShield.transform.position - pos).magnitude > playerShield.transform.localScale.x)
			{
				if(playerShield) 
				{
					Instantiate(playerShield, pos, Quaternion.identity);
					info.SubtractAmmo(1);
				}
			}
		}
		else
		{
			if(playerShield) 
			{
				Instantiate(playerShield, pos, Quaternion.identity);
				info.SubtractAmmo(1);
			}
		}
	}
	
	GameObject FindClosestShield()
	{
		GameObject[] shields;
        shields = GameObject.FindGameObjectsWithTag("Player Shield");
		
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = this.transform.position;
		
        foreach(GameObject shield in shields) 
		{
            Vector3 diff = shield.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if(curDistance < distance) 
			{
                closest = shield;
                distance = curDistance;
            }
        }
		
        return closest;
	}
}
