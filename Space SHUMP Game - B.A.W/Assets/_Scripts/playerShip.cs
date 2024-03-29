﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerShip : MonoBehaviour {

    static public playerShip ship;   //setting ship default vals
    public float speed = 30f;
    public float rollMult = -45f;
	public float pitchMult = 30f;
	public float gameRestartDelay = 2f;
    public GameObject projectilePrefab;
    public GameObject whiteFlash;
    public float projectileSpeed = 40f;
	private GameObject _lastTriggerGo = null;
	private float _shieldLevel = 4f; //reference to the last triggering Gameobject

    public delegate void WeaponFireDelegate();
    public WeaponFireDelegate fireDelegate;

    public bool activeLaser;
    private bool _bombLoaded;
    private bool _activeShieldBoost; //add infinite shield for 5 sec
    private float _currTime;

    // Use this for initialization
    private void Start(){
        transform.position= new Vector3(0, -25, 10);
        activeLaser = false;
        _bombLoaded = false;
        whiteFlash.SetActive(false);
        _activeShieldBoost = false;
		print ("okay");
    }
    void Awake() {
        if(ship == null){
            ship = this;   //set ship singleton
        }
        else{
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.Ship!");
        }
        //fireDelegate += TempFire; 
    }

    // Update is called once per frame
    void Update () {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector3 position = transform.position;      //change player position based on axis
        position.x += xAxis * speed * Time.deltaTime;
        position.y += yAxis * speed * Time.deltaTime;
        transform.position = position;

        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult,0); //ship rotation
                                                                                      //allow ship to fire at enemies using delegate (spacebar)
        if (activeLaser)
        {
            fireDelegate();
            if (Time.time - _currTime > 3f) activeLaser = false;
        }
        else if (whiteFlash.activeSelf && Time.time - _currTime > 0.1f)
        {
            whiteFlash.SetActive(false);
        }
        else if (_bombLoaded && Input.GetKeyDown("space"))
        {
            _bombLoaded = false;
            whiteFlash.SetActive(true);
            _currTime = Time.time;
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                ScoreManager.EVENT(enemy.GetComponent<Enemy>().score);
                Destroy(enemy);
                Enemy.deathCount++; //increment number of enemies destroyed by this powerup
                Level.setDeathCount(Enemy.deathCount);
            }
        }
        else if (Input.GetKeyDown("space") && fireDelegate != null)
        {
            fireDelegate();
        }
        if (_activeShieldBoost)
        {
            _shieldLevel = 4;
            Shield.mat.SetColor("_Color", Color.yellow);
            if (Time.time - _currTime > 5f)
            {
                _activeShieldBoost = false; //activate infinite shield for 5 sec
                _shieldLevel = 4; //resets shield HP back to full after active is done
                Shield.mat.SetColor("_Color", Color.green);
            }
        }

    }
  /*  void TempFire() //firing bullets
    {
        GameObject projGO = Instantiate<GameObject>(projectilePrefab);
        projGO.transform.position = transform.position;
        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
        //rigidB.velocity = Vector3.up * projectileSpeed;
        Projectile proj = projGO.GetComponent<Projectile>();
        proj.type = WeaponType.blaster;
        float tSpeed = Main.GetWeaponDefinition(proj.type).velocity;
        rigidB.velocity = Vector3.up * tSpeed;
    }*/

	void OnTriggerEnter (Collider other){
		Transform rootT = other.gameObject.transform.root;
		GameObject go = rootT.gameObject;
		print(go.name);

		if (go == _lastTriggerGo) {
			return;
		}
		_lastTriggerGo = go;

		if (go.tag == "Enemy") {
			shieldLevel--;
			Destroy (go);
		}
        else if(go.tag == "PowerUp")
        {
            AbsorbPowerUp(go);
        }
        else {
            shieldLevel--;
            Destroy(go);
        }
	}

    public void AbsorbPowerUp(GameObject go)
    {
        PowerUp pu = go.GetComponent<PowerUp>();


        pu.AbsorbedBy(this.gameObject);
        if(activeLaser || _bombLoaded || _activeShieldBoost)
        {
            return;
        }
        switch (pu.type)
        {
            case WeaponType.laser:
                _currTime = Time.time;
                activeLaser = true;
                break;
            case WeaponType.phaser:
                _bombLoaded = true;
                break;
            case WeaponType.shield:
                _currTime = Time.time;
                _activeShieldBoost = true;
                break;
        }
    }

	public float shieldLevel{
		get {
			return (_shieldLevel);
		}
		set {
			_shieldLevel = Mathf.Min (value, 4);
			//if the shield is going to be set to less than zero

			if (value < 0) {
				Destroy (this.gameObject);
                ScoreManager.GAMEOVER();
                Level.levelText.GetComponent<Text>().text = "GAME OVER!";
                Level.setLevelCount(1); //reset level
                Enemy.deathCount=0; //reset kills
                Level.setDeathCount(0);//reset kills
                
                Main.S.DelayedRestart(gameRestartDelay);
			}
		}
	}

}
