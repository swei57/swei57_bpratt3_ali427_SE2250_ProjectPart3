using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Level : MonoBehaviour
{
    static private Level S;
    static public GameObject levelText;
    static public GameObject elimText;
    static int waves = 0; //hold for number of enemies in each wave
    static string levelName = ""; //name of level
    static int levelCount = 1;
    static int deathCount = 0;
    static public bool levelOver = false;
    // Use this for initialization

    static public int getWaves()
    {
        return waves;
    }
    static public string getLevelText()
    {
        return levelName;
    }
    static public void setLevelCount(int count)
    {
         levelCount=count;
    }
    static public void SetLevelOver(bool b)
    {
        levelOver = b;
    }
    static public void setDeathCount(int count)
    {
        deathCount = count;
    }
    static public int getDeathCount()
    {
        return deathCount;
    }
    static public void incrementWaves()
    {
        waves = (waves * 2); //scale number of enemies per wave needed to level up
    }

    private void Awake() //initialize level
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("Error: Level.Awake(): S is already set");
        }
        levelText = GameObject.Find("Level");
        elimText = GameObject.Find("Eliminations");
        waves = 5;
        levelName = "Level: 1";
        levelOver = false;
        levelText.GetComponent<Text>().text = levelName;
        elimText.GetComponent<Text>().text = "Eliminations needed: 0\nElimination count: 0";
    }

    // Update is called once per frame
    void Update()
    {
        if (levelOver == true) //check to see if requirements are met to update level 
        {
            levelCount++;
            levelName = "Level: " + levelCount;
            levelText.GetComponent<Text>().text = levelName;
            levelOver = false;
            incrementWaves();
            print("LevelUP");
        }
        elimText.GetComponent<Text>().text = "Eliminations needed: " + getWaves()+"\nElimination count: "+getDeathCount(); 

    }
}
