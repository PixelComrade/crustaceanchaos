using UnityEngine;
using System.Collections;

public class ArmControls : MonoBehaviour 
{
	public GameObject playerCharacter;
	public GameObject[] arms;
	
	public float maxArmDistance = 20.0f;
	
	private PlayerHUD healthScript;

	// Use this for initialization
	void Awake () 
	{
		healthScript = this.GetComponent<PlayerHUD>() as PlayerHUD;
	}
	
	// Update is called once per frame
	void Update () 
	{		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		Vector3 armPoint = ray.GetPoint(Mathf.Abs(Camera.main.transform.position.z));
		
		//Debug.Log(armPoint);
		//Debug.DrawLine(Camera.main.transform.position, armPoint);
		
		//arms[arms.Length - 1].transform.position = armPoint;
		
		Vector3 between = armPoint - playerCharacter.transform.position;
		
		//Debug.DrawLine(playerCharacter.transform.position, armPoint);
		//Debug.DrawRay(playerCharacter.transform.position, between * Vector3.Magnitude(between), Color.red);
		
		//Debug.Log("player character is at " + playerCharacter.transform.position);
		
		Vector3 final;
		if(healthScript.CheckHealth() < healthScript.CheckMaxHealth())
		{
			if(Vector3.Magnitude(between) >= maxArmDistance)
			{
				final  = between * (maxArmDistance / Vector3.Magnitude(between));
			}
			else
				final = between;
		}
		else
			final = between;
		
		for(int i = 0; i < arms.Length; i++)
		{
			//ArmMovement script = arms[i].GetComponent<ArmMovement>() as ArmMovement;
			
			float factor = ((float)(i + 1) / (float)arms.Length);
			
			arms[i].transform.position = playerCharacter.transform.position;
			arms[i].transform.position += final * factor;
			
			//Vector3 posPoint = playerCharacter.transform.position + (between * factor);
			
			//script.Positioning(posPoint);
			
			//Debug.DrawLine(Camera.main.transform.position, posPoint);
		}
	}
	
	public float checkArmDistance ()
	{
		return maxArmDistance;
	}
}
