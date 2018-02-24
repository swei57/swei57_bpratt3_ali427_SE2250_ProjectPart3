using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1_Movement : Enemy {

	private int x, y, z;

	public Vector3 ChooseDirection;

	void Start(){
		float i = Random.value;
		y = -1;
		z = 0;
		x = (i <= 0.5) ? -1 : 1;
	}

	// Update is called once per frame
	public override void Update () {
		Move ();
	}
	public override void Move (){
		ChooseDirection.Set (x, y, z);
		transform.Translate (ChooseDirection * speed);
	}


			
}
