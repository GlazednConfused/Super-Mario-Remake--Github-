using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    //The powerup's speed and start direction. 1 is right, -1 is left.
    public int PowerSpeed = 3;
    public int XMoveDirection = 1;

    //The power up will move back and forth, bouncing off objects, and turning.
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(XMoveDirection, 0));
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(XMoveDirection, 0) * PowerSpeed;
        if (hit.distance < 1.2f)
        {
            Flip();
        }
    }

    //Powerup with change direction on collision
    void Flip()
    {
        if (XMoveDirection > 0)
        {
            XMoveDirection = -1;
        }
        else
        {
            XMoveDirection = 1;
        }
    }
}
/*You'll notice code is very close to EnemyMove. Put this script on the powerup gameobject. NOTE!:
 * This is not the script for the powerup effect, this is just it's movement, the powerup effect will
 * (Most likely) be put in the player_script and be triggered upon collision with the "power" tag.*/