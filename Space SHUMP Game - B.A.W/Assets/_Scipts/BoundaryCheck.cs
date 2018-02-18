using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Keeps player on the screen
public class BoundaryCheck : MonoBehaviour {
    public float radius = 1f;
    public float cameraWidth;
    public float cameraHeight;
	// Use this for initialization
	void Awake () {
        cameraHeight = Camera.main.orthographicSize;
        cameraWidth = cameraHeight * Camera.main.aspect;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 position = transform.position;
        if(position.x > cameraWidth - radius){
            position.x = cameraWidth - radius;
        }
        if (position.x < -cameraWidth + radius){
            position.x = -cameraWidth + radius;
        }
        if (position.y > cameraHeight - radius){
            position.y = cameraHeight - radius;
        }
        if (position.y > -cameraHeight + radius){
            position.y = -cameraHeight + radius;
        }
        transform.position = position;
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Vector3 boundSize = new Vector3(cameraWidth * 2, cameraHeight * 2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }
}
