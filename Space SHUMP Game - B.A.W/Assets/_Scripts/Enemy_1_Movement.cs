using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1_Movement : MonoBehaviour {

	private float speed = .1f;

	public Vector3 ChooseDirection;
	
	// Update is called once per frame
	void Update () {

		System.Random random = new System.Random();

		float i = Random.value;
		if(i==0){
			ChooseDirection.Set (-1, -1, 0);
		}
		else {
			ChooseDirection.Set (1f, -1f, 0f);
		}

		transform.Translate (ChooseDirection * speed);

	}


			
}
