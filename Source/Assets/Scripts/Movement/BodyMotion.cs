using UnityEngine;
using System.Collections;

public class BodyMotion : MonoBehaviour 
{
	public GameObject source;
	public float rotateSpeed = 5.0f;
	
	public enum chosenAxis {up, forward, right};
	public chosenAxis choice;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(choice == chosenAxis.forward)
			this.gameObject.transform.RotateAround(source.transform.position, Vector3.forward, rotateSpeed * Time.deltaTime);
		else if(choice == chosenAxis.right)
			this.gameObject.transform.RotateAround(source.transform.position, Vector3.right, rotateSpeed * Time.deltaTime);
		else
			this.gameObject.transform.RotateAround(source.transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
	}
}
