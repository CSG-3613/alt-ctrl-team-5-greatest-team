using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    public static TileManager TMinstance { get; private set; }

    private static GameManager gm;

    [SerializeField] private float velocity;
    [SerializeField] private GameObject FirstTile;
    [SerializeField] private float tileSize;
    public float startTileZ = 100;

    public bool gameMoving = true;


    [Header(" Tile/Objects")]
    //index of the current , used to swap what set of arrays are being used for obsticle and Tile generation
    [SerializeField] private int _currentIndex;
    //arrays for all the different s
    [SerializeField] private GameObject[] _1TilePrefabs;
// [SerializeField] private GameObject[] _1ObstaclePrefabs;
    [SerializeField] private GameObject[] _2TilePrefabs;
//  [SerializeField] private GameObject[] _2ObstaclePrefabs;
    [SerializeField] private GameObject[] _3TilePrefabs;
    //   [SerializeField] private GameObject[] _3ObstaclePrefabs;
    [SerializeField] private GameObject[] _4TilePrefabs;

    [SerializeField] private GameObject[] _5TilePrefabs;

    [SerializeField] private GameObject[] _6TilePrefabs;
    //chance to spawn an obsticle
    [SerializeField] private float chanceObstacle = 0.3f;

    //2D array initialization needs a static int value, This cannot be seen in the inspector
    private static int _numberOfs = 6;

    //2D Arrays Cannot be seen in inspector! Must be assigned in code!
    private GameObject[][] TilePrefabs = new GameObject[_numberOfs][];
    private GameObject[][] ObstaclePrefabs = new GameObject[_numberOfs][];

    private void Initialize2DArrays()
    {
        //Assigning the values of the 2d Array
        //Must have one for each number of s!!
        TilePrefabs[0] = _1TilePrefabs;
   //   ObstaclePrefabs[0] = _1ObstaclePrefabs;
        TilePrefabs[1] = _2TilePrefabs;
   //   ObstaclePrefabs[1] = _2ObstaclePrefabs;
        TilePrefabs[2] = _3TilePrefabs;
        //    ObstaclePrefabs[2] = _3ObstaclePrefabs;

        TilePrefabs[3] = _4TilePrefabs;
        TilePrefabs[4] = _5TilePrefabs;
        TilePrefabs[5] = _6TilePrefabs;


    }

    private void Awake()
    {
        // Ensure this is the only instance. If not, destroy self.
        if (TMinstance != null && TMinstance != this) { Destroy(this); }
        else { TMinstance = this; }
        //Check that Last Tile has been assigned
        if (FirstTile == null) { Debug.LogError("Assign Last Tile in inspector!!!"); }

        //Assigning the values of the 2d Array
        Initialize2DArrays();

    }

    void Start()
    {
        //get reference to GameManager
        gm = GameManager.instance;
    }

    void FixedUpdate()
    {
        if (gameMoving)
        {
            velocity = gm.getCurrSpeed();
            while (FirstTile.transform.position.z < startTileZ)
            {
                SpawnTile();
            }
        }
    }

    public void SpawnTile()
    {
        Vector3 spawnPos = FirstTile.transform.position + (tileSize - Time.deltaTime * velocity) * Vector3.forward;
        Quaternion spawnRot = FirstTile.transform.rotation;
        GameObject nextTile = Instantiate(
            TilePrefabs[_currentIndex][Random.Range(0, TilePrefabs[_currentIndex].Length)], //Gameobject to instantiate
            spawnPos, //TilePosition
            spawnRot); //Tile Rotation

        FirstTile = nextTile;
    }

}
