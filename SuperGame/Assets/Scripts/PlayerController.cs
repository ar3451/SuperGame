using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	const int movecdTime = 1;
	const int offcdTime = 3;


	public VitrualJoyStick joystick;
	public GameObject exp;
	public float radius;
	public float power;
	private Rigidbody rb;
	//ParticleSystem particle;
	bool pushed;
	Transform camTransform;
	Button butt;
	float offcd;		//keep track of push cooldown
	float movecd;	//keep track of movement cd



	void Start(){
		//get basic items
		rb = GetComponent<Rigidbody> ();
		joystick = GameObject.FindWithTag("joystick").GetComponent<VitrualJoyStick>();


		//set initial position
		transform.Translate (0f, 0.905f, 0f);
		transform.Rotate(-90,0,0);

		//check to see if this is the player and set camera and button
		if(isLocalPlayer){
			camTransform = Camera.main.transform;
			camTransform.position = new Vector3(0f,1.5f,-1f);
			camTransform.Rotate (-70, 0, 0);
			camTransform.parent = transform;
			butt = GameObject.FindWithTag ("button1").GetComponent<Button> ();
			butt.onClick.AddListener(Push);
		}

	}
		
	void FixedUpdate () {
		
		//check to see if its the local player
		if (!isLocalPlayer) {

			return;
		}



		if (movecd > Time.time)
			return;


		//CharacterController controller = GetComponent<CharacterController> ();
		float moveH = joystick.inputVector.x;
		float moveV = joystick.inputVector.y;
		int dirMult;

		//controll the direction
		if (moveV > -.1)
			dirMult = -1;
		else
			dirMult = 1;
		
		Vector3 movement = new Vector3(0, 0, 1);
		transform.Rotate (0f,0f,moveH*1.25f);



		movement = new Vector3 (0, dirMult * joystick.inputVector.magnitude/3, 0);
		//Debug.Log (movement);
		rb.AddRelativeForce  (movement,ForceMode.VelocityChange);

	}

	public void Push(){
		//check the cooldown time
		if (offcd > Time.time)
			return;
		CmdPush ();
		//check the cooldown time
		//add the cooldown
		offcd = Time.time + offcdTime;
		movecd = Time.time + movecdTime;

	}

	[Command]
	public void CmdPush(){
		GameObject instance = Instantiate(exp,transform) as GameObject;
		NetworkServer.Spawn (instance);


		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
		foreach (Collider hit in colliders){
			if(hit.gameObject.tag=="Player")
				hit.gameObject.GetComponent<PlayerController>().RpcExplosion(explosionPos);

		}
	}

	[ClientRpc]
	public void RpcExplosion(Vector3 explosionPos){
		Debug.Log ("Explode");
		if (!isLocalPlayer)
			return;
		rb.AddExplosionForce(power, explosionPos, radius, 0F);
		rb.AddRelativeForce (1f, 0f, 1f);
		transform.Rotate (0f,0f,90f);
	}

}