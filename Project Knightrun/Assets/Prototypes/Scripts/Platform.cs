using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Player player;
    public InputController inputController;
    public BoxCollider PlatformCollider;
    private bool isPassable = true;
    private bool isActive = false;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        inputController = FindObjectOfType<InputController>();
        PlatformCollider.enabled = false;
    }

    private void Update()
    {
        CheckHeight();
        GetDown();
    }

    private void CheckHeight()
    {
        if(player.transform.position.y > transform.position.y && !Input.GetKey(KeyCode.S) && !Input.GetKeyDown(KeyCode.S) && isPassable)
        {
            PlatformCollider.enabled = true;
        }
        else
        {
            PlatformCollider.enabled = false;
        }
    }

    private void GetDown()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKey(KeyCode.S))
        {
            PlatformCollider.enabled = false;
            if (isActive)
            {
                isPassable = false;
                inputController.StopJump();
                player.PlayerCollider.enabled = false;
                StartCoroutine(AnimationTimer());
                player.PlayerCollider.enabled = true;
            }
        }
    }

    IEnumerator AnimationTimer()
    {
        yield return new WaitForSeconds(0.1f);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            inputController.ResetJump();
            isActive = true;
        }
    }
}