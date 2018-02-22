using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1_Movement : MonoBehaviour {

	private float speed = .1f;
	private int x, y, z;

	public Vector3 ChooseDirection;

	void Start(){
		float i = Random.value;

		if(i<=0.5){
			x = -1;
			y = -1;
			z = 0;
		}
		else {
			x = 1;
			y = -1;
			z = 0;
		}
	}

	// Update is called once per frame
	void Update () {

		//System.Random random = new System.Random();

		ChooseDirection.Set (x, y, z);
		transform.Translate (ChooseDirection * speed);

	}


			
}
