using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    private ControlaPer thePlayer;

    [SerializeField]
    Transform player;

    public float Range; // alcance do tiro

    public Transform Target; // localizar o player

    bool Detected = false; // Vai atirar se detectar o player

    Vector2 Direction; // Direção

    public GameObject Rotação; // Acompanhar o player (Cima, baixo, esquerda, direita e diagonais)

    public GameObject Bullet;

    public float FireRate;
    float nextTimeToFire = 0; // intervalo dos tiros

    public Transform ShootPoint;

    public float Force;

    Animator animator;

    void Start()
    {
        thePlayer = FindObjectOfType<ControlaPer>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetPos = Target.position; // posição do alvo

        Direction = targetPos - (Vector2)transform.position;

        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, Direction, Range); //Raycast por conta da fisica entre o tiro e a personagem. Há uma relação entre a distância

        if (rayInfo)
        {
            if (rayInfo.collider.gameObject.tag == "Player") // verificar a colisão
            {
                if (Detected == false)
                {
                    Detected = true;
                    chasePlayer();
                    animator.SetBool("Fire", true);

                }

            }
            else
            {
                if (Detected == true)
                {
                    Detected = false;
                    chasePlayer();
                    animator.SetBool("Fire", false);

                }
            }
        }

        if (Detected)
        {
            Rotação.transform.up = Direction; // Clonar o tiro e seguir o player
            if (Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / FireRate;
                shoot();
            }

        }
    }

    void shoot() // Força do tiro
    {
        GameObject BulletIns = Instantiate(Bullet, ShootPoint.position, Quaternion.identity);
        BulletIns.GetComponent<Rigidbody2D>().AddForce(Direction * Force);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }

    void chasePlayer() // Vira a sprite
    {
        if (transform.position.x < player.position.x)
        {
            transform.localScale = new Vector2(2, 2);
        }
        else
        {
            transform.localScale = new Vector2(-2, 2);
        }
    }
}
