using UnityEngine;
using System.Collections;

public class GetConsumed : MonoBehaviour 
{
	public GameObject source;
	public float maxSpeed = 30.0f;
	public float thresholdDistance = 5.0f;
	
	public float multiplier = 1.0f;
	public float sourceVarience = 3.0f;
	
	private Vector3 between = Vector3.zero;
	private Vector3 variedSource = Vector3.zero;
	
	// Use this for initialization
	void Awake () 
	{
		if(!source)
		{
			source = GameObject.Find("Player");
			source = source.transform.FindChild("PlayerCharacter").gameObject;	
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		//ManageSpeed();
		
		//Debug.DrawRay(this.transform.position, between * thresholdDistance, Color.red);
		//Debug.DrawRay(this.transform.position, between * 100.0f);
		
		if(Vector3.Magnitude(between) <= thresholdDistance && Input.GetButton("Consume"))
		{
			variedSource = source.transform.position;
			variedSource = VaryVector(variedSource, sourceVarience, false);
			between = variedSource - this.transform.position;
			
			this.rigidbody.useGravity = false;
			this.rigidbody.AddForce(between * multiplier * Time.deltaTime);
		}
		else
			this.rigidbody.useGravity = true;
	}
	
	Vector3 VaryVector (Vector3 input, float amount, bool modZ)
	{
		input.x += Random.Range(-amount, amount);
		input.y += Random.Range(-amount, amount);
		if(modZ == true)
			input.z += Random.Range(-amount, amount);
		return input;
	}
	
	protected void ManageSpeed ()
	{
		float magnitude = Vector3.Magnitude(this.rigidbody.velocity);
	    Vector3 velocity = this.rigidbody.velocity;
		
	    if (velocity == Vector3.zero) return;
		
		//Debug.Log("speed is " + magnitude);
		//Debug.Log("vector speed is " + source.rigidbody.velocity);

	    if (magnitude > maxSpeed)
	    {
	        velocity *= (maxSpeed / magnitude);
	        this.rigidbody.velocity = velocity;
	    }
	}
}
