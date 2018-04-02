using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {
	[Header ("Set in Inspector")]
	public float rotationsPerSecond = 0.1f;

	[Header ("Set Dynamically")]
	public int levelShown = 0;

	static public Material mat;

	// Use this for initialization
	void Start () {
		mat = GetComponent<Renderer> ().material;
	}


	
	// Update is called once per frame
	void Update () {
		//get the current shield level from the hero
		int currLevel = Mathf.FloorToInt(playerShip.ship.shieldLevel);

		//if this is different from levelShown
		if (levelShown != currLevel) {
			levelShown = currLevel;
            if(levelShown == 4) //if shield is infinite, don't decrease shield levels
            {
                mat.mainTextureOffset = new Vector2(levelShown, 0);
            }
			//adjust the texture offset to show different shield level
			mat.mainTextureOffset = new Vector2 (0.2f*levelShown,0);
        }

		float rZ = -(rotationsPerSecond * Time.time * 360) % 360f;
		transform.rotation = Quaternion.Euler (0, 0, rZ);
	}
}
