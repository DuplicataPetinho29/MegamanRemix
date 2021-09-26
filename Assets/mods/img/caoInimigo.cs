using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caoInimigo : MonoBehaviour
{

    private ControlaPer thePlayer;

    [SerializeField]
    Transform player;

    [SerializeField]
    Transform GroundCheckPoint;

    [SerializeField]
    Transform WallCheckPoint;

    [SerializeField]
    float circleRadius;

    [SerializeField]
    LayerMask GroundLayer;

    private bool checkingGround;
    private bool chekingWall;

    public float moveSpeed;

    public float playerRange;

    public LayerMask playerLayer;

    public bool playerInRange;

    public bool dam;

    Rigidbody2D rigidbody2d;

    Animator animator;

    private int health = 10;
    public int maxHealth = 100;
    int currrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<ControlaPer>();

        rigidbody2d = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        currrentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            playerInRange = true;
            barra();
            transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, moveSpeed * Time.deltaTime);
            chasePlayer();
            moveSpeed = 1;
        }
        else
        {
            animator.SetBool("CORRENDO", false);
            moveSpeed = 0;
            chasePlayer();
            // dano();
        }
        // dano();
    }

    private void FixedUpdate() //
    {

        playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);

        checkingGround = Physics2D.OverlapCircle(GroundCheckPoint.position, circleRadius, GroundLayer);
        chekingWall = Physics2D.OverlapCircle(WallCheckPoint.position, circleRadius, GroundLayer);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, playerRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(GroundCheckPoint.position, circleRadius);
        Gizmos.DrawSphere(WallCheckPoint.position, circleRadius);

    }

    void barra()
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
        if (chekingWall)
        {
            animator.SetTrigger("PULA");
            rigidbody2d.AddForce(Vector2.up * 800f);
        }


    }




    void chasePlayer()
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



    // void dano()
    // {
    //     if (transform.position.x == player.position.x)
    //     {
    //         dam = true;
    //         player.position = new Vector3(-5.2f, -0, 24f);
    //     }
    //     else
    //         dam = false;
    // }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            health--;
            animator.Play("LoboEnemyDanoFire");

            if (health <= 0)
                Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        currrentHealth -= damage;
        animator.Play("LoboEnemyDano");

        if (currrentHealth <= 0)
            Destroy(gameObject);
    }

}