using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour 
{
	public Camera source;

	// Use this for initialization
	void Awake () 
	{
		if(!source)
			source = this.camera;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(camera.orthographicSize > 5 && camera.orthographicSize < 35)
			camera.orthographicSize -= (Input.GetAxis("Mouse ScrollWheel") * 20);
		
		if(camera.orthographicSize <= 5) // cannot increase
			if(Input.GetAxis("Mouse ScrollWheel") < 0.0)
				camera.orthographicSize -= (Input.GetAxis("Mouse ScrollWheel") * 20);
				
		if(camera.orthographicSize >= 35) // cannot decrease
			if(Input.GetAxis("Mouse ScrollWheel") > 0.0)
				camera.orthographicSize -= (Input.GetAxis("Mouse ScrollWheel") * 20);
	}
}
