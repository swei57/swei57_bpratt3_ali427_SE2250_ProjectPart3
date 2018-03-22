using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShip : MonoBehaviour {

    static public playerShip ship;   //setting ship default vals
    public float speed = 30;
    public float rollMult = -45;
	public float pitchMult = 30;
	public float gameRestartDelay = 5f;
	private GameObject lastTriggerGo = null;
	private float _shieldLevel = 4; //reference to the last triggering Gameobject

    // Use this for initialization
    private void Start(){
        transform.position= new Vector3(0, -25, 10);
		print ("okay");
    }
    void Awake() {
        if(ship == null){
            ship = this;   //set ship singleton
        }
        else{
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.Ship!");
        }
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
    }

	void OnTriggerEnter (Collider other){
		Transform rootT = other.gameObject.transform.root;
		GameObject go = rootT.gameObject;
		print(go.name);

		if (go == lastTriggerGo) {
			return;
		}
		lastTriggerGo = go;

		if (go.tag == "Enemy") {
			shieldLevel--;
			Destroy (go);

		} else {
			print ("Triggered by non-Enemy: " + go.name);
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
				Main.S.DelayedRestart(gameRestartDelay);
			}
		}
	}

}
