using UnityEngine;
using System.Collections;

public class Navigation : MonoBehaviour 
{
	public GameObject player;
	public GameObject arrow1;
	public GameObject arrow2;
	
	public float arrowDistance = 10.0f;
	public float minThreshold = 40.0f;
	
	private GameObject[] allIslands;
	private int islandCounter = 0;
	
	private float shortestDist = 99999.9f;
	private int counter1 = -1;
	private int counter2 = -1;
	
	private bool displayArrows = false;
	
	// Use this for initialization
	void Awake () 
	{
		if(!player)
			player = this.gameObject;
		
		allIslands = GameObject.FindGameObjectsWithTag("IslandFree");
		islandCounter = allIslands.Length;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(islandCounter != allIslands.Length)
			FixInconsistency();
		
		FindClosestIslands();
		DoWeShowArrows();
		
		if(displayArrows == true)
		{
			arrow1.active = true;
			ObserveNavigation();
		}
		else
		{
			arrow1.active = false;
		}
	}
	
	void ObserveNavigation ()
	{
		Vector3 arrow1Pos = Vector3.zero;
		Vector3 arrow2Pos = Vector3.zero;
		
		Vector3 between1 = allIslands[counter1].transform.position - player.transform.position;
		Vector3 between2 = allIslands[counter2].transform.position - player.transform.position;
		
		//arrow1Pos = (arrowDistance / Vector3.Magnitude(between1)) * between1;
		//arrow2Pos = (arrowDistance / Vector3.Magnitude(between2)) * between2;
		
		arrow1Pos = between1 * (1 / (Vector3.Magnitude(between1) / arrowDistance));
		arrow2Pos = between2 * (1 / (Vector3.Magnitude(between2) / arrowDistance));
		
		arrow1.transform.position = player.transform.position;
		arrow2.transform.position = player.transform.position;
		
		arrow1.transform.position = arrow1Pos;
		arrow1.transform.up = between1;
		arrow2.transform.position = arrow2Pos;
		arrow1.transform.up = between2;
	}
	
	void DoWeShowArrows ()
	{
		if(Vector3.Magnitude(allIslands[counter1].transform.position - player.transform.position) >= minThreshold)
			displayArrows = true;
		else
			displayArrows = false;
	}
	
	void FindClosestIslands ()
	{
		shortestDist = 99999.9f;
		counter1 = -1;
		counter2 = -1;
		for(int i = 0; i < allIslands.Length; i++)
		{
			if(Vector3.Magnitude(allIslands[i].transform.position - player.transform.position) <= shortestDist)
				counter1 = i;
		}
		shortestDist = 99999.9f;
		for(int i = 0; i < allIslands.Length; i++)
		{
			if(Vector3.Magnitude(allIslands[i].transform.position - player.transform.position) <= shortestDist)
			{
				if(counter1 != i)
					counter2 = i;
			}
		}
	}
	
	void FixInconsistency ()
	{
		allIslands = GameObject.FindGameObjectsWithTag("IslandFree");
		islandCounter = allIslands.Length;
	}
}
