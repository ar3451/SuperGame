using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class Explosion : NetworkBehaviour {

	private ParticleSystem ps;
	public int rate = 7500;
	int count;

	// Use this for initialization
	void Start () {
		ps = GetComponent<ParticleSystem>();
		var em = ps.emission;
		em.rateOverTime = rate;
		count = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (count >50) {
			var em = ps.emission;
			em.rateOverTime = 0;
			Destroy (gameObject, 1);
		}
		count ++;
	}
}

