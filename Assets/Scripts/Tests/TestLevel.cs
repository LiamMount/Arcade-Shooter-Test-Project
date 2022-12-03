using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevel : MonoBehaviour
{
    GameManager gm;

    [Header("Level Tracking")]
    public string checkpoint = "Start";
    public bool inProgress = false;
    public int stocksLost = 0;

    [Header("Death Screen Anim")]
    public Animator fadeAnim;

    [Header("Dialogue")]
    public Conversation testConvo1;
    public Conversation testConvo2;
    public Conversation testConvo3;

    [Header("Enemies")]
    public GameObject testEnemyPrefab;
    public GameObject testBossPrefab;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        DeathLogic();

        WavesLogic();
    }

    public void DeathLogic()
    {
        //When you lose a stock
        if (gm.gameOver && inProgress)
        {
            inProgress = false;
            StartCoroutine(StockReset());
        }

        //When you start a stock
        if (!inProgress && !gm.gameOver)
        {
            inProgress = true;
            if (checkpoint == "Start")
            {
                FirstWave(); //First wave is only called in death logic, is only needed there
            }
            else if (checkpoint == "Double")
            {
                SecondWave();

                //NOTE
                //We can also add different dialogue here to taunt players.
                DialogueManager.StartConversation(testConvo1);
            }
            else if (checkpoint == "Boss")
            {
                BossWave();

                //NOTE
                //We can also add different dialogue here to taunt players.
                DialogueManager.StartConversation(testConvo2);
            }
        }
    }

    public void WavesLogic()
    {
        // NOTE
        //Waves have to go from the bottom up due to priority issues

        //When you finish a wave, and it clears bullets and starts a new one
        if (inProgress && !gm.gameOver && (GameObject.FindGameObjectsWithTag("EnemyShip").Length <= 0 && gm.altGoalComplete))
        {
            gm.altGoalComplete = false;

            //From wave 3 to done
            if (checkpoint == "Boss")
            {
                checkpoint = "Done";
                
                BulletClear();
                DialogueManager.StartConversation(testConvo3);
                //Can start whatever coroutine we want to destroy the boss, if any
            }

            //From wave 2 to 3
            if (checkpoint == "Double")
            {
                checkpoint = "Boss";
                BossWave();

                BulletClear();
                DialogueManager.StartConversation(testConvo2);
            }

            //From wave 1 to 2
            if (checkpoint == "Start")
            {
                checkpoint = "Double";
                SecondWave();

                BulletClear();
                DialogueManager.StartConversation(testConvo1);
            }
        }
    }

    // WAVES

    public void FirstWave()
    {
        for (int i = 0; i < 5; i += 1)
        {
            Instantiate(testEnemyPrefab, new Vector2(Random.Range(-8f, 8f), Random.Range(10.5f, 14.5f)), Quaternion.identity);
        }

        gm.altGoalComplete = true; //No alt objective
    }

    public void SecondWave()
    {
        for (int i = 0; i < 10; i += 1)
        {
            Instantiate(testEnemyPrefab, new Vector2(Random.Range(-8f, 8f), Random.Range(10.5f, 14.5f)), Quaternion.identity);
        }

        gm.altGoalComplete = true; //No alt objective
    }

    public void BossWave()
    {
        gm.altGoalComplete = false; //Has alt objective

        Instantiate(testBossPrefab, new Vector2(Random.Range(-8f, 8f), Random.Range(10.5f, 14.5f)), Quaternion.identity);
    }

    // END WAVES

    public IEnumerator StockReset()
    {
        stocksLost += 1;

        //Fade to stock lost screen
        fadeAnim.SetBool("isOn", true);
        yield return new WaitForSeconds(1f);

        gm.lives = 3;

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("EnemyShip"))
        {
            Destroy(enemy);
        }
        foreach (GameObject boss in GameObject.FindGameObjectsWithTag("BossShip"))
        {
            Destroy(boss);
        }
        foreach (GameObject enemyBullet in GameObject.FindGameObjectsWithTag("EnemyBullet"))
        {
            Destroy(enemyBullet);
        }
        foreach (GameObject playerBullet in GameObject.FindGameObjectsWithTag("PlayerBullet"))
        {
            Destroy(playerBullet);
        }

        yield return new WaitForSeconds(1f);
        //Fade back
        fadeAnim.SetBool("isOn", false);

        gm.ForceRespawn();
        gm.gameOver = false;

        yield return new WaitForSeconds(0.5f);
    }

    public void BulletClear()
    {
        foreach (GameObject enemyBullet in GameObject.FindGameObjectsWithTag("EnemyBullet"))
        {
            enemyBullet.GetComponent<BulletTypesEnemy>().HitFX();
        }
        foreach (GameObject playerBullet in GameObject.FindGameObjectsWithTag("PlayerBullet"))
        {
            playerBullet.GetComponent<BulletTypes>().HitFX();
        }
    }
}
