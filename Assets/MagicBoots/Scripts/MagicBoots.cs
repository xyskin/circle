using UnityEngine;
using System.Collections;

public class MagicBoots : MonoBehaviour {

	public float gravitySpeed; //500
	public LayerMask RayCastLayerMask;	// Level
	public float movementSpeed; //2
	public float rotationSpeedGrounded; //10
	public float rotationSpeedFly; //1
	
		
	private float distForward;
	private float distDown;
	private float distBack;
	
	private float rotationSpeed;
	
	public bool isGrounded;
	
	private bool move;
	private Animation anim;
	
	
	
	void Start(){
		
		//setup character animations
		anim = GetComponentInChildren<Animation>();
		if (anim != null)	{
			anim["run"].speed =0.5f; anim["run"].wrapMode = WrapMode.Loop;
			anim["runback"].speed =0.5f; anim["runback"].wrapMode = WrapMode.Loop;
			anim["runleft"].speed =0.5f; anim["runleft"].wrapMode = WrapMode.Loop;
			anim["runright"].speed =0.5f; anim["runright"].wrapMode = WrapMode.Loop;
			anim["idle"].wrapMode = WrapMode.Loop;		
			anim["jump"].speed =1.5f;
		}
		
		isGrounded = true;
		rotationSpeed = rotationSpeedGrounded;	
		
		
	}
	
	
	
	
	// Update is called once per frame
	void FixedUpdate () {
	    
		//control
		move = false;
		
		if ((Input.GetAxis("Vertical"))>0)					
		{
			//Forward			
			transform.position += transform.forward * movementSpeed * Time.deltaTime;
			move = true;
			
			if (isGrounded && !anim.IsPlaying("run")) anim.CrossFade("run");			
		}
		else if ((Input.GetAxis("Vertical"))<0)
		{			
			//Back			
			transform.position -= transform.forward * movementSpeed * Time.deltaTime;
			move = true;
			if (isGrounded && !anim.IsPlaying("runback")) anim.CrossFade("runback");
		}
		
		if ((Input.GetAxis("Horizontal"))<0)					
		{					
			//Left		
			transform.RotateAround (transform.up, Input.GetAxis("Horizontal") * 3 * Time.deltaTime);			
			move = true;			
			if (isGrounded && !anim.IsPlaying("runleft") && !anim.IsPlaying("run") && !anim.IsPlaying("runback") ) anim.CrossFade("runleft");
			
		}
		else if ((Input.GetAxis("Horizontal"))>0)					
		{		
			//Right			
			transform.RotateAround (transform.up, Input.GetAxis("Horizontal") * 3 * Time.deltaTime);
			move = true;
			if (isGrounded && !anim.IsPlaying("runright") && !anim.IsPlaying("run") && !anim.IsPlaying("runback") )anim.CrossFade("runright");
		}
		
		//stay
		if (!move && isGrounded && (anim.IsPlaying("runback") || anim.IsPlaying("run") || anim.IsPlaying("runleft") || anim.IsPlaying("runright") || anim["jump-land"].time > 0.34f)) anim.CrossFade("idle");
		
		
		//jump
		if (Input.GetKeyDown("space") && isGrounded)
		{
			Debug.Log ("space!");
			rigidbody.AddForce(transform.up * gravitySpeed * 1.2f); 
			isGrounded = false;
			rotationSpeed = rotationSpeedFly;
			anim.CrossFade("jump");
		}
		
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel(0);
		}
		
		
		//stick
			distForward = Mathf.Infinity;
			RaycastHit hitForward;
			if (Physics.SphereCast(transform.position, 0.25f, -transform.up + transform.forward, out hitForward, 5, RayCastLayerMask))
			{
			distForward = hitForward.distance;				
			}
			distDown = Mathf.Infinity;
			RaycastHit hitDown;
			if (Physics.SphereCast(transform.position, 0.25f, -transform.up, out hitDown, 5, RayCastLayerMask))
			{
			distDown = hitDown.distance;				
			}
			distBack = Mathf.Infinity;
			RaycastHit hitBack;
			if (Physics.SphereCast(transform.position, 0.25f, -transform.up + -transform.forward, out hitBack, 5, RayCastLayerMask))
			{
			distBack = hitBack.distance;				
			}
			 
			if (distForward < distDown && distForward < distBack)
			{			
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, hitForward.normal), hitForward.normal), Time.deltaTime * rotationSpeed);
			}
			else if (distDown < distForward && distDown < distBack)
			{				
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, hitDown.normal), hitDown.normal), Time.deltaTime * rotationSpeed);
			}
			else if (distBack < distForward && distBack < distDown)
			{				
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, hitBack.normal), hitBack.normal), Time.deltaTime * rotationSpeed);
			}				
	
		//gravity
		rigidbody.AddForce(-transform.up * Time.deltaTime * gravitySpeed); 
		
		
	}
	
	
	
	
	void OnCollisionEnter(Collision col) 
	{
		//if character collide with level
		if (((1<<col.gameObject.layer) & RayCastLayerMask) != 0)		
		{			
			isGrounded = true;
			rotationSpeed = rotationSpeedGrounded;
			
			if (!move && (anim.IsPlaying("runback") || anim.IsPlaying("run") || anim.IsPlaying("runleft") || anim.IsPlaying("runright") || anim["jump"].time == 0 )) anim.CrossFade("jump-land");
		}
		
		
		//stick to animated platform
		if (col.gameObject.tag == "Platform") transform.parent = col.transform;		
	}
	
	
	
	void OnCollisionExit(Collision col)
	{
		if (col.gameObject.tag == "Platform") transform.parent = null;
	}

}
