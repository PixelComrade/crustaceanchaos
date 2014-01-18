using UnityEngine;
using System.Collections;

public class ArmMovement : MonoBehaviour 
{
	public float moveSpeed = 5.0f;
	
	private Vector3 targetVector = Vector3.zero;
	
	// Use this for initialization
	void Awake () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log(this.name + " has target: " + targetVector);
		/*
		this.rigidbody.velocity = Vector3.zero;
		
		if(targetVector != Vector3.zero)
			this.rigidbody.AddForce(targetVector * moveSpeed * Time.deltaTime);*/
	}
	
	public void Positioning (Vector3 targetPoint)
	{
		//Debug.Log("passing " + targetPoint);
		targetVector = targetPoint - this.transform.position;
	}
}
