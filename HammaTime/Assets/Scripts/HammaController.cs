using System.Collections;
using System.IO.Ports;
using System.Collections.Generic;
using UnityEngine;


public class HammaController : MonoBehaviour
{
    private SerialPort port = new SerialPort("COM3", 9600);
    private string ardInput;

    public float hammerModifier = 1;
    public Rigidbody rb;

    [SerializeField] private Vector3 playerStartPos; 
    [SerializeField] private Vector3 hammerHitOffset = new Vector3(0, 2, 2);
    [SerializeField] private Vector3 raycastDir = new Vector3(0, -1, 0.5f).normalized;
    [SerializeField] private float rayLength = 3;

    public float strafeSpeed = 1;
    [SerializeField] private int lastDir = 0;

    public int currentSlot = 0;

    [SerializeField] private float slotSize =  1.0f;



    // Start is called before the first frame update
    void Start()
    {
        playerStartPos = transform.position;
        port.Open();
        Debug.Log("Port is Open");
    }

    // Update is called once per frame
    void Update()
    {
        SerialDataReading();
        int dir = (int)Input.GetAxisRaw("Horizontal");
        
        Strafe(dir);

        float distToTarget = (playerStartPos.x + currentSlot * slotSize) - transform.position.x;
        if (Mathf.Abs(distToTarget) > 0.1)
        {
            rb.velocity = new Vector3(distToTarget * strafeSpeed, rb.velocity.y, 0);
        }

        float jumpStrength = 2.0f;
        if (Input.GetKeyDown(KeyCode.Space))
            HitHammer(jumpStrength);
    }


    void Strafe(int dir)
    {
        if (dir == lastDir)
            return;

        //rb.velocity = new Vector3(dir * strafeSpeed, rb.velocity.y, 0);

        if (Mathf.Abs(currentSlot + dir) > 1)
            return;

        currentSlot += dir;
        lastDir = dir;
    }


    void HitHammer(float hitMagnitude)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + hammerHitOffset, raycastDir, out hit, rayLength))
        {
            if (Vector3.Dot(hit.normal, new Vector3(0, 1, 0)) > 0.75f)
            {
                Jump(hitMagnitude);
            }
        }
    }

    void Jump(float jumpMagnitude)
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpMagnitude * hammerModifier, 0);
    }


    void Death()
    {

    }
    public float SerialDataReading()
    {
        ardInput = port.ReadLine();
        //Debug.Log("Input: " + ardInput);

        if (ardInput == "Left")
        {
            Strafe(-1);
        }

        else if (ardInput == "Right")
        {
            Strafe(1);
        }

        if (ardInput == "Big squeeze")
        {
            HitHammer(2.0f);
        }
        return 1;
    }
}
