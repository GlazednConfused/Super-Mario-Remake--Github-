using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMove : MonoBehaviour
{
    //Enemy movement speed
    public int EnemySpeed;
    public int XMoveDirection;

    // Update is called once per frame
    void Update()
    {
        /*Using the Raycast layer to check when to turn the enemy around. IMPORTANT, go to: Edit, Project Settings,
         * Physics2D, and turn "Queries Start In Colliders" off.*/
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(XMoveDirection, 0), 6f);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(XMoveDirection, 0) * EnemySpeed;
        /* Detects an object in front and behind the enemy, and when detected, changes direction. Change hit.distance
         number to change how far raycast check goes out to.*/
         Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z), new Vector2(XMoveDirection, 0) * 6f, Color.red);
        if (hit.collider != null)
        {
            Flip();
            //Enemy damage system, set tag to Player tag. Make sure to change current scene name
            if (hit.collider.tag == "Player")
            {
                Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
            }
        }
    }

    /*Enemy turns and flips orientaion. NOTE: If an enemy is facing/moving the wrong direction, set X Move Direction 
    to -1 in the inspector*/
    void Flip()
    {
        if (XMoveDirection > 0)
        {
            XMoveDirection = -1;
            Vector2 localScale = gameObject.transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
        else
        {
            XMoveDirection = 1;
            Vector2 localScale = gameObject.transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}
//Attach script to the enemy game object
