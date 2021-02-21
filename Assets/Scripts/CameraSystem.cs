using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    private GameObject player;

    //These variables will lock the camera from going past the side, above, or below the map. Put in max/min x/y.
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    void Start()
    {
        // Unity will define player as the object with the Player tag. (Our player character, this tag is in unioty by default)
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // If camera is having issues, try changing to void "LateUpdate" instead up void "Update".
    void Update()
    {
        float x = Mathf.Clamp (player.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(player.transform.position.y, yMin, yMax);
        gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
    }
}

/* Things to note: due to it being based on mario, the min/max y could most likely be set to 0, as we don't actually need the camera to move
 * up and down during jumping. However, if we are taking from older mario titles and want to have a map that is a little tall, then use the
 * yMin and yMax to set limits. 
 * This script is to be attached to the main camera in unity!*/
