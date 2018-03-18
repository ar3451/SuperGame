using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WallBehavior : NetworkBehaviour {


	void OnTriggerEnter(Collider collide){
		if(collide.gameObject.tag == "Player"){
			collide.gameObject.transform.DetachChildren ();
			NetworkServer.Destroy (collide.gameObject);
		}

	}
}
