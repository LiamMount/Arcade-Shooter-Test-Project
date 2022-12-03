using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTypesEnemy : MonoBehaviour
{
    GameManager gm;

    public float lifeSpan;

    //VFX for hitting ships

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (!gm.isPaused && !gm.isCutscene)
        {
            LifeCounter();
        }
    }

    public void LifeCounter()
    {
        if (lifeSpan <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            lifeSpan -= Time.deltaTime;
        }
    }

    public void HitFX()
    {
        //Spawn a hit effect
        Destroy(this.gameObject);
    }
}
