﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaPer : MonoBehaviour
{
    public LayerMask layermascara; // para quais layers eu vou verificar a colisao
    public LayerMask enemyLayers;

    private Boss2 boss;

    private Health saude;

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    Transform bulletSpawnPos;

    [SerializeField]
    private float shootDelay = 0.5f;

    [SerializeField]
    Transform attackPoint;

    [SerializeField]
    private float attackRange = 0.5f;

    private bool isShooting;
    private Rigidbody2D rb;

    Animator animator;
    Vector3 diferenca;

    const float RAIO = 0.15f;
    bool isSpriteLeft;

    public int attackDamage = 2;
    public bool atk; // que isso?

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        diferenca = new Vector3(-0.15f, 0.80f, 0);
        boss = FindObjectOfType<Boss2>();
        saude = FindObjectOfType<Health>();
    }
    void Update()
    {
        dano();
        corre();
        pula();
        atq();
        shoot(); 

    }

    private void corre()
    {
        float horz = Input.GetAxis("Horizontal");

        if (horz != 0)
        {
            animator.SetBool("CORRENDO", true);
            transform.Translate(3f * Time.deltaTime * horz, 0, 0); // faz personagem andar 
            if (horz < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); // vira a sprite
                isSpriteLeft = true; // Para não bugar o tiro da personagem
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
                isSpriteLeft = false;
            }

        }
        else
        {
            animator.SetBool("CORRENDO", false);
        }
    }

    private void pula()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, 7.5f), ForceMode2D.Impulse); // para valores quebrados colocar "f" do lado
            animator.SetTrigger("PULAR");
            animator.SetBool("NOCHAO", true);
        }
    }

    private void shoot()
    {
        if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.LeftControl)) // a condição só vai ser verdadeira se pressionar o botão do mouse
        {
            if (isShooting) return;// para a animação e o tiro não ficarem infinitos ~ Vai verificar a condição abaixo, se ela ñ for verdadeira, vai fazer a verificação no resetshoot

            animator.Play("PlayerShoot");
            isShooting = true;

            GameObject b = Instantiate(bullet);
            b.GetComponent<BulletScriptPer>().StartShoot(isSpriteLeft);
            b.transform.position = bulletSpawnPos.transform.position; // faz com que a posição do tiro saia do mesmo lugar que o spawn

            Invoke("ResetShoot", shootDelay); // resetar a animação sem precisar fazer a teia de aranha
        }
    }


    private void FixedUpdade()
    {
        Collider2D[] colisoes = Physics2D.OverlapCircleAll(transform.position - diferenca, 20f, layermascara);
        if (colisoes.Length == 0)
            animator.SetBool("NOCHAO", false);
        else
            animator.SetBool("NOCHAO", true);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position - diferenca, RAIO);

        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void atq() // ataque do personagem
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            animator.Play("chutando");
           Collider2D[] hitEnemies =  Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach(Collider2D enemy in hitEnemies)
            {
                
            }
            
        }
        
    }
    private void ResetShoot() // reseta a animação
    {
        isShooting = false;
        animator.Play("Player");
    }

    void dano() // dano no personagem 
    {

        if (boss.playerInAtq)
        {
            animator.SetTrigger("DAN");
            saude.health = saude.health- 1; // diminui tudo de uma vez ( bugado )
        }
        
    }

}

