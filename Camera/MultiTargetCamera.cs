using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Based on the YouTube tutorial by Brackeys
//Last Accessed on 31st July 2019 at the following url
//https://www.youtube.com/watch?v=aLpixrPvlB8


[RequireComponent(typeof(Camera))]
public class MultiTargetCamera : MonoBehaviour {

	public List<Transform> targets;
	public Vector3 offset;
	public float smoothTime = 0.5f;
	public bool lerpMove;

	public bool followPlayer;
	//3D Zoom
	[Header("3D Zoom")]
	public float minZoom = 40f;
	public float maxZoom = 10f;
	public float zoomLimiter = 50f;

	//2D Zoom
	[Header("2D Zoom")]
	public float minSize = 40f;
	public float maxSize = 10f;
	public float sizeLimiter = 50f;

	private Vector3 velocity;
	private Camera cam;

	void Start(){
		cam = GetComponent<Camera>();
		if (followPlayer)
			AddCameraTarget(GameObject.FindGameObjectWithTag("Player"));
	}

	void Update(){

		if (targets.Count == 0 ){
			return;
		}

		if(lerpMove)		
			LerpMove();
		else		
			Move();
		
		if (cam.orthographic)
			Zoom2D();
		else
			Zoom();
	}

	void Zoom(){
		float newZoom = Mathf.Lerp(maxZoom,minZoom,GetGreatestDistance()/zoomLimiter);
		cam.fieldOfView = Mathf.Lerp(cam.fieldOfView,newZoom,Time.deltaTime);
	}

	//CURRENTLY UNUSED 
	void Zoom2D(){
		// use Size instead of FOV
		float newSize = Mathf.Lerp(maxSize,minSize,GetGreatestDistance()/sizeLimiter);
		cam.orthographicSize = Mathf.Lerp(cam.orthographicSize,newSize,Time.deltaTime);
	}

	void LerpMove()
	{
		Vector3 centerPoint = GetCenterPoint();
		Vector3 newPosition = centerPoint + offset;
		transform.position = Vector3.Lerp(transform.position,newPosition, smoothTime);
	}

	void Move(){
		Vector3 centerPoint = GetCenterPoint();
		Vector3 newPosition = centerPoint + offset;
		transform.position = Vector3.SmoothDamp(transform.position,newPosition, ref velocity, smoothTime);
	}

	float GetGreatestDistance(){
		var bounds = new Bounds(targets[0].position, Vector3.zero);
		for (int i = 0; i< targets.Count; i++){
			bounds.Encapsulate(targets[i].position);
		}
		return bounds.size.x;
	}

	//Get the center point of the target objects using Unity's Bounds class
	Vector3 GetCenterPoint(){
		if (targets.Count == 1){
			return targets[0].position;
		}

		var bounds = new Bounds(targets[0].position, Vector3.zero);
		for (int i = 0; i< targets.Count; i++){
			bounds.Encapsulate(targets[i].position);
		}
		return bounds.center;
	}

	//Adds a GameObj to the list of camera targets
	public void AddCameraTarget(GameObject gameObj)
	{
		targets.Add(gameObj.transform);
	}
}


