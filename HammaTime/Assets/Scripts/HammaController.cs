using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammaController : MonoBehaviour
{
    public float hammerModifier = 1;
    public Rigidbody rb;

    [SerializeField] private Vector3 hammerHitOffset = new Vector3(0, 2, 2);
    [SerializeField] private Vector3 raycastDir = new Vector3(0, -1, 0.5f).normalized;
    [SerializeField] private float rayLength = 3;

    public float strafeSpeed = 1;
    [SerializeField] private bool rightKeyDown = false;
    [SerializeField] private bool leftKeyDown = false;

    public int currentSlot = 0;

    [SerializeField] private float slotSize =  1.0f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dir = Input.GetAxisRaw("Horizontal");
        Strafe(dir);

        float jumpStrength = 2.0f;
        if (Input.GetKeyDown(KeyCode.Space))
            HitHammer(jumpStrength);

    }


    void Strafe(float dir)
    {
        rb.velocity = new Vector3(dir * strafeSpeed, rb.velocity.y, 0);
        
    }


    void HitHammer(float hitMagnitude)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + hammerHitOffset, raycastDir, out hit, rayLength))
        {
            if (Vector3.Dot(hit.normal, new Vector3(0, 1, 0)) > 0.9f)
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

}
