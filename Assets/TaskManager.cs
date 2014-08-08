using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using System.Collections.Generic;



public class TaskManager : MonoBehaviour {

	public float RotationAngleZ = 90;
	public float RotationAngleY = 45;
	public float Delay = 1;
	public float RotationTime = 1;
	Sequence sequence;

	//3D
	public List<GameObject> GameObject3DList;
	public List<GameObject> GameObject2DList;

	public Material skybox;
	public Camera _camera;

	public Transform CubeObject;
	public GameObject Actor;
	public PlayerControlller playerController;

	// Use this for initialization
	void Start () {

	
	}

	// Update is called once per frame
	void Update () {

		if (!animating) {

						if (Input.GetKeyDown (KeyCode.Alpha1)) {
								TaskRotationZ ();
								animating = true;
						}

						if (Input.GetKeyDown (KeyCode.Alpha2)) {
								TaskRotationY ();
								animating = true;
						}

						if (Input.GetKeyDown (KeyCode.Alpha3)) {
								Duck ();
								animating = true;
						}

						if (Input.GetKeyDown (KeyCode.Alpha4)) {
								Move ();
								animating = true;
						}

						if (Input.GetKeyDown (KeyCode.Alpha5)) {
								Attack ();
								animating = true;
						}
				}
	
	}

	bool animating = false;

	public void Attack()
	{
		AvatorSetting();
		playerController.Attack (()=>
		                       {
			ClearGameObject();
		});
	}

	public void Move()
	{
		AvatorSetting();
		playerController.Move (()=>
		                       {
			ClearGameObject();
		});
	}

	public void Duck()
	{
		AvatorSetting();
		playerController.Duck (()=>
		{
			ClearGameObject();
		});
	}

	public void AvatorSetting()
	{
		foreach (GameObject o in GameObject3DList) 
		{
			o.SetActive(true);
		}
		
		_camera.orthographic = false;
		RenderSettings.skybox = skybox;
		Actor.SetActive(true);
	}

	public void TaskRotationZ()
	{
		ClearSequence();

		CubeObject.gameObject.SetActive(true);
	    sequence = new Sequence();
		sequence.Insert(Delay,HOTween.To(CubeObject, RotationTime, new TweenParms().Prop("rotation", new Vector3(0, 0, RotationAngleZ),false)));
		sequence.Insert(RotationTime+Delay*2,HOTween.To(CubeObject, RotationTime*2, new TweenParms().Prop("rotation", new Vector3(0, 0, -RotationAngleZ),false)));
		sequence.Insert(RotationTime*3+Delay*3,HOTween.To(CubeObject, RotationTime, new TweenParms().Prop("rotation", new Vector3(0, 0, 0),false).OnStepComplete(Done)));

		sequence.Play();
	}

	public void TaskRotationY()
	{
		ClearSequence();

		_camera.orthographic = false;

		CubeObject.gameObject.SetActive(true);
		sequence = new Sequence();
		sequence.Insert(Delay,HOTween.To(CubeObject, RotationTime, new TweenParms().Prop("rotation", new Vector3(0, RotationAngleY, 0),false)));
		sequence.Insert(RotationTime+Delay*2,HOTween.To(CubeObject, RotationTime*2, new TweenParms().Prop("rotation", new Vector3(0, -RotationAngleY, 0),false)));
		sequence.Insert(RotationTime*3+Delay*3,HOTween.To(CubeObject, RotationTime, new TweenParms().Prop("rotation", new Vector3(0, 0, 0),false).OnStepComplete(Done)));
		sequence.Play();
	}

	public void Done()
	{
		CancelInvoke ();
		Invoke("ClearGameObject",Delay);
	}

	public void ClearGameObject()
	{
		animating = false;
		//_camera.projectionMatrix = _camera.
		_camera.orthographic = true;

		foreach (GameObject g in GameObject2DList) 
		{
			g.SetActive(false);		
		}

		foreach (GameObject g in GameObject3DList) 
		{
			g.SetActive(false);
		}

		RenderSettings.skybox = null;
		Actor.SetActive(false);
	}

	//public 

	public void ClearSequence()
	{
		ClearGameObject();
		if (sequence != null) 
		{
			sequence.Clear ();
		}
	}
}
