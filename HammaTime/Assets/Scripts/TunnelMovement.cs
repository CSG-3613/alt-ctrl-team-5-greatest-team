using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour
{
    public static GameManager gm;
    public static TileManager tm;

    public float velocity;
    public float endTileZ = -40;

    public float Zpos;

    private void Start()
    {
        //get reference to GameManager
        gm = GameManager.instance;
        //get reference to TileManager
        tm = TileManager.TMinstance;

    }
    // Update is called once per frame
    void Update()
    {
        //get current velocity from GM
        velocity = gm.getCurrSpeed();
        //move the object towards the player
        gameObject.transform.position = gameObject.transform.position + (Vector3.back)*velocity*Time.deltaTime;
        Zpos = gameObject.transform.position.z;

        if (this.gameObject.transform.position.z < endTileZ)
        {
            //Check to see if this item is a Tile or an obsticle
            if(gameObject.layer == 3)
            {
                //Call the Tile manager to SpawnTile() and pass the Tile to destroy
                tm.SpawnTile(this.gameObject);
            }
            //if its an obsticle, destroy it
            else if(gameObject.layer == 6) { Destroy(gameObject);}

        }
    }
}
