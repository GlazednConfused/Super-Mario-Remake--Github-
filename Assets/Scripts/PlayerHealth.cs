using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public bool hasDied;

    //Player is not dead at the start
    void Start()
    {
        hasDied = false;
    }

    void Update()
    {
        //IMPORTANT! Set the Y minimum the player can fall to here, it is not public, so must be done in code.
        if (gameObject.transform.position.y < -25)
        {
            //Debug Log test of dying
            Debug.Log("Player Has Died");
            //Player has died
            hasDied = true;

        }
        if (hasDied == true)
        {
            StartCoroutine("Die");
        }
    }

    // When the player dies, reload the scene.
    IEnumerator Die ()
    {
        //Set LoadScene to the level scene! DON'T FORGET! Change the scene load when playing "level 2".
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        yield return null;
    }
}
/* Attach this script to the player object.
*/