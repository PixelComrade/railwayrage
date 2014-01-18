using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour 
{
	// Fire the missiles!
	
	public GameObject missilePrefab;
	public GameObject missileSpawn;
	
	protected void OpenFire()
	{
		GameObject missile = 
			Instantiate(missilePrefab, missileSpawn.transform.position, missileSpawn.transform.rotation) as GameObject;
	}
}
