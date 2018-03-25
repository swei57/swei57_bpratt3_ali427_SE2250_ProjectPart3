using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum WeaponType //declares all possible weapon types and powerups
{
    none,   
    blaster,   //blaster bois
    spread,    //2 shot
    phaser,    //wave shots
    missile,   //homing shots
    laser,     //D.O.T weapon
    shield,    //improve shield
}

[System.Serializable] //allows us to both see and edit in the inspector
public class WeaponDefinition
{
    public WeaponType type = WeaponType.none;
    public string letter;
    public Color color = Color.white;
    public GameObject projectilePrefab;
    public Color projectileColor = Color.white;
    public float damageOnHit = 0;
    public float continuousDamage = 0;
    public float delayBetweenShots = 0;
    public float velocity = 20;

}
public class Weapon : MonoBehaviour {
    static public Transform PROJECTILE_ANCHOR;
    [Header("Set Dynamically")]
    [SerializeField]
    private WeaponType _type = WeaponType.none;
    public WeaponDefinition def;
    public GameObject collar;
    public float lastShotTime; //time of the last shot fired
    private Renderer collarRend;

	// Use this for initialization
	void Start () {
        collar = transform.Find("Collar").gameObject;
        collarRend = collar.GetComponent<Renderer>();
        //set type call
        SetType(_type);

        //create anchor for all projectiles
        if(PROJECTILE_ANCHOR == null)
        {
            GameObject go = new GameObject("_ProjectileAnchor");
            PROJECTILE_ANCHOR = go.transform;
        }

        //find fireDelgate of root gameobject
        GameObject rootGo = transform.root.gameObject;
        if(rootGo.GetComponent<playerShip>() != null)
        {
            rootGo.GetComponent<playerShip>().fireDelegate += Fire;
        }

	}

    public WeaponType type
    {
        get { return (_type); }
        set { SetType(value); }
    }

    public void SetType (WeaponType wt)
    {
        _type = wt;
        if (type == WeaponType.none)
        {
            this.gameObject.SetActive(false);
            return;
        }
        else
        {
            this.gameObject.SetActive(true);
        }
        def = Main.GetWeaponDefinition(_type);
        collarRend.material.color = def.color;
        lastShotTime = 0;
    }

    public void Fire()
    {
        //return if inactive
        if (!gameObject.activeInHierarchy) return;
        //if not enough time between shots, return
        if(Time.time - lastShotTime < def.delayBetweenShots)
        {
            return;
        }
        Projectile p;
        Vector3 vel = Vector3.up * def.velocity;
        if (transform.up.y < 0)
        {
            vel.y = -vel.y;
        }
        switch (type)
        {
            case WeaponType.blaster:
                p = MakeProjectile(); //middle projectile
                p.rigid.velocity = vel;
                p = MakeProjectile(); //make right projectile
                p.transform.rotation = Quaternion.AngleAxis(30, Vector3.back);
                p.rigid.velocity = p.transform.rotation * vel;
                p = MakeProjectile(); //make left projectile
                p.transform.rotation = Quaternion.AngleAxis(-30, Vector3.back);
                p.rigid.velocity = p.transform.rotation * vel;
                break;
            //... add more weapons heres
        }
    }

    public Projectile MakeProjectile()
    {
        GameObject go = Instantiate<GameObject>(def.projectilePrefab);
        if(transform.parent.gameObject.tag == "Hero")
        {
            go.tag = "ProjectileHero";
            go.layer = LayerMask.NameToLayer("ProjectileHero");

        }
        else
        {
            go.tag = "ProjectileEnemy";
            go.layer = LayerMask.NameToLayer("ProjectileEnemy");
        }
        go.transform.position = collar.transform.position;
        go.transform.SetParent(PROJECTILE_ANCHOR, true);
        Projectile p = go.GetComponent<Projectile>();
        p.type = type;
        lastShotTime = Time.deltaTime;
        return (p);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
