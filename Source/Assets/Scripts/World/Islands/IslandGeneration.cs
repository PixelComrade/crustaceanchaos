using UnityEngine;
using System.Collections;

public class IslandGeneration : MonoBehaviour 
{
	public GameObject[] islandPrefabs;
	public float minYPos = -30.0f;
	public float distVarience = 20.0f;
	public int population = 30;
	
	// Use this for initialization
	void Awake () 
	{
		if(islandPrefabs.Length > 0)
		{
			for(int i = 0; i < population; i++)
			{
				Vector3 spawnPos = this.transform.position;
				spawnPos.x += Random.Range(-distVarience, distVarience);
				if(minYPos < distVarience)
					spawnPos.y += Random.Range(minYPos, distVarience);
				else
					spawnPos.y += Random.Range(-minYPos, distVarience);
				//spawnPos.z += Random.Range(-distVarience, distVarience);
				spawnPos.z = Vector3.zero.z;
				
				int selectedIsland = (int)Random.Range(0, islandPrefabs.Length);
				GameObject spawnIsland = Instantiate(islandPrefabs[selectedIsland], spawnPos, Quaternion.identity) as GameObject;
				
				// TODO - some way to keep a min distance between islands
				
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
