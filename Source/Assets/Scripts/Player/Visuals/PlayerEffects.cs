using UnityEngine;
using System.Collections;

public class PlayerEffects : MonoBehaviour 
{
	public ParticleEmitter consumption;
	
	// Use this for initialization
	void Awake () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButtonDown("Consume"))
			consumption.emit = true;
		
		if(Input.GetButtonUp("Consume"))
			consumption.emit = false;
	}
}
