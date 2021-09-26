using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoInteracao : MonoBehaviour
{
    Animator animator;
    private int health = 2;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            health--;
            animator.Play("CaixaFire");

            if (health <= 0)
                Destroy(gameObject);
        }
    }
}
