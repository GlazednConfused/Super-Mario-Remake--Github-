using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_script : MonoBehaviour
{
    //Player speed
    public int playerSpeed = 10;

    /*While you can change the jump power with playerJumpPower, I recommend instead increasing the
    gravity scale on the player object's rigid body, it will give the player more weight and less 
    "floaty-ness". Balance both variables when doing the jumping*/
    public int playerJumpPower = 1250;

    /*If the player sprite faces backwards when moving, check/uncheck the facingRight bool on the 
    player object.*/
    public bool facingRight = false;

    // moveX is the program detecting movement in the player object on the the X-axis
    private float moveX;

    // variable for checking if player is touching ground.
    public bool isGrounded;

    void Update()
    {
        //Check void PlayerMove for more info
        PlayerMove();
        PlayerRaycast();
    }

    void PlayerMove()
    {
        //moveX is defined to read the x-axis movement
        moveX = Input.GetAxis("Horizontal");

        //Jumping Button input, by default set to the space bar, also check if player is grounded.
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            Jump();
        }

        //code checks the players movement direction. Horizontal movement speed changing above or below 0 flips the player character
        if (moveX < 0.0f && facingRight == false)
        {
            FlipPlayer();
        }
        else if (moveX > 0.0f && facingRight == true)
        {
            FlipPlayer();
        }

        //General physics for the player
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    //Defining the jump mechanic for the program
    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
        //When jumping, you not touching the ground (lol duh).
        isGrounded = false;
    }

    //Code defining how to flip the player gameObject
    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug Log testing for ground
        //Debug.Log("Player has collided with ground");
        //Set the enviroment objects to a tag "ground" "breakblock" or "spawnblock" or create and change tag to match. (See footnotes; A)
        if (col.gameObject.tag == "ground")
        {
            isGrounded = true;
        }
        if (col.gameObject.tag == "breakblock")
        {
            isGrounded = true;
        }
        if (col.gameObject.tag == "spawnblock")
        {
            isGrounded = true;
        }
    }

    //PlayerRaycast is sends out a raycast. RayCasts will check for object names, tags, etc.
    void PlayerRaycast()
    {
        //RayCast facing above the player
        RaycastHit2D rayUp = Physics2D.Raycast(transform.position, Vector2.up);
        //Change (0.3f) number so block breaks and player hits it. Change tag to match as well (breakblock).
        if (rayUp != null && rayUp.collider != null && rayUp.distance < 0.3f && rayUp.collider.tag == "breakblock")
        {
            Destroy(rayUp.collider.gameObject);
        }
        //Change (0.3f) number so block break and player hits it. Change tag to match as well (spawnblock).
        if (rayUp != null && rayUp.collider != null && rayUp.distance < 0.3f && rayUp.collider.tag == "spawnblock")
        {
            //This code for spawning in invincibibilty gem
        }
        //RayCast facing below the player
        RaycastHit2D rayDown = Physics2D.Raycast(transform.position, Vector2.down);
        /*Change the hit distance (1.2f) to just under the player character, make tag the enemy tag. 
         *This will kill enemy and make player jump*/
        if (rayDown != null && rayDown.collider != null && rayDown.distance < 1.1f && rayDown.collider.tag == "enemy")
        {
            //I recommend make the Vector2.up force number slightly less than the jump power.
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1100);
            Destroy(rayDown.collider.gameObject);
        }

        /*Make tag same as enemy tag. (See footnotes; B) This entire code is blocked off and unnessisary for
        now. If ground can not have the "ground" tag, make below code not a comment.*/
        /*
        if (rayDown != null && rayDown.collider != null && rayDown.distance < 0.1f && rayDown.collider.tag != "enemy")
        {
            isGrounded = true;
        }
        */
    }
}

/*   Foot notes:
 *The code under label A is to let player jump when touching objects labeled with "ground" tag, however
 *some objects, like blocks, may have a seperate tag. Code under label B is set so that if object the
 *player collides with is not labeled enemy, then the player is touching the ground. Code under A is not
 *technically needed, but is there for back up/testing sake.*/
