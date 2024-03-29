﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum WeaponType //declares all possible weapon types and powerups
{
    none,  
    blaster,   //blaster bois
    simple,
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
    private Renderer _collarRend;

	// Use this for initialization
	void Start () {
        collar = transform.Find("Collar").gameObject;
        _collarRend = collar.GetComponent<Renderer>();
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
        if(rootGo.GetComponent<Enemy>() != null)
        {
            if(rootGo.GetComponent<Enemy>().canShoot)
            {
                Invoke("Fire", 2f);
            }
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
        else if(type == WeaponType.phaser)
        {
            
        }
        else if(type == WeaponType.laser)
        {
            
        }else if(type == WeaponType.shield)
        {

        }
        else
        {
            this.gameObject.SetActive(true);
        }
        def = Main.GetWeaponDefinition(_type);
        _collarRend.material.color = def.color;
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
                Main.GetWeaponDefinition(p.type).damageOnHit = 1;
                p = MakeProjectile(); //make right projectile
                p.transform.rotation = Quaternion.AngleAxis(30, Vector3.back);
                p.rigid.velocity = p.transform.rotation * vel;
                Main.GetWeaponDefinition(p.type).damageOnHit = 1;
                p = MakeProjectile(); //make left projectile
                p.transform.rotation = Quaternion.AngleAxis(-30, Vector3.back);
                p.rigid.velocity = p.transform.rotation * vel;
                Main.GetWeaponDefinition(p.type).damageOnHit = 1;
                break;
            case WeaponType.simple:
                p = MakeProjectile();
                p.rigid.velocity = vel;
                Main.GetWeaponDefinition(p.type).damageOnHit = 3;
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
            go.tag = "Projectile Enemy";
            go.layer = LayerMask.NameToLayer("ProjectileEnemy");
        }
        go.transform.position = collar.transform.position;
        go.transform.SetParent(PROJECTILE_ANCHOR, true);
        Projectile p = go.GetComponent<Projectile>();
        p.type = type;
        GameObject rootGo = transform.root.gameObject;
        lastShotTime = Time.time;
        if (rootGo.GetComponent<playerShip>() != null)
        {
            if (rootGo.GetComponent<playerShip>().activeLaser) lastShotTime = 0;
        }
        return (p);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("x"))
        {
            if(_type == WeaponType.blaster)
            {
                SetType(WeaponType.simple);
            }
            else if(_type == WeaponType.simple)
            {
                SetType(WeaponType.blaster);
            }
        }
	}
}
