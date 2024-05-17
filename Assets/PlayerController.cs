using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float angleSpeed;
    [SerializeField] Camera playerCamera;

    float moveX, moveZ;
    Rigidbody rigigBody;
    Animator animator;

    bool onGround;
    bool isRunning;
    bool isJumping;

    void Start()
    {
        rigigBody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        if(moveZ != 0)
        {
            animator.SetBool("isWalking", true);
        } else
        {
            animator.SetBool("isWalking", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
            speed *= 2;
            animator.SetBool("isRunning", isRunning);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
            speed /= 2;
            animator.SetBool("isRunning", isRunning);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            onGround = false;
            isJumping = true;
            animator.SetTrigger("jumped");
        }
    }

    void FixedUpdate()
    {
        DetectGround();
        MovePlayer();
    }

    void MovePlayer()
    {
        if (!onGround) return;

        rigigBody.velocity = transform.forward * moveZ * speed;

        rigigBody.MoveRotation(rigigBody.rotation
            * Quaternion.Euler(0f, angleSpeed * moveX, 0f));

        if(isJumping)
        {
            rigigBody.AddForce(Vector3.up * speed, ForceMode.Force);
        }
    }

    void DetectGround()
    {
        RaycastHit hit;
        if(Physics.Raycast(this.transform.position, Vector3.down, out hit, 1.5f))
        {
            onGround = true;
        } else
        {
            onGround = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position,
            transform.position + Vector3.down * 1.2f) ;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
            isJumping = false;
        }
    }
}
