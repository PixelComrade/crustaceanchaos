using UnityEngine;
using System.Collections;

public class DeathManagement : MonoBehaviour 
{
	public GameObject deathPoint;
	
	// Use this for initialization
	void Awake () 
	{
		if(!deathPoint)
			deathPoint = GameObject.Find("DeathPoint");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(this.transform.position.y <= deathPoint.transform.position.y)
		{
			// Die due to being too far away from the game bounds
			
			if(this.transform.parent.name == "NPC") // NPCs
			{
				Destroy(this.gameObject.transform.parent.gameObject);
				Destroy(this.gameObject);
			}
			else // Other objects
				Destroy(this.gameObject);
		}
	}
}
