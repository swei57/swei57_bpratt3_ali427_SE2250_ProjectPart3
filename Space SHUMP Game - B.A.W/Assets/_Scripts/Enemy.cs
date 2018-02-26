using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float speed = .1f;
	public float fireRate = 0.3f;
	public float health = 10;
	public int score = 100;

    private BoundsCheck bndCheck;

    private void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    public Vector3 position {
		get {return(this.transform.position);}
		set {this.transform.position = value;}
	}

	public virtual void Update(){
		Move ();
        if(bndCheck != null && (bndCheck.offDown || bndCheck.offRight || bndCheck.offLeft))
        {
            Destroy(gameObject);
        }
	}

	public virtual void Move (){
	}
		
}
