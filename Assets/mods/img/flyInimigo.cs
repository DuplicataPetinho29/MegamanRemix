using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyInimigo : MonoBehaviour
{

    private ControlaPer thePlayer;

    [SerializeField]
    Transform player;

    [SerializeField]
    LayerMask GroundLayer;


    public float moveSpeed;

    public float playerRange;

    public LayerMask playerLayer;

    public bool playerInRange;

    public float ground;

    public bool InGround;

    private int health = 10;





    Rigidbody2D rigidbody2d;
    Animator animator;




    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<ControlaPer>();

        rigidbody2d = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);

        if (playerInRange)
        {
            playerInRange = true;
            moveSpeed = 1;
            transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, moveSpeed * Time.deltaTime);
            chasePlayer();

        }
        else
        {

            moveSpeed = 0;
            chasePlayer();

        }


    }
    private void FixedUpdate()
    {
        playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);

        InGround = Physics2D.OverlapCircle(transform.position, ground, GroundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, playerRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, ground);
    }




    void chasePlayer()
    {
        if (transform.position.x < player.position.x)
        {
            rigidbody2d.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            rigidbody2d.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            health--;
            animator.Play("DanoFlyEnemy");

        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
