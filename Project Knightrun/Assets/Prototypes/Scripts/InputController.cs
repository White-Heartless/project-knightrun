using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
	const int MIDDLE_LANE = 1;

    public float laneChangeSpeed = 5f;
    public float jumpForce3d = 150f;
	public float jumpForce2d = 150f;
    public bool canMove;
    private bool canJump;
	[SerializeField]
    private Player player;
	[SerializeField]
    private GameController gameController;
    [SerializeField]
    AnimatorController animatorController;


	private Rigidbody rb;

	private Coroutine inputCoroutine = null;

	// lane positions, more could be added
	//IF MORE LANES ARE ADDED UPDATE MIDDLE_LANE MANUALLY!!!
	private float[] lanePositions = new float[] { -1.75f, 0f, 1.75f };

	//todo add get set
    private int currentLaneIndex = 1; // start at the middle lane

    public void Start()
    {
        animatorController = FindObjectOfType<AnimatorController>();
        player = FindObjectOfType<Player>();
		rb =  player.GetComponent<Rigidbody>();
		canMove = true;
    }

    void Update()
    {
        LeftRight();
        Jump();
    }

	public void LeftRight()
	{
		if (Input.GetKeyDown(KeyCode.A) && currentLaneIndex > 0 && canMove && !gameController.is2D && gameController.runSpeed > 1f && gameController.IsRunning())
		{
            animatorController.AnimLeftStrafe();
            StartCoroutine(AnimationTimer());
            inputCoroutine = StartCoroutine(MoveToLane(currentLaneIndex - 1)); // Move to the left lane
			gameController.onLaneChange(currentLaneIndex - 1);
		}
    else if (Input.GetKeyDown(KeyCode.D) && (currentLaneIndex < (lanePositions.Length - 1))  && canMove && !gameController.is2D  && gameController.runSpeed > 1f && gameController.IsRunning())
    {
            animatorController.AnimRightStrafe();
            StartCoroutine(AnimationTimer());
            inputCoroutine = StartCoroutine(MoveToLane(currentLaneIndex + 1)); // Move to the right lane
			gameController.onLaneChange(currentLaneIndex + 1);
		}    
	}

	//these 2 functions are needed to prevent lane bugs when switching 2d <-> 3d
	public void Adjust() => StartCoroutine(CoroutineAdjust());

	public void ResetPlayer()
	{
		if (inputCoroutine != null)
		{
			StopCoroutine(inputCoroutine);
		}
		canMove = true;
		currentLaneIndex = 1;
	}

	IEnumerator CoroutineAdjust()
    {
        yield return new WaitForSeconds(0.25f); //prevents a bug that messes up the lanes
		StartCoroutine(MoveToLane(MIDDLE_LANE));
    }

    //timer to prevent que of animation
    IEnumerator AnimationTimer()
    {
        yield return new WaitForSeconds(0.4f);
        animatorController.AnimSetRightLeftOff();
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
        //player.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (canJump == true) && gameController.getDistance() >= 3f  && gameController.runSpeed > 1f && gameController.IsRunning())
        {
            animatorController.AnimJump();
            Vector3 currentVelocity = rb.velocity;
            rb.velocity = new Vector3(currentVelocity.x, 0, currentVelocity.z);
			if (gameController.is2D)
				rb.AddForce(Vector3.up * jumpForce2d, ForceMode.Impulse);
			else
				rb.AddForce(Vector3.up * jumpForce3d, ForceMode.Impulse);
            canJump = false;
        }
    }

    public void ResetJump()
    {
        canJump = true;
    }

    public void StopJump()
    {
        canJump = false;
    }

    public void UpdatePlayer(Player p)
    {
        player = p;
		//animator = a;
		rb =  player.GetComponent<Rigidbody>();
    }
}
