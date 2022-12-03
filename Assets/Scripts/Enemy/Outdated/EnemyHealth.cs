using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    GameManager gm;

    public BoxCollider2D boxCol; //Check

    public float health; //Check

    private float invulnTimer; //Check
    private bool invulnerable; //Check

    public bool crashed = false; //Check

    //VFX for death and whatnot
    //Also any necessary code
    //INSERT LATER

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
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
        }
        else
        {
            invulnTimer -= Time.deltaTime;
        }
    }

    public void GetHurt(float damage)
    {
        if (damage >= health)
        {
            Destroy(this.gameObject);
        }
        else
        {
            //Take damage
            health -= damage;
            Debug.Log("Enemy took damage");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!invulnerable && !gm.isCutscene)
        {
            invulnerable = true;
            invulnTimer = 0.15f;

            if (collision.gameObject.tag == "PlayerBullet")
            {
                BulletTypes collidedBullet = collision.gameObject.GetComponent<BulletTypes>();
                GetHurt(collidedBullet.damage);
                collidedBullet.HitFX();
            }
        }
        else if (invulnerable || gm.isCutscene)
        {
            if (collision.gameObject.tag == "PlayerBullet")
            {
                BulletTypes collidedBullet = collision.gameObject.GetComponent<BulletTypes>();
                collidedBullet.HitFX();
            }
        }
    }

    //Ship breaking code
    public void CrashIntoPlayer()
    {
        crashed = true;
        Debug.Log("Enemy crashed");
    }
}
