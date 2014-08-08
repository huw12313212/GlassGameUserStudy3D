using UnityEngine;
using System.Collections;

public class PlayerControlller : MonoBehaviour {
    Animator animator;

	void Start () {
        animator = GetComponent<Animator>();
		initPosition = transform.position;
	}

	public delegate void AnimateDone();
	public AnimateDone _animateDone;

	public Vector3 initPosition;



	void Update () {

		if (Input.GetKeyDown (KeyCode.A))
						Attack (null);

		if (moving) 
		{
			gameObject.transform.position+= movingSpeed*gameObject.transform.forward*Time.deltaTime;
		}

    }

	public bool moving = false;
	public float movingSpeed = 1;

	public void Attack(AnimateDone animateDone){

		CancelInvoke ();
		_animateDone = animateDone;
		animator.CrossFade ("idle", 0f);
		
		gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
		Invoke ("CallAttack", 1f);
	
	}

	public void CallAttack()
	{
		animator.CrossFade ("attack01", 0.2f);
		Invoke ("Done",2);
	}

	public void Move(AnimateDone animateDone)
	{
		CancelInvoke ();
		_animateDone = animateDone;
		animator.CrossFade ("walk", 0.2f);
		moving = true;
		gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,90,0));
		Invoke ("TurnBack",1);

	}

	public void TurnBack()
	{
		gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,-90,0));
		Invoke ("TurnLeft",1);
	}

	public void TurnLeft()
	{
		gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
		Invoke ("TurnRight",1);
	}

	public void TurnRight()
	{
		gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
		Invoke ("Done",2);

		Invoke ("Idle",1);
		//animator.CrossFade ("walk", 0);
	}

	public void Idle()
	{
		moving = false;
		animator.CrossFade ("idle", 0);
		gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,-90,0));
	}


	public void Duck(AnimateDone animateDone)
	{
		CancelInvoke ();
		_animateDone = animateDone;

		transform.rotation = Quaternion.Euler(new Vector3(0,90,0));
		animator.CrossFade ("walk", 0);
		Invoke("Duck",1);
		Invoke("Done",3);
	}

	public void Done()
	{
		if (_animateDone != null)
		{
			_animateDone();
		}
		Debug.Log ("done");
		moving = false;
		gameObject.transform.position = initPosition;
	}

	public void Duck()
	{
		transform.rotation = Quaternion.Euler(new Vector3(0,90,0));
		animator.CrossFade("duck", 0.2f);
	}



}
