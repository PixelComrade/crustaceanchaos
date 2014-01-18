using UnityEngine;
using System.Collections;

public class Progression : GameConditions 
{
	public Camera menuCamera;
	public Camera gameCamera;
	
	public GameObject player;
	public GameObject collectionPoint;
	
	public float distanceThreshold = 5.0f;

	private winConditions conditionMet = winConditions.Incomplete;
	
	/*
	 * Win conditions
	 * 
	 * Consume all survivors (Consume)
	 * Collect all home blocks (Collect)
	 * Knock all home blocks off islands (Destroy)
	 */
	
	private GameObject[] allHomes;
	private GameObject[] survivors;
	private bool onStartScreen = false; // We start in the game
	
	// Use this for initialization
	void Awake () 
	{
		gameCamera.gameObject.active = false;
		menuCamera.gameObject.active = true;
		
		if(!player)
			player = GameObject.Find("Player");
		
		if(!collectionPoint)
			collectionPoint = GameObject.Find("CollectionPoint");
		
		player.active = false;
		
		StartGame();
		
		GatherData();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(onStartScreen == false) // Currently in game
		{
			GatherData();
			
			if(survivors.Length <= 0)
			{
				// All survivors killed or consumed
				EndGame(1);
			}
			
			if(allHomes.Length > 0)
			{
				if(collectionPoint)
				{
					// All homes are within range of the collection point
					int counter = 0;
					foreach(GameObject home in allHomes)
					{
						if(Vector3.Magnitude(home.transform.position - collectionPoint.transform.position) <= distanceThreshold)
							counter++;
					}
					if(counter >= allHomes.Length)
						EndGame(2);
				}
			}
			else
			{
				// All homes destroyed via thrown off island
				EndGame(3);
			}
		}
		else // Currently in menu
		{

		}
	}
	
	void SwapCameras (int input)
	{
		// 1 - menu, 2 - game
		if(input == 1)
		{
			gameCamera.gameObject.active = false;
			menuCamera.gameObject.active = true;
		}
		else
		{
			menuCamera.gameObject.active = false;
			gameCamera.gameObject.active = true;
		}	
	}
	
	void GatherData ()
	{
		allHomes = GameObject.FindGameObjectsWithTag("Home");
		survivors = GameObject.FindGameObjectsWithTag("Survivor");
	}
	
	public void StartGame ()
	{
		MenuHUD menuScript = GetComponent<MenuHUD>() as MenuHUD;
		menuScript.ResetData();
		conditionMet = winConditions.Incomplete;
		onStartScreen = false;
		player.active = true;
		SwapCameras(2);
	}
	
	public void EndGame (int condition)
	{
		/*
		 * Conditions
		 * 
		 * 1 - Consume
		 * 2 - Collect
		 * 3 - Destroy
		 * 4 - Lose
		 */
		
		if(condition == 1)
		{
			conditionMet = winConditions.Consume;
			onStartScreen = true;
		}
		else if(condition == 2)
		{
			conditionMet = winConditions.Collect;
			onStartScreen = true;
		}
		else if(condition == 3)
		{
			conditionMet = winConditions.Destroy;
			onStartScreen = true;
		}
		else
		{
			conditionMet = winConditions.Lose;
			onStartScreen = true;
		}
		
		onStartScreen = true;
		player.active = false;
		SwapCameras(1);
	}
	
	public bool GrabProgressData ()
	{
		return onStartScreen;
	}
	
	public winConditions GrabConditionData ()
	{
		return conditionMet;
	}
}
