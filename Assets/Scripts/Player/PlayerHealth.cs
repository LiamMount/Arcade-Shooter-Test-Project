using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    GameManager gm;

    public BoxCollider2D boxCol;

    public float health;
    private float invulnTimer;
    private bool invulnerable;

    private Animator animator; //Test

    //Extra VFX and whatnot
    //INSERT LATER

    void Start()
    {
        gm = FindObjectOfType<GameManager>();

        animator = GetComponent<Animator>();

        invulnerable = true;
        invulnTimer = 1f;

        if (gm.lives < 3)
        {
            animator.SetBool("invulnerable", true); //Test
        }
    }

    void Update()
    {
        if (!gm.isPaused && !gm.isCutscene)
        {
            InvulnFrames();
        }
    }

    public void InvulnFrames()
    {
        if (invulnTimer <= 0)
        {
            invulnerable = false;
            animator.SetBool("invulnerable", false); //Test
        }
        else
        {
            invulnTimer -= Time.deltaTime;
        }
    }

    public void GetHurt()
    {
        health -= 1;

        //End run somehow if health reaches 0
        if (health <= 0)
        {
            //Spawn death VFX
            //Play death sound (Make the death VFX do it since this will die)

            Debug.Log("Dead :(");
            gm.RespawnPlayer();
            Destroy(this.gameObject);
        }

        invulnerable = true;
        invulnTimer = 0.25f;

        animator.SetBool("invulnerable", true); //Test

        Debug.Log("Get dinged");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!invulnerable && !gm.isCutscene)
        {
            if (collision.gameObject.tag == "EnemyBullet")
            {
                GetHurt();
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.tag == "EnemyShip")
            {
                GetHurt();
            }
            else if (collision.gameObject.tag == "BossShip")
            {
                health = 1; //Set health to one so this insta-kills
                GetHurt();
            }
        }
        else if (invulnerable || gm.isCutscene)
        {
            if (collision.gameObject.tag == "EnemyBullet")
            {
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.tag == "EnemyShip")
            {
                //Make enemy ship crash
                //collision.gameObject.GetComponent<EnemyHealth>().CrashIntoPlayer(); Old
            }
            else if (collision.gameObject.tag == "EnemyShip")
            {
                //Make enemy ship crash
                //collision.gameObject.GetComponent<EnemyHealth>().CrashIntoPlayer(); Old
            }
        }
    }
}
