using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public int forwardSpeed;
    public int laneNum = 2;
    public float horizVel = 0;
    public float horizSpeed = 2000f;
    public int jumpForce = 400;
    public bool canMove;
    public bool canJump;
	public float slideTime;
    [SerializeField]
    public Player player;

    public void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        LeftRight();
        Jump();
    }

    public void LeftRight()
    {
        Vector3 currentJump = player.GetComponent<Rigidbody>().velocity;
        player.GetComponent<Rigidbody>().velocity = new Vector3(horizVel, currentJump.y, forwardSpeed);

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
        yield return new WaitForSeconds(slideTime);
        horizVel = 0;
        canMove = false;
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (canJump == false))
        {
            Vector3 currentVelocity = player.GetComponent<Rigidbody>().velocity;
            player.GetComponent<Rigidbody>().velocity = new Vector3(currentVelocity.x, 0, currentVelocity.z);
            player.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce);
            canJump = true;
        }
    }

    public void ResetJump()
    {
        canJump = false;
    }

}
