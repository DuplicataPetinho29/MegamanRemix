using System.Collections;
using System.Collections;
using UnityEngine;

public class flyInimigo : MonoBehaviour
{

    private ControlaPer thePlayer;

    public float moveSpeed;

    public float playerRange;

    public LayerMask playerLayer;

    public bool playerInrange;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<ControlaPer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, moveSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, playerRange);
    }
}