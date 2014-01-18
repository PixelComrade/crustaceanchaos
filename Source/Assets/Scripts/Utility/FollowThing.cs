using UnityEngine;
using System.Collections;

public class FollowThing : MonoBehaviour 
{
	public GameObject source;
	public GameObject target;
	
	public float followSpeed = 5.0f;
	
	private Vector3 targetPosition = Vector3.zero;
	
	private CharacterController control;
	
	// Use this for initialization
	void Awake () 
	{
		if(!source)
			source = this.gameObject;
		
		if(source && !control)
			control = source.GetComponent<CharacterController>() as CharacterController;
	}
	
	// Update is called once per frame
	void Update () 
	{
		targetPosition = target.transform.position - source.transform.position;
		//targetPosition.z = source.transform.position.z;
		targetPosition.z = Vector3.zero.z;
		
		//Debug.DrawRay(source.transform.position, targetPosition * 100.0f);
		
		source.transform.Translate(targetPosition * followSpeed * Time.deltaTime);
		//control.Move(targetPosition * followSpeed * Time.deltaTime);
	}
}
