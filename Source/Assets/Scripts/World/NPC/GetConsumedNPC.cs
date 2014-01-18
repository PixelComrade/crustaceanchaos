using UnityEngine;
using System.Collections;

public class GetConsumedNPC : MonoBehaviour 
{
	public GameObject player;
	
	private PlayerHUD healthScript;
	
	// Use this for initialization
	void Awake () 
	{
		if(!player)
			player = GameObject.Find("Player");
		
		healthScript = player.GetComponent<PlayerHUD>() as PlayerHUD;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
			
	void OnCollisionEnter (Collision collision)
	{
		if(collision.collider.transform.root.name == "Player")
		{
			if(Input.GetButton("Consume"))
			{
				// Players eats the creature
				
				healthScript.ModHealth(1);
				
				//Debug.Log("consuming");
				
				Destroy(this.gameObject.transform.parent.gameObject);
				Destroy(this.gameObject);
			}
		}
	}
}
