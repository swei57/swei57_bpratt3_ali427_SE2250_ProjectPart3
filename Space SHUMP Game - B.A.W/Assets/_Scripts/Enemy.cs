using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float speed = .1f;
	public float fireRate = 0.3f;
	public float health = 10;
	public int score = 100;
    public float showDamageDuration = 0.1f;
    public float powerUpDropChance = 0f;
<<<<<<< HEAD

=======
    static public int deathCount = 0;
>>>>>>> levelFix
    [Header("Set Dynamically: Enemy")]
    public Color[] originalColors;
    public Material[] materials;
    public bool showingDamage = false;
    public float damageDoneTime;
    public bool notifiedOfDestruction = false;

    private BoundsCheck bndCheck;

    private void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
        //get materials & colors for this GameObject & children
        materials = Utils.GetAllMaterials(gameObject);
        originalColors = new Color[materials.Length];
        for(int i = 0; i < materials.Length; i++)
        {
            originalColors[i] = materials[i].color;
        }
    }

    public Vector3 position {
		get {return(this.transform.position);}
		set {this.transform.position = value;}
	}

	public virtual void Update(){
		Move ();

        if (showingDamage && Time.time > damageDoneTime)
        {
            UnShowDamage();
        }

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
                ShowDamage();
                health -= Main.GetWeaponDefinition(p.type).damageOnHit;
                if(health <= 0)
                {
                    if (!notifiedOfDestruction)
                    {
                        Main.S.ShipDestroyed(this);
                    }
                    notifiedOfDestruction = true;
                    // Destroy enemy if health at or below 0
                    Destroy(this.gameObject);
                    ScoreManager.EVENT(score);
<<<<<<< HEAD
=======
                    deathCount++; //counting how many enemies were destroyed
                    Level.setDeathCount(deathCount);
>>>>>>> levelFix
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
    void ShowDamage()
    {
        foreach(Material m in materials)
        {
            m.color = Color.red;
        }
        showingDamage = true;
        damageDoneTime = Time.time + showDamageDuration;
    }
    void UnShowDamage()
    {
        for(int i = 0; i < materials.Length; i++)
        {
            materials[i].color = originalColors[i];
        }
        showingDamage = false;
    }

}
