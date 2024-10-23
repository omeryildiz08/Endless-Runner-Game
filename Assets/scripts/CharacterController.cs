using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 5f;
    public float strafeSpeed = 20f;
    private Rigidbody rb;
    private Animator anim;

    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;
    private bool canDoubleJump;
    public SpawnManager sm;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        rb.constraints = RigidbodyConstraints.FreezeRotation; // Sadece pozisyon hareketi için
    }

    private void Update()
    {

        RunForward();
        
        CheckGroundStatus();
        HandleJump();

    }
    private void RunForward()
    {

        Vector3 forwardMovement = new Vector3(0, 0, speed * Time.deltaTime);
        transform.Translate(forwardMovement);

        // Karakterin X ekseninde sağa-sola hareket etmesi (strafe)
        float moveHorizontal = Input.GetAxis("Horizontal") * strafeSpeed;
        Vector3 strafeMovement = new Vector3(moveHorizontal, 0, 0);
        transform.Translate(strafeMovement * Time.deltaTime);
        anim.SetBool("isMoving", true);
    }




    private void CheckGroundStatus()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);

        if (isGrounded)
        {
            canDoubleJump = true; // Reset double jump when grounded
            anim.SetBool("isJumping", false);
            anim.SetBool("Landed", true);
        }
        else
        {
            anim.SetBool("Landed", false);
        }
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                Jump();
            }
            else if (canDoubleJump)
            {
                Jump();
                canDoubleJump = false; // Disable further jumping in the air
            }
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Reset vertical velocity
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        anim.SetBool("isJumping", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnTrigger"))
        {
            sm.SpawnTriggerEntered();
        }
        else if (other.CompareTag("GoldTrigger"))
        {
            sm.SpawnGoldTrigger();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            FindObjectOfType<GameManager>().GameOver();
        }
    }
}
