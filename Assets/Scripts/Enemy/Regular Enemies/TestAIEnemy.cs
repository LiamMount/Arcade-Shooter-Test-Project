using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAIEnemy : MonoBehaviour
{
    GameManager gm;

    //This is gonna be a basic enemy type that picks a point and hovers to it
    //Just like the old melee VR game test we did

    //Start port
    [Header("Ship bits")]
    public BoxCollider2D boxCol; //Ported over
    public Transform standardShootPoint;

    public float health; // Ported over
    private float invulnTimer; //Ported over
    private bool invulnerable; //Ported over

    private Animator animator; //Test

    //VFX for death and whatnot
    //Also any necessary code
    //INSERT LATER
    //End port

    [Header("Bullet bits")]
    public Rigidbody2D standardBullet;
    public float standardBulletSpeed;

    private float shootTimer = 3f;

    [Header("Aiming bits")]
    public GameObject target;
    public PolygonCollider2D detectorRange;
    private bool targetFound;

    // Movement stuff
    private Vector2 stagePos;
    private float stageTimer;
    public float speed = 1f;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();

        animator = GetComponent<Animator>();

        shootTimer = Random.Range(2f, 3.5f);

        TargetPlayer();
        LocationDecide();
    }

    void Update()
    {
        if (!gm.isPaused && !gm.isCutscene)
        {
            AILogic();
            Movement();

            InvulnFrames(); //Ported over
        }
    }

    //Health mechanics
    public void InvulnFrames() //Ported over
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

    public void GetHurt(float damage) //Ported over
    {
        if (damage >= health)
        {
            Destroy(this.gameObject);
        }
        else
        {
            //Take damage
            health -= damage;

            invulnerable = true;
            invulnTimer = 0.15f;

            animator.SetBool("invulnerable", true); //Test

            Debug.Log("Enemy took damage");
        }
    }



    //Notes:
    //Y Range: 0.5f to 4.5f
    //X Range: -8f to 8f

    // AI Thinking
    public void AILogic()
    {
        // Player targeting
        if (target == null)
        {
            TargetPlayer();
        }
        
        // Shooting timer
        if (shootTimer <= 0)
        {
            FireStandard();
        }
        else
        {
            shootTimer -= Time.deltaTime;
        }

        // Movement thinking
        if (stageTimer <= 0)
        {
            LocationDecide();
        }
        else
        {
            stageTimer -= Time.deltaTime;
        }
    }

    // Moving
    public void LocationDecide()
    {
        stageTimer = 5f;

        Vector2 potentialPos = new Vector2(0, 0);

        bool loopAgain = true;
        int tryCount = 0;
        while (loopAgain == true && tryCount < 5)
        {
            loopAgain = false;
            tryCount += 1;
            potentialPos = new Vector2(Random.Range(-8f, 8f), Random.Range(0.5f, 4.5f));
            Collider2D[] otherEnemies = Physics2D.OverlapBoxAll(potentialPos, new Vector2(1f, 1f), 0);
            foreach (Collider2D enemy in otherEnemies)
            {
                if (enemy.gameObject.tag == "EnemyShip")
                {
                    loopAgain = true;
                }
            }

            if (loopAgain && tryCount >= 0)
            {
                potentialPos = stagePos;
            }
        }

        // Found position
        stagePos = potentialPos;
    }

    public void Movement()
    {
        float interpolation = speed * Time.deltaTime;

        if (interpolation >= 0.009)
        {
            interpolation = 0.009f;
        }

        Vector3 position = this.transform.position;
        position.y = Mathf.Lerp(this.transform.position.y, stagePos.y, interpolation);
        position.x = Mathf.Lerp(this.transform.position.x, stagePos.x, interpolation);

        this.transform.position = position;
    }

    // Shooting
    public void TargetPlayer()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    public void FireStandard()
    {
        shootTimer = 1.5f;

        //Check player position
        if (targetFound) // Cone Trigger
        {
            Rigidbody2D bulletClone = (Rigidbody2D)Instantiate(standardBullet, standardShootPoint.transform.position, standardShootPoint.transform.rotation);
            Vector2 targetLine = (this.transform.position - target.transform.position).normalized;
            
            bulletClone.velocity = (-targetLine) * standardBulletSpeed;
        }

        else
        {
            Rigidbody2D bulletClone = (Rigidbody2D)Instantiate(standardBullet, standardShootPoint.transform.position, standardShootPoint.transform.rotation);
            bulletClone.velocity = new Vector2(0, -1) * standardBulletSpeed;
        }
    }

    //Targeting triggers
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            targetFound = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            targetFound = false;
        }
    }



    //Health collisions
    private void OnCollisionEnter2D(Collision2D collision) //Ported over
    {
        //Active game
        if (!invulnerable && !gm.isCutscene)
        {
            if (collision.gameObject.tag == "PlayerBullet")
            {
                BulletTypes collidedBullet = collision.gameObject.GetComponent<BulletTypes>();
                GetHurt(collidedBullet.damage);
                collidedBullet.HitFX();
            }
            else if (collision.gameObject.tag == "Player")
            {
                CrashIntoPlayer();
            }
        }

        //Cutscene or I-frames
        else if (invulnerable || gm.isCutscene)
        {
            if (collision.gameObject.tag == "PlayerBullet")
            {
                BulletTypes collidedBullet = collision.gameObject.GetComponent<BulletTypes>();
                collidedBullet.HitFX();
            }
            else if (collision.gameObject.tag == "Player")
            {
                CrashIntoPlayer();
            }
        }
    }

    //Ship breaking code (Insta-kill)
    public void CrashIntoPlayer() //Ported over
    {
        //Spawn VFX <--
        Destroy(this.gameObject);
    }
}
