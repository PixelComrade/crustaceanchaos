using UnityEngine;
using System.Collections;

public class NPCMotion : MonoBehaviour 
{
	public enum AIState {idle, alert, scared};
	
	public GameObject source;
	
	public float moveSpeed = 5.0f;
	public float maxSpeed = 15.0f;
	public float durability = 30.0f;
	
	private Vector3 waypoint;
	
	// Use this for initialization
	void Awake () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void OnCollisionEnter (Collision collision)
	{
		if(collision.relativeVelocity.magnitude > durability)
		{
			Destroy(this.gameObject.transform.parent.gameObject);
			Destroy(this.gameObject);
		}
	}
	
	protected void SetWaypoint (Vector3 point)
	{
		waypoint = point;
	}
	
	protected void Move ()
	{
		Vector3 target = waypoint - source.transform.position;
		
		//Debug.DrawRay(source.transform.position, target * moveSpeed * Time.deltaTime, Color.black);
		
		source.rigidbody.AddForce(target * moveSpeed * Time.deltaTime);
	}
	
	protected void Brakes ()
	{
		//source.rigidbody.velocity = Vector3.Lerp(source.rigidbody.velocity, Vector3.zero, Time.deltaTime);
		//source.rigidbody.AddForce((-(source.rigidbody.velocity) * 5.0f) * Time.deltaTime);
		source.rigidbody.drag = 5.0f;
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
	        source.rigidbody.velocity = velocity;
	    }
	}
}
