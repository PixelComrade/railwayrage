using UnityEngine;
using System.Collections;

public class Spawning : MonoBehaviour 
{
	// This script will handle the spawning of the train
	
	public GameObject trainPrefab;
	public GameObject spawnLocation;
	
	public Texture2D trainTex;
	public Texture2D trainBarBackTex;
	public Texture2D trainBarFrontTex;
	
	public float spawnTimer = 10.0f;
	public int numLimit = 4; // Extra cars attached to the lead car
	
	private float counter = 0.0f;
	
	private int numCars = 1; // Starting cars (in addition to the one in front)
	
	private GameInfo info;
	
	private float texX = 0.3f;
	private float texY = 0.04f;
	private float texXpos = 0.01f;
	private float texYpos = 0.01f;
	
	// Use this for initialization
	void Awake () 
	{
		info = this.gameObject.GetComponent<GameInfo>() as GameInfo;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(info.CheckGameState() != true)
		{
			counter += 1 * Time.deltaTime;
			
			if(counter >= spawnTimer)
			{
				counter = 0.0f;
				
				info.AddAmmo(1); // You get one free shot per train
				
				// Create a new train at the spawn location
				if(trainPrefab && spawnLocation)
				{
					GameObject train = 
						Instantiate(trainPrefab, spawnLocation.transform.position, spawnLocation.transform.rotation) 
							as GameObject;
					
					if(numCars > 0)
					{
						GameObject prevCar = train;
						GameObject currCar;
						for(int i = 0; i < numCars; i++)
						{
							Vector3 newSpawn = prevCar.transform.position;
							newSpawn.x -= 5.5f;
							currCar = 
								Instantiate(trainPrefab, newSpawn, prevCar.transform.rotation) 
									as GameObject;
							
							HingeJoint hj = prevCar.AddComponent<HingeJoint>() as HingeJoint;
							hj.connectedBody = currCar.rigidbody;
							hj.anchor = Vector3.zero;
							Vector3 correct = new Vector3(-1, 0, 0);
							hj.axis = correct;
							
							prevCar = currCar;
						}
					}
					
					if(numCars < numLimit)
						numCars++;
				}
			}
		}
	}
	
	void OnGUI()
	{
		if(info.CheckGameState() != true)
		{
			//GUI.color = Color.black;
			//GUI.Label(new Rect(5, 5, 100, 100), "Time left: " + (spawnTimer - counter));
			
			GUI.DrawTexture(new Rect(
				Screen.width * texXpos, Screen.height * texYpos, 
				Screen.width * texX, Screen.height * texY), 
				trainTex, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect(
				Screen.width * texXpos, (Screen.height * texYpos * 2) + (Screen.height * texY), 
				Screen.width * texX, Screen.height * texY), 
				trainBarBackTex, ScaleMode.StretchToFill);
			
			// Draw the front bar, which moves as a progress bar
			float percentage = counter / spawnTimer;
			
			GUI.DrawTexture(new Rect(
				Screen.width * texXpos, (Screen.height * texYpos * 2) + (Screen.height * texY), 
				(Screen.width * texX) * percentage, Screen.height * texY), 
				trainBarFrontTex, ScaleMode.StretchToFill);
			
			GUI.color = Color.black;
			GUI.Label(new Rect(
				Screen.width * texXpos + (Screen.width * texX) / 6, (Screen.height * texYpos * 2) + (Screen.height * texY), 
				Screen.width * texX, Screen.height * texY),
				"Time left: " + (spawnTimer - counter) + " seconds");
		}
	}
	
	public void Reset()
	{
		counter = 0.0f;
	}
}
