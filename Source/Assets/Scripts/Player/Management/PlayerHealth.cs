using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour 
{
	public GameObject handler;
	
	public GameObject[] healthRepresentation;
	
	public Material normal;
	public Material power;
	
	public int maxHealth = 6;
	
	public ParticleEmitter powerful;
	
	private int healthValue = 2;
	private GameObject character;
	private bool powerfulNow = false;
	
	// Use this for initialization
	void Awake () 
	{
		if(!handler)
			handler = GameObject.Find("Handler");
		
		if(healthRepresentation.Length > 0)
		{
			for(int i = 0; i < healthRepresentation.Length; i++)
				healthRepresentation[i].renderer.enabled = false;
			for(int i = 0; i < healthValue; i++)
				healthRepresentation[i].renderer.enabled = true;
			
			Transform healthParticle;
			ParticleEmitter healthEmitter;
			
			for(int i = 0; i < healthRepresentation.Length; i++)
			{
				healthParticle = healthRepresentation[i].transform.Find("HealthEffect") as Transform;
				healthEmitter = healthParticle.GetComponent<ParticleEmitter>() as ParticleEmitter;
				if(i < healthValue)
					healthEmitter.emit = true;
				else
					healthEmitter.emit = false;
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log("curr hp is " + healthValue + " and max is " + maxHealth);
		if(healthValue >= maxHealth)
		{
			//Debug.Log("changing to power");
			character = this.transform.FindChild("PlayerCharacter").gameObject;
			MeshRenderer appearance = character.GetComponent<MeshRenderer>() as MeshRenderer;
			appearance.material = power;
			if(powerfulNow == false)
			{
				powerful.Emit();
				powerfulNow = true;
			}
		}
		else
		{
			//Debug.Log("changing to normal");
			character = this.transform.FindChild("PlayerCharacter").gameObject;
			MeshRenderer appearance = character.GetComponent<MeshRenderer>() as MeshRenderer;
			appearance.material = normal;
			powerfulNow = false;
		}
		
		if(healthValue <= 0)
			RunEndGame();
	}
	
	protected bool CheckPower ()
	{
		return powerfulNow;
	}
	
	protected int GetHealth ()
	{
		return healthValue;
	}
	
	protected int GetMaxHealth ()
	{
		return maxHealth;
	}
	
	public void SetHealth (int input)
	{
		//if(healthValue < maxHealth && healthValue > 0)
			//healthValue += input;
		
		if(input > 0)
		{
			if(healthValue < maxHealth)
				healthValue += input;
		}
		else if(input < 0)
		{
			if(healthValue > 0)
				healthValue += input;
		}
		
		if(healthRepresentation.Length > 0)
		{
			for(int i = 0; i < healthRepresentation.Length; i++)
				healthRepresentation[i].renderer.enabled = false;
			for(int i = 0; i < healthValue; i++)
				healthRepresentation[i].renderer.enabled = true;
			
			Transform healthParticle;
			ParticleEmitter healthEmitter;
			
			for(int i = 0; i < healthRepresentation.Length; i++)
			{
				healthParticle = healthRepresentation[i].transform.Find("HealthEffect") as Transform;
				healthEmitter = healthParticle.GetComponent<ParticleEmitter>() as ParticleEmitter;
				if(i < healthValue)
					healthEmitter.emit = true;
				else
					healthEmitter.emit = false;
			}
		}
		
		//Debug.Log("setting health by " + input + " and now at " + healthValue);
	}
	
	void RunEndGame ()
	{
		if(!handler)
			handler = GameObject.Find("Handler");
		
		Progression progressScript = handler.GetComponent<Progression>() as Progression;
		progressScript.EndGame(4); // Lose the game
	}
}
