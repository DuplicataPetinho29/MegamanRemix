using System.Collections;
using System.Collections;
using UnityEngine;

public class flyInimigo : MonoBehaviour
{

    private ControlaPer thePlayer;

    [SerializeField]
    Transform player;

    [SerializeField]
    float agroRange;

    Rigidbody2D rigidbody2d;

    public float moveSpeed;

    public float playerRange;

    public LayerMask playerLayer;

    public bool playerInrange;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<ControlaPer>();

        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, moveSpeed * Time.deltaTime);
        chasePlayer();
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
            transform.localScale = new Vector2(-3, 3);
        }
        else
        {
            rigidbody2d.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(3, 3);
        }
    }
}