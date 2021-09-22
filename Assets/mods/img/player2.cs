using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class player2 : MonoBehaviour
{
    //variáveis criadas no escopo da classe, são chamadas de campos(fields)

    public LayerMask layerMascara;//para quais layer eu vou verificar a colisao
    public LayerMask enemyLayers;
    public Vector3 diferenca;
    Rigidbody2D rb;
    Animator animator;
    Vector3 inicio;
    int pulos;

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

    bool isSpriteLeft;

    public int attackDamage = 2;



    float alturaAnterior;
    const int PULOS = 2;
    const float RAIO = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        inicio = gameObject.transform.position;
        pulos = PULOS;

        boss = FindObjectOfType<Boss2>();

        saude = FindObjectOfType<Health>();


    }



    // Update is called once per frame
    void Update()
    {

        dano();
        corre();
        pula();
        atq();
        shoot();

    }

    void corre()
    {
        float horz = Input.GetAxis("Horizontal");



        if (horz != 0)
        {
            GetComponent<Animator>().SetBool("CORRENDO", true);
            transform.Translate(3f * Time.deltaTime * horz, 0, 0);//faz o personagem andar
            if (horz < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);//vira a sprite
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
            GetComponent<Animator>().SetBool("CORRENDO", false);
        }
    }

    void pula()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (animator.GetBool("NOCHAO") || pulos > 1))
        {
            rb.AddForce(new Vector2(0, 7f), ForceMode2D.Impulse);
            animator.SetTrigger("PULAR");
            animator.SetBool("NOCHAO", false);
            pulos -= 1;//pulos--
        }



        animator.SetBool("CAINDO", transform.position.y - alturaAnterior < 0 &&
        !animator.GetBool("NOCHAO"));
        alturaAnterior = transform.position.y;//guarda altura atual
    }



    private void FixedUpdate()
    {



        Collider2D[] colisoes = Physics2D.OverlapCircleAll(transform.position - diferenca,
        RAIO,
        layerMascara);
        if (colisoes.Length == 0)
            animator.SetBool("NOCHAO", false);
        else
        {
            animator.SetBool("NOCHAO", true);
            pulos = PULOS;
        }



    }
    //Isso aqui é só para debug!!!!
    //void OnDrawGizmos()//desenha o tempo inteiro
    void OnDrawGizmosSelected()//desenha apenas quando o gameObject for selecionado na hierarquia
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position - diferenca, RAIO);

        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
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

    void atq() // ataque do personagem
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            animator.Play("chutando");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
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
            saude.health = saude.health - 1; // diminui tudo de uma vez ( bugado )
        }

    }
}