using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovementTutorial : MonoBehaviour
{
    public QuestManager questManager;
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    public Score scoreScript;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    float starttime = 0;

    public GameObject cnvsRstrt;
    public Animator animator;

    bool Right;
    int index;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
        index = PlayerPrefs.GetInt("Mission");
    }

    private void Update()
    {
        
        int lastRunScore = int.Parse(scoreScript.scoreText.text.ToString());
        questManager.quests[index].currentMeters = lastRunScore;
        starttime += Time.deltaTime;
        if(starttime > 10f && moveSpeed < 10f)
        {
            moveSpeed += 1f;
            starttime = 0;
        }
        else
        {
            starttime += Time.deltaTime;
        }
       
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        SpeedControl();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Tree")
        {
            if(other.gameObject.transform.position.x > transform.position.x)
            {
                animator.SetTrigger("Right");
               // animator.SetBool("LFT", false);
                rb.AddForce(-20f, rb.velocity.y, -30f, ForceMode.Impulse);
                moveSpeed = 3f;

            }

            if(other.gameObject.transform.position.x < transform.position.x)
            {
              //  animator.SetBool("RGHT", false);
                animator.SetTrigger("Left");
                rb.AddForce(20f, rb.velocity.y, -30f, ForceMode.Impulse);
                moveSpeed = 3f;

            }
            
        }

        if(other.gameObject.tag == "Enemy")
        {
            moveSpeed = 0f;
            cnvsRstrt.SetActive(true);
            int lastRunScore = int.Parse(scoreScript.scoreText.text.ToString());
            PlayerPrefs.SetInt("lastRunScore", lastRunScore);
        }
    }

    private void MyInput()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            horizontalInput = Input.acceleration.x;
            if (horizontalInput > 0.3f )
            {
                animator.SetBool("RGHT", true);
                animator.SetBool("LFT", false);
                /*  animator.ResetTrigger("Left");
                  animator.SetTrigger("Right"); */
            }
            if (horizontalInput < -0.3f )
            {
                
                animator.SetBool("RGHT", false);
                animator.SetBool("LFT", true);
              /*  animator.ResetTrigger("Right");
                animator.SetTrigger("Left"); */
                
            }
            else if(horizontalInput == 0)
            {
                animator.SetBool("LFT", false);
                animator.SetBool("RGHT", false);
                /* animator.ResetTrigger("Right");
                 animator.ResetTrigger("Left"); */
            }
        }
        else
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            if (horizontalInput > 0 )
            {
                animator.SetBool("RGHT", true);
                animator.SetBool("LFT", false);
                /*  animator.ResetTrigger("Left");
                  animator.SetTrigger("Right"); */
            }
            if (horizontalInput < 0 )
            {
                
                animator.SetBool("RGHT", false);
                animator.SetBool("LFT", true);
              /*  animator.ResetTrigger("Right");
                animator.SetTrigger("Left"); */
                
            }
            else if(horizontalInput == 0)
            {
                animator.SetBool("LFT", false);
                animator.SetBool("RGHT", false);
                /* animator.ResetTrigger("Right");
                 animator.ResetTrigger("Left"); */
            }

        }

        

        verticalInput = moveSpeed;//Input.GetAxisRaw("Vertical");

        // when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}