﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed, jumpForce;
    private float jumpHeight = .4f;
    private float moveInput;

    private Rigidbody2D myBody;
    private Animator anim;

    public Transform groundCheck;
    public LayerMask groundLayer;

    private bool facingRight = true;

    private Vector3 range;

    public AudioClip[] footStepClips;
    public AudioClip jumpClip;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Movement();
        CheckCollsionForJump();
    }

    void Movement()
    {
        moveInput = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (anim.GetBool("SwordAttack")) moveInput = 0;

        anim.SetFloat("Speed", Mathf.Abs(moveInput));

        myBody.velocity = new Vector2(moveInput, myBody.velocity.y);

        if (Input.GetKeyUp(KeyCode.Space))
        { 
            if(myBody.velocity.y > 0)
            {
                myBody.velocity = new Vector2(myBody.velocity.x, myBody.velocity.y * jumpHeight);
            }
        }

        if (moveInput > 0 && !facingRight || moveInput < 0 && facingRight)
            Flip();

        if (myBody.velocity.y < 0)
            anim.SetBool("Fall", true);
        else
            anim.SetBool("Fall", false);
    }

    void CheckCollsionForJump()
    {
        Collider2D bottomHit = Physics2D.OverlapBox(groundCheck.position, range, 0, groundLayer);

        if(bottomHit != null)
        {
            if(bottomHit.gameObject.tag == "Ground" && Input.GetKeyDown(KeyCode.Space))
            {
                myBody.velocity = new Vector2(myBody.velocity.x, jumpForce);
                SoundManager.instance.PlaySoundFx(jumpClip, .2f);
                anim.SetBool("Jump", true);
            }
            else
            {
                anim.SetBool("Jump", false);
            }
        }

    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 transformScale = transform.localScale;
        transformScale.x *= -1;
        transform.localScale = transformScale;
    }

    void RunningSound()
    {
        SoundManager.instance.PlayRandomSoundFx(footStepClips);
    }

}