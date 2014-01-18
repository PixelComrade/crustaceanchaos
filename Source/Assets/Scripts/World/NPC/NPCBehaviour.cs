using UnityEngine;
using System.Collections;

public class NPCBehaviour : NPCMotion 
{
	public GameObject player;
	public GameObject homePoint;
	
	public float alertThreshold = 15.0f;
	public float homeProximity = 3.0f;
	
	public GameObject acidSpit;
	
	public float acidSpeed = 50.0f;
	public float acidCooldown = 5.0f;
	
	private AIState state = AIState.idle;
	
	private float timeCounter = 5.0f;
	private bool slowDownOnce = false;
	
	// Use this for initialization
	void Awake () 
	{
		if(!source)
			source = this.gameObject;
		
		if(!player)
		{
			player = GameObject.Find("Player");
			player = player.transform.FindChild("PlayerCharacter").gameObject;	
		}
		
		if(!homePoint)
		{
			GameObject[] homes = GameObject.FindGameObjectsWithTag("Home");
			
			if(homes.Length <= 0)
				return;
			
			float counter = 9999.0f;
			GameObject closestHome = homes[0];
			
			foreach(GameObject home in homes)
			{
				Vector3 distance = home.transform.position - source.transform.position;
				if(Vector3.Magnitude(distance) < counter)
				{
					closestHome = home;
					counter = Vector3.Magnitude(distance);
				}
			}
			
			homePoint = closestHome;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log("current state is " + state);
		
		timeCounter += Time.deltaTime;
		
		// TODO - if no homepoint, do something else
		
		Think();
		Move();
		ManageSpeed();
	}
	
	void Think ()
	{
		switch(state)
		{
			case NPCMotion.AIState.idle:
				ActionsIdle();
				break;
				
			case NPCMotion.AIState.alert:
				ActionsAlert();
				break;
				
			case NPCMotion.AIState.scared:
				ActionsScared();
				break;
		}
	}
	
	void ActionsIdle ()
	{
		Vector3 waypoint = source.transform.position;;
		if(homePoint)
		{
			waypoint = homePoint.transform.position;
			waypoint.x += Random.Range(-homeProximity, homeProximity);
			waypoint.y += Random.Range(-homeProximity, homeProximity);
			//waypoint.z += Random.Range(-homeProximity, homeProximity);
		}
		else // No home or destroyed home
		{
			if(slowDownOnce == false)
			{
				//Debug.Log("slowing down " + Vector3.Magnitude(source.rigidbody.velocity) + " vs " + Vector3.Magnitude(Vector3.zero));
				Brakes();
				if(Vector3.Magnitude(source.rigidbody.velocity) <= 2.0f)
				{
					source.rigidbody.drag = 0.05f;
					slowDownOnce = true;
				}
			}
			else
			{
				//Debug.Log("done slowing with velo " + Vector3.Magnitude(source.rigidbody.velocity));
				waypoint = source.transform.position;
				waypoint.x += Random.Range(-homeProximity * 2.0f, homeProximity * 2.0f);
				waypoint.y += Random.Range(-homeProximity * 2.0f, homeProximity * 2.0f);
				//waypoint.z += Random.Range(-homeProximity * 2.0f, homeProximity * 2.0f);
			}
		}
		SetWaypoint(waypoint);
		
		// Conditions for change
		if(Vector3.Magnitude(player.transform.position - source.transform.position) <= alertThreshold)
		{
			state = NPCMotion.AIState.alert;
			if(Input.GetMouseButton(0) || Input.GetMouseButton(1))
				state = NPCMotion.AIState.scared;
		}
	}
	
	void ActionsAlert ()
	{
		Vector3 waypoint = source.transform.position;
		if(homePoint)
		{
			Vector3 protectLine = player.transform.position - homePoint.transform.position;
			Vector3 factor = protectLine * (homeProximity / Vector3.Magnitude(protectLine));
			waypoint = homePoint.transform.position + factor;
		}
		else
		{
			if(slowDownOnce == false)
			{
				//Debug.Log("slowing down " + Vector3.Magnitude(source.rigidbody.velocity) + " vs " + Vector3.Magnitude(Vector3.zero));
				Brakes();
				if(Vector3.Magnitude(source.rigidbody.velocity) <= 2.0f)
				{
					source.rigidbody.drag = 0.05f;
					slowDownOnce = true;
				}
			}
			else
			{
				//Debug.Log("done slowing with velo " + Vector3.Magnitude(source.rigidbody.velocity));
				waypoint = source.transform.position;
				waypoint.x += Random.Range(-homeProximity * 2.0f, homeProximity * 2.0f);
				waypoint.y += Random.Range(-homeProximity * 2.0f, homeProximity * 2.0f);
				//waypoint.z += Random.Range(-homeProximity * 2.0f, homeProximity * 2.0f);
			}
		}
		SetWaypoint(waypoint);
		
		// Acid attack vs player
		if(timeCounter >= acidCooldown)
		{
			Vector3 towardsPlayer = player.transform.position - source.transform.position;
			Ray targetting = new Ray(source.transform.position, towardsPlayer);
			Vector3 attackPoint = targetting.GetPoint(Vector3.Magnitude(towardsPlayer) / 10);
			
			RaycastHit hit;
			if(Physics.Raycast(source.transform.position, towardsPlayer, out hit))
			{
				if(hit.transform.root.name == "Player")
				{
					GameObject attack = Instantiate(acidSpit, attackPoint, Quaternion.identity) as GameObject;
					attack.transform.LookAt(player.transform.position);
					attack.rigidbody.AddForce(attack.transform.forward * acidSpeed * 5000.0f * Time.deltaTime);
				}
			}
			timeCounter = 0.0f;
		}
		
		// Conditions for change
		ArmControls playerArmScript = player.transform.parent.gameObject.GetComponent<ArmControls>() as ArmControls;
		//if(Vector3.Magnitude(player.transform.position - source.transform.position) <= alertThreshold)
		if(Vector3.Magnitude(player.transform.position - source.transform.position) <= playerArmScript.checkArmDistance())
		{
			if(Input.GetMouseButton(0) || Input.GetMouseButton(1))
				state = NPCMotion.AIState.scared;
		}
		else if(Vector3.Magnitude(player.transform.position - source.transform.position) > alertThreshold)
			state = NPCMotion.AIState.idle;
	}
	
	void ActionsScared ()
	{
		// TODO - need to correct the targets
		
		//Vector3 runAway = Vector3.Reflect((player.transform.position - source.transform.position), Vector3.up);
		//Vector3 runAway = Vector3.Reflect((player.transform.position), Vector3.up);
		Vector3 runAway = source.transform.position - player.transform.position;
		Vector3 runAwayPoint = runAway / 10;
		runAway = source.transform.position + (runAwayPoint * 10);
		SetWaypoint(runAway);
		
		// Conditions for change
		if(Vector3.Magnitude(player.transform.position - source.transform.position) >= alertThreshold * 1.5f)
			state = NPCMotion.AIState.idle;
	}
}
