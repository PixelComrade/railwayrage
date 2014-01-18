using UnityEngine;
using System.Collections;

public class EnemySpawning : MonoBehaviour 
{
	// This script should be placed on each train car
	
	public GameObject person;
	public GameObject personExplosion;
	
	public float startChance = 0.5f;
	private float chance = 0.5f;
	
	private ArrayList people;
	
	private FixedJoint[] fj = new FixedJoint[2];
	
	// Use this for initialization
	void Awake () 
	{
		chance = startChance;
		
		people = new ArrayList();
		
		// Not the proper way to init an array, but since it's always a small, fixed, size of 2, this'll be ok
		fj[0] = null;
		fj[1] = null;
		
		if(person)
		{
			Vector3 p1 = new Vector3(
				this.transform.position.x - 0.75f,
				this.transform.position.y + 1.75f,
				0.0f);
			Vector3 p2 = new Vector3(
				this.transform.position.x + 0.75f,
				this.transform.position.y + 1.75f,
				0.0f);
			
			// First person
			if(Random.Range(0.0f, 1.0f) >= chance)
			{
				GameObject p = Instantiate(person, p1, Quaternion.identity) as GameObject;
				
				fj[0] = this.gameObject.AddComponent<FixedJoint>() as FixedJoint;
				fj[0].connectedBody = p.rigidbody;
				
				CollisionDeath cd = p.gameObject.GetComponent<CollisionDeath>() as CollisionDeath;
				cd.parentTrain = this.gameObject;
				
				AddPerson(p);
			}
			
			// Second person
			if(Random.Range(0.0f, 1.0f) >= chance)
			{
				GameObject p = Instantiate(person, p2, Quaternion.identity) as GameObject;
				
				fj[1] = this.gameObject.AddComponent<FixedJoint>() as FixedJoint;
				fj[1].connectedBody = p.rigidbody;
				
				CollisionDeath cd = p.gameObject.GetComponent<CollisionDeath>() as CollisionDeath;
				cd.parentTrain = this.gameObject;
				
				AddPerson(p);
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	public void AddPerson(GameObject p)
	{
		people.Add(p);
	}
	
	public void RemovePerson(GameObject p)
	{
		people.Remove(p);
	}
	
	public void ClearPeople()
	{
		for(int i = people.Count - 1; i >= 0; i--)
		{
			RemovePerson((GameObject)people[i]);
		}
	}
	
	public void AddDeathCounter()
	{
		for(int i = people.Count - 1; i >= 0; i--)
		{
			GameObject p = people[i] as GameObject;
			if(p != null)
			{
				if(personExplosion)
				{
					EnemyDeath ed = p.AddComponent<EnemyDeath>() as EnemyDeath;
					ed.explosion = personExplosion;
					ed.life = 20.0f;
				}
				else
				{
					Life l = p.AddComponent<Life>() as Life;
					l.life = 0.01f;
				}
			}
		}
	}
}
