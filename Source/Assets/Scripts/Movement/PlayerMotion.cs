using UnityEngine;
using System.Collections;

public class PlayerMotion : Motion 
{
	private float Vert;
	private float Hori;
	
	// Use this for initialization
	void Awake () 
	{
		if(!source)
			source = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.DrawRay(source.transform.position, Vector3.right * 100.0f, Color.red);
		//Debug.DrawRay(source.transform.position, Vector3.up * 100.0f, Color.blue);
		
		if(!Input.anyKey)
			ResetSpeed(false);
		else
			ResetSpeed(true);
		
		ManageSpeed();
		
		Vert = Input.GetAxis("Vertical");
		Hori = Input.GetAxis("Horizontal");
		Move(Vert, Hori);
		
		if(Input.GetButton("Reset"))
			ResetSpeed(false);
		if(Input.GetButtonUp("Reset"))
			ResetSpeed(true);
	}
}
