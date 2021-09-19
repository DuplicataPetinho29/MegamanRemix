﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{

    private ControlaPer thePlayer;

    private Health saude;

    [SerializeField]
    Transform player;

    [SerializeField]
    Transform GroundCheckPoint;

    [SerializeField]
    float circleRadius;

    [SerializeField]
    LayerMask GroundLayer;

    private bool checkingGround;

    public float moveSpeed;

    public float playerRange;

    public float playerAtq;

    public bool playerInAtq;

    public LayerMask playerLayer;

    public bool playerInRange;

    public bool dam;

    [SerializeField]
    int saud = 4;


    Rigidbody2D rigidbody2d;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<ControlaPer>();

        saude = FindObjectOfType<Health>();

        rigidbody2d = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            //  playerInRange = true;
            chasePlayer();
            if (playerInAtq)
            {
                dano();
                moveSpeed = 0;
                animator.SetBool("CORRENDO", false);
                animator.SetTrigger("ATQ");
            }
            else
            {
                barra();
                transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            animator.SetBool("CORRENDO", false);
            moveSpeed = 0;
        }

    }

    private void FixedUpdate() // verifica colosoes 
    {

        playerInAtq = Physics2D.OverlapCircle(transform.position, playerAtq, playerLayer);

        playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);

        checkingGround = Physics2D.OverlapCircle(GroundCheckPoint.position, circleRadius, GroundLayer);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, playerRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerAtq);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(GroundCheckPoint.position, circleRadius);


    }

    void barra() // verifica chao 
    {
        if (!checkingGround)
        {
            animator.SetBool("CORRENDO", false);
            moveSpeed = 0;
        }
        else
        {
            animator.SetBool("CORRENDO", true);
            moveSpeed = 1;
        }



    }

    void chasePlayer() // muda a direção em que o personagem olha 
    {
        if (transform.position.x < player.position.x)
        {
            rigidbody2d.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            rigidbody2d.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
        }
    }

    void dano() // dano no boss
    {
        if (thePlayer.atk)
        {
            saud = saud - 1;
        }

        if (saud <= 0)
        {
            animator.SetTrigger("DAN");
        }
    }
}
