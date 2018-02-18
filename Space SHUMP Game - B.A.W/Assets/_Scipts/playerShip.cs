using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShip : MonoBehaviour {

    static public playerShip ship;   //setting ship default vals
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public float shieldLevel = 1;
    // Use this for initialization
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
}
