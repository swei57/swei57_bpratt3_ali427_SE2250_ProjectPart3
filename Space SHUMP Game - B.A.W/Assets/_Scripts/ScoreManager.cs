using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    static private ScoreManager _S;
    static public int HIGH_SCORE = 0;

    public GameObject scoreBoard;

    [Header("Set Dynamically")]
    public int score = 0;

    private void Awake()
    {
        if (_S == null)
        {
            _S = this;
        }
        else
        {
            Debug.LogError("Error: ScoreManager.Awake(): S is already set");
        }
        scoreBoard = GameObject.Find("ScoreBoard");
        //check for high score in player prefs
        if (PlayerPrefs.HasKey("SHUMPHighScore"))
        {
            HIGH_SCORE = PlayerPrefs.GetInt("SHUMPHighScore");
            scoreBoard.GetComponent<Text>().text = "Score: "+score+"\nHigh Score: " + HIGH_SCORE;
        }
    }
    static public void EVENT(int points)
    {
        try
        {
            _S.Event(points);
        } catch(System.NullReferenceException nre)
        {
            Debug.LogError("ScoreManager:EVENT() called while S=null.\n" + nre);
        }
    }
    public void Event(int points)
    {
        score += points;
        scoreBoard.GetComponent<Text>().text = "Score: " + score + "\nHigh Score: " + HIGH_SCORE;
    }
    static public void GAMEOVER()
    {
        try
        {
            _S.GameOver();
        }
        catch (System.NullReferenceException nre)
        {
            Debug.LogError("ScoreManager:EVENT() called while S=null.\n" + nre);
        }
    }
    public void GameOver()
    {
        if (score > HIGH_SCORE)
        {
            HIGH_SCORE = score;
            PlayerPrefs.SetInt("SHUMPHighScore", HIGH_SCORE);
        }
        score = 0;
        scoreBoard.GetComponent<Text>().text = "Score: " + score + "\nHigh Score: " + HIGH_SCORE;
    }
}
