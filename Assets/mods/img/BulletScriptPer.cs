using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScriptPer : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    int damage;

    [SerializeField]
    float timeToDestroy = 2;

    public void StartShoot(bool isSpriteLeft)
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();

        if (isSpriteLeft)
        {
            rb2d.velocity = new Vector2(-speed, 0);
        }
        else
        {
            rb2d.velocity = new Vector2(speed, 0);
        }

        Destroy(gameObject, timeToDestroy);
    }
}
