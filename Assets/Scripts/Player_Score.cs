using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Score : MonoBehaviour
{
    //Time left for player, can be changed in the inspector
    public float timeLeft = 120;
    public int playerScore = 0;
    public GameObject timeLeftUI;
    public GameObject playerScoreUI;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(timeLeft); Debug Log code for showing time left.
        timeLeft -= Time.deltaTime;
        /* Create a Text UI object for both the time left and the score. In the timeLeftUI and playerScoreUI
         * public class, place both text objects to their respective place in the inspector */
        timeLeftUI.gameObject.GetComponent<Text>().text = ("Time Left: " + (int)timeLeft);
        playerScoreUI.gameObject.GetComponent<Text>().text = ("Score: " + playerScore);
        //Time is treated as a float, there for 0.1f is used instead of 0 or 1.
        if (timeLeft < 0.1f)
        {
            //Use whatever the scene is named. Change for different levels.
            SceneManager.LoadScene("SampleScene");
        }
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        /*The gameObject trigger that is used for the end of the level should be put here. Set to look for NAMED object EndLevel.
         * also enter the load scene name here for when you beat the level.*/
        if (trig.gameObject.name == "EndLevel")
        {
            CountScore();
            SceneManager.LoadScene("Level 2"); 
        }
        //On last level, passing gameObject "LastLevel" will take you to the main menu or a high score menu
        if (trig.gameObject.name == "LastLevel")
        {
            CountScore();
            SceneManager.LoadScene("Menu");
        }
        //The collectable gameObject should have matching tag here.
        if (trig.gameObject.tag == "coin")
        {
            playerScore += 20;
            Destroy(trig.gameObject);
        }
        /*NOTE: Besides coin, other objects use RayCast. This code will not activate as it is not having 2d collision.
        //Enemy tag here*/
        if (trig.gameObject.tag == "enemy")
        {
            playerScore += 20;
            Destroy(trig.gameObject);
        }
        //Breaking blocks increase score by 10
        if (trig.gameObject.tag == "breakblock")
        {
            playerScore += 10;
            Destroy(trig.gameObject);
        }

        if (trig.gameObject.tag == "power")
        {
            playerScore += 10;
            Destroy(trig.gameObject);
        }
        
        void CountScore()
        {
            //Debug.Log("Level Complete"); A Debug Log test code, remove comment tag for testing
            //Get player score at the end of the level
            playerScore = playerScore + (int)(timeLeft * 10);
            //Displays playerscore in the debug log
            Debug.Log(playerScore);
        }
    }
}
//Attach to the player character