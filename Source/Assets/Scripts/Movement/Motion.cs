using UnityEngine;
using System.Collections;

public class Motion : MonoBehaviour 
{
	public GameObject source;
	
	public float moveSpeed = 150.0f;
	public float maxSpeed = 15.0f;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	protected void Move (float Vert, float Hori)
	{
		//source.rigidbody.AddForce(source.transform.up * Vert * moveSpeed * Time.deltaTime);
		//source.rigidbody.AddForce(source.transform.forward * Hori * moveSpeed * Time.deltaTime);
		source.rigidbody.AddForce(Vector3.up * Vert * moveSpeed * Time.deltaTime);
		source.rigidbody.AddForce(Vector3.right * Hori * moveSpeed * Time.deltaTime);
	}
	
	protected void ResetSpeed (bool undo)
	{
		//Debug.Log ("resetting");
		source.rigidbody.velocity = Vector3.Lerp(source.rigidbody.velocity, Vector3.zero, Time.deltaTime);
		if(undo == false)
			source.rigidbody.angularDrag = 50.0f;
		else
			source.rigidbody.angularDrag = 2.5f;
	}
	
	protected void ManageSpeed ()
	{
		float magnitude = Vector3.Magnitude(source.rigidbody.velocity);
	    Vector3 velocity = source.rigidbody.velocity;
		
	    if (velocity == Vector3.zero) return;
		
		//Debug.Log("speed is " + magnitude);
		//Debug.Log("vector speed is " + source.rigidbody.velocity);

	    if (magnitude > maxSpeed)
	    {
	        velocity *= (maxSpeed / magnitude);
	        rigidbody.velocity = velocity;
	    }
	}
}
