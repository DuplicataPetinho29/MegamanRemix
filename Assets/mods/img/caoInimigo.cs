using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caoInimigo : MonoBehaviour
{

    private ControlaPer thePlayer;

    private Health saude;

    [SerializeField]
    Transform player;

    public float moveSpeed;

    public float playerRange;

    public LayerMask playerLayer;

    public bool playerInRange;

    public bool dam;

    Rigidbody2D rigidbody2d;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<ControlaPer>();

        rigidbody2d = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        saude = FindObjectOfType<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);

        if (playerInRange)
        {
            playerInRange = true;
            animator.SetBool("CORRENDO", true);
            moveSpeed = 1;
            transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, moveSpeed * Time.deltaTime);
            chasePlayer();
            dano();
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, playerRange);
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

    //  void OnTriggerEnter2D(Collider2D trig)
    //  {
    //      if (trig.gameObject.CompareTag("parede"))
    //      {
    //          animator.SetBool("correndo", false);
    //
    //      }
    //      else
    //      {
    //          animator.SetBool("correndo", true);
    //      }
    //  }

    void dano()
     {
         if (transform.position.x == player.position.x)
         {
             dam = true;
             player.position = new Vector3(-5.2f, -0, 24f);
             saude.health = saude.health - 1;
        }
         else
             dam = false;
     }
}