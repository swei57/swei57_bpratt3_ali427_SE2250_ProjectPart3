using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1_Movement : Enemy {

	private int _x, _y, _z;

	public Vector3 ChooseDirection;

	void Start(){
		float i = Random.value;
		_y = -1;
		_z = 0;
		_x = (i <= 0.5) ? -1 : 1;
	}

	// Update is called once per frame
	public override void Update () {
        base.Update();
	}
	public override void Move (){
		ChooseDirection.Set (_x, _y, _z);
		transform.Translate (ChooseDirection * speed);
	}


			
}
