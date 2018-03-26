﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {
    [Header("Set in Inspector")]
    public GameObject poi; //player ship
    public GameObject[] panels; //scrolling foregrounds
    public float scrollSpeed = -30f;
    public float motionMult = 0.25f; //how much panels react to player mvmt

    private float panelHt; //height of panel
    private float depth; //pos.z of panels

    private void Start()
    {
        panelHt = panels[0].transform.localScale.y;
        depth = panels[0].transform.position.z;

        //set initial pos of panels
        panels[0].transform.position = new Vector3(0, 0, depth);
        panels[1].transform.position = new Vector3(0, panelHt, depth);
    }
    private void Update()
    {
        float tY, tX = 0;
        tY = Time.time * scrollSpeed % panelHt + (panelHt * 0.5f);
        if (poi != null)
        {
            tX = -poi.transform.position.x * motionMult;
        }
        //position of first panel
        panels[0].transform.position = new Vector3(tX, tY, depth);
        //position of second panel so starfield continuous
        if (tY >= 0)
        {
            panels[1].transform.position = new Vector3(tX, tY - panelHt, depth);
        }
        else
        {
            panels[1].transform.position = new Vector3(tX, tY + panelHt, depth);
        }
    }
}
