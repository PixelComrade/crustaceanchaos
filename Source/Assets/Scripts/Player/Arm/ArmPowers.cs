using UnityEngine;
using System.Collections;

public class ArmPowers : MonoBehaviour 
{
	public GameObject source;
	
	public ParticleEmitter blackHole;
	public ParticleEmitter explosion;
	
	public float influenceRange = 5.0f;
	public float power = 50.0f;
	
	private Collider[] objectsInRange;
	
	// Use this for initialization
	void Awake () 
	{
		if(!source)
			source = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButton(0)) // Left click
			BlackHole();
		
		if(Input.GetMouseButton(1)) // Right click
			ReverseBlackHole();
		
		if(Input.GetMouseButtonDown(0))
			blackHole.emit = true;
		
		if(Input.GetMouseButtonDown(1))
			explosion.Emit();
		
		if(Input.GetMouseButtonUp(0))
			blackHole.emit = false;
	}
	
	void BlackHole ()
	{
		objectsInRange = Physics.OverlapSphere(source.transform.position, influenceRange);
		
		//Debug.Log("grabbing");
		
		foreach(Collider hit in objectsInRange)
		{
			if(!hit)
			{}
			
			if(hit.rigidbody)
			{
				if(hit.gameObject.tag != "IslandFree")
				{
					Vector3 between = source.transform.position - hit.transform.position;
					hit.rigidbody.AddForce(between * power * 10.0f * Time.deltaTime);
				}
			}
		}
	}
	
	void ReverseBlackHole ()
	{
		objectsInRange = Physics.OverlapSphere(source.transform.position, influenceRange);
		
		//Debug.Log("exploding");
		
		foreach(Collider hit in objectsInRange)
		{
			if(!hit)
			{}
			
			if(hit.rigidbody)
			{
				if(hit.gameObject.tag != "IslandFree")
				{
					hit.rigidbody.AddExplosionForce(power * 2.0f, source.transform.position, influenceRange, 0.0f);			
				}
			}
		}
	}
}
