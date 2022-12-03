using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    GameManager gm;

    public Transform standardShootPoint;

    public Rigidbody2D standardBullet;
    public float standardBulletSpeed;

    private float shootTimer = 0f;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (!gm.isPaused && !gm.isCutscene)
        {
            ShootingLogic();
        }
    }

    public void ShootingLogic()
    {
        if (shootTimer <= 0)
        {
            GetFireInput();
        }
        else
        {
            shootTimer -= Time.deltaTime;
        }
    }

    public void GetFireInput()
    {
        if (Input.GetButton("Fire1"))
        {
            // Add checks for bullet types later

            FireStandard();
            shootTimer = 0.25f;
        }
    }

    public void FireStandard()
    {
        Rigidbody2D bulletClone = (Rigidbody2D)Instantiate(standardBullet, standardShootPoint.transform.position, standardShootPoint.transform.rotation);
        bulletClone.velocity = transform.up * standardBulletSpeed;
    }
}
