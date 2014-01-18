using UnityEngine;
using System.Collections;

public class Attachment : MonoBehaviour 
{
	public float breakThreshold = 50.0f;
	public float twistThreshold = 50.0f;
	public float cooldown = 2.0f;
	
	private float counter = 0.0f;
	private bool canAttach = true;
	
	// Use this for initialization
	void Awake () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		counter += Time.deltaTime;
		
		if(canAttach == false && counter >= cooldown)
		{
			canAttach = true;
			counter = 0.0f;
		}
	}
	
	void OnCollisionEnter (Collision collision)
	{
		if(canAttach == true)
		{
			if(collision.collider.tag == "IslandFree" || collision.collider.transform.parent.tag == "IslandFree")
			{
				FixedJoint attachedJoint = this.gameObject.AddComponent<FixedJoint>() as FixedJoint;
				if(collision.gameObject.rigidbody)
				{
					attachedJoint.connectedBody = collision.gameObject.rigidbody;
					attachedJoint.breakForce = breakThreshold;
					attachedJoint.breakTorque = twistThreshold;
					canAttach = false;
				}
				else
					Destroy(attachedJoint);
			}
			else
			{
				
			}
		}
	}
	
	void OnJointBreak(float breakForce) 
	{
		Debug.Log("breaking");
		
        Collider[] objectsInRange = Physics.OverlapSphere(this.transform.position, 2.0f);
		Vector3[] allVectors = new Vector3[objectsInRange.Length];
		int objCount = 0;
		
		foreach(Collider hit in objectsInRange)
		{
			allVectors[objCount] = this.transform.position - hit.transform.position;
			objCount++;	
		}
		
		Vector3 finalResult = Vector3.zero;
		for(int i = 0; i < allVectors.Length; i++)
		{
			finalResult += allVectors[i];
		}
		
		this.rigidbody.velocity = Vector3.Normalize(finalResult);
    }
	
	void CalculateAttachment ()
	{
		// For correct rotation

	}
	
	void ExertForce ()
	{
		// Act as island engine
		
	}
}
