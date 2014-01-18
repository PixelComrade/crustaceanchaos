using UnityEngine;
using System.Collections;

public class AcidAttack : MonoBehaviour 
{
	public GameObject player;
	public ParticleEmitter effect;
	public float survivalTime = 3.0f;
	
	private PlayerHUD healthScript;
	private float counter = 0.0f;
	
	// Use this for initialization
	void Awake () 
	{
		if(!player)
			player = GameObject.Find("Player");
		
		if(!effect)
			effect = this.transform.FindChild("AcidExplodeEffect").gameObject.GetComponent<ParticleEmitter>() as ParticleEmitter;
		
		healthScript = player.GetComponent<PlayerHUD>() as PlayerHUD;
	}
	
	// Update is called once per frame
	void Update () 
	{
		counter += Time.deltaTime;
		
		//this.transform.LookAt(player.transform.position);
		this.transform.forward = this.rigidbody.velocity;
		
		if(counter >= survivalTime * 20.0f)
			DestroyMe();
	}
	
	void OnCollisionEnter (Collision collision)
	{
		if(collision.collider.transform.root.name == "Player")
		{
			healthScript.ModHealth(-1);
			DestroyMe();
		}
		if(counter >= survivalTime)
			DestroyMe();
	}
	
	void DestroyMe ()
	{
		Debug.Log("need to emit now");
		//effect.emit = true;
		effect.Emit();
		//effect.emit = true;
		Destroy(this.gameObject);
	}
}
