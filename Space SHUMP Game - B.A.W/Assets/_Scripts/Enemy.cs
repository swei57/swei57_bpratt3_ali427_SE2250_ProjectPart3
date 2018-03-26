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
    private void OnCollisionEnter(Collision coll)
    {
        GameObject otherGO = coll.gameObject;
        switch (otherGO.tag)
        {
            case "ProjectileHero":
                Projectile p = otherGO.GetComponent<Projectile>();
                if (!bndCheck.isOnScreen)   //destroy projectile if enemy is off screen
                {
                    Destroy(otherGO);
                    break;
                }
                //hurt enemy
                health -= Main.GetWeaponDefinition(p.type).damageOnHit;
                if(health <= 0)
                {
                    // Destroy enemy if health at or below 0
                    Destroy(this.gameObject);
                }
                Destroy(otherGO);
                break;
            default:
                print("Enemy hit by non-ProjectileHero: " + otherGO.name);
                break;
        }
        /*
        if(otherGO.tag == "ProjectileHero")
        {
            Destroy(otherGO); //gudbye profile
            Destroy(gameObject); //gudbye enemy
        }
        else
        {
            print("Enemy hit by non-ProjectileHero: " + otherGO.name);
        }
        */
    }

}
