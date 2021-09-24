using System.Collections;
using System.Collections;
using UnityEngine;

public class flyInimigo : MonoBehaviour
{

    private ControlaPer thePlayer;

    private Health saude;

    [SerializeField]
    Transform player;

    [SerializeField]
    float agroRange;

    Rigidbody2D rigidbody2d;

    public float moveSpeed;

    public float playerRange;

    public bool dam;

    public LayerMask playerLayer;

    public bool playerInRange;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<ControlaPer>();

        saude = FindObjectOfType<Health>();

        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);

        if (playerInRange)
        {
            playerInRange = true;
            moveSpeed = 1;
            transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, moveSpeed * Time.deltaTime);
            chasePlayer();
            dano();
        }
        else
        {
            //playerInRange = false;
            moveSpeed = 0;
            chasePlayer();
        }
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
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            rigidbody2d.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
        }
    }

    void dano()
    {
        if (transform.position.x == player.position.x)
        {
            dam = true;
            player.position = new Vector3(-2f, -3f);
            saude.health = saude.health - 1;
        }
        else
            dam = false;
    }
}