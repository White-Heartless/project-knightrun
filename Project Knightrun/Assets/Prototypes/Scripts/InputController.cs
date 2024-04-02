using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
	const int MIDDLE_LANE = 1;

    public float laneChangeSpeed = 5f;
    public float jumpForce = 150f;
    private bool canMove;
    private bool canJump;
	[SerializeField]
    private Player player;
	[SerializeField]
    private GameController gameController;
    [SerializeField]
    private Animator animator;

	// lane positions, more could be added
	//IF MORE LANES ARE ADDED UPDATE MIDDLE_LANE MANUALLY!!!
	private float[] lanePositions = new float[] { -1.5f, 0f, 1.5f };

	//todo add get set
    private int currentLaneIndex = 1; // start at the middle lane

    public void Start()
    {
        player = FindObjectOfType<Player>();
		canMove = true;
    }

    void Update()
    {
        LeftRight();
        Jump();
    }

	public void LeftRight()
	{
        if (Input.GetKeyDown(KeyCode.A) && currentLaneIndex > 0 && canMove && !gameController.is2D)
        {
            animator.SetTrigger("LeftStrafe");
            StartCoroutine(MoveToLane(currentLaneIndex - 1)); // Move to the left lane
        }
        else if (Input.GetKeyDown(KeyCode.D) && (currentLaneIndex < (lanePositions.Length - 1))  && canMove && !gameController.is2D)
        {
            animator.SetTrigger("RightStrafe");
            StartCoroutine(MoveToLane(currentLaneIndex + 1)); // Move to the right lane
        }
	}

	//these 2 functions are needed to prevent lane bugs when switching 2d <-> 3d
	public void Adjust() => StartCoroutine(CoroutineAdjust());

	IEnumerator CoroutineAdjust()
    {
        yield return new WaitForSeconds(0.25f); //prevents a bug that messes up the lanes
		StartCoroutine(MoveToLane(MIDDLE_LANE));
    }

	IEnumerator MoveToLane(int targetLaneIndex)
    {
        canMove = false; //prevent moving while already moving

        float targetX = lanePositions[targetLaneIndex];
        while (Mathf.Abs( player.gameObject.transform.position.x - targetX) > 0.01f)
        {
            player.gameObject.transform.position = Vector3.MoveTowards(player.gameObject.transform.position, new Vector3(targetX, player.gameObject.transform.position.y, player.gameObject.transform.position.z), laneChangeSpeed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }
        // Ensure the player is exactly at the target position
        player.gameObject.transform.position = new Vector3(targetX, player.gameObject.transform.position.y, player.gameObject.transform.position.z);
        currentLaneIndex = targetLaneIndex;
        canMove = true;
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (canJump == false))
        {
            animator.SetTrigger("Jump");
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
