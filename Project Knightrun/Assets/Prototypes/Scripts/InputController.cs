using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public int forwardSpeed;
    public int laneNum = 2;
    public float horizVel = 0;
    public float horizSpeed = 10;
    public float jumpForce = 200f;
    public bool canMove;
    public bool canJump;
    [SerializeField]
    private Rigidbody rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        LeftRight();
        Jump();
    }

    public void LeftRight()
    {
        Vector3 currentJump = rb.velocity;
        rb.velocity = new Vector3(horizVel, currentJump.y, forwardSpeed);

        if (Input.GetKeyDown(KeyCode.A) && (laneNum > 1) && (canMove == false))
        {
            horizVel = -horizSpeed;
            StartCoroutine(stopSlide());
            laneNum -= 1;
            canMove = true;
        }

        if (Input.GetKeyDown(KeyCode.D) && (laneNum < 3) && (canMove == false))
        {
            horizVel = horizSpeed;
            StartCoroutine(stopSlide());
            laneNum += 1;
            canMove = true;
        }
    }

    IEnumerator stopSlide()
    {
        yield return new WaitForSeconds(.5f);
        horizVel = 0;
        canMove = false;
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (canJump == false))
        {
            Vector3 currentVelocity = rb.velocity;
            rb.velocity = new Vector3(currentVelocity.x, 0, currentVelocity.z);
            rb.AddForce(Vector3.up * 500);
            canJump = true;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            canJump = false;
        }
    }
}
