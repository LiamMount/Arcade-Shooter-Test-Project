using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Flow Stuff")]
    public bool isPaused;
    public bool isCutscene;

    //Player necessities
    [Header("Player Stuff")]
    public int lives = 3;
    public bool gameOver = false;
    //public bool playerIsDead = false;   -   Probably don't need this

    public Rigidbody2D playerPrefab;
    Transform respawnPoint;

    //UI necessities
    [Header("UI Stuff")]
    public bool respawning = false;

    [Header("Level Stuff")]
    public bool altGoalComplete = false;

    //Scorekeeping
    //public int points = 0;

    void Start()
    {
        respawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform;
    }

    void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnPlayerRoutine());
    }

    public IEnumerator RespawnPlayerRoutine()
    {
        respawning = true; //Any UI scripts can enable things

        if (lives > 1)
        {
            yield return new WaitForSeconds(2f);

            lives -= 1;
            Rigidbody2D newPlayer;
            newPlayer = Instantiate(playerPrefab, respawnPoint.position, respawnPoint.rotation);
            respawning = false;
        }
        else
        {
            lives = 0;
            respawning = false;
            gameOver = true; //The level script will take it from here, running a game over script and then exiting to the menu
        }
    }

    //For external level scripts
    public void ForceRespawn()
    {
        Rigidbody2D newPlayer;
        newPlayer = Instantiate(playerPrefab, respawnPoint.position, respawnPoint.rotation);
        respawning = false;
    }
}
