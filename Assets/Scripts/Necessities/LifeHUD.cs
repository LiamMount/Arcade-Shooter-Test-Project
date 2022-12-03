using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeHUD : MonoBehaviour
{
    GameManager gm;

    public Image firstLife;
    public Image secondLife;
    public Image thirdLife;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (gm.lives == 3)
        {
            firstLife.gameObject.SetActive(false);
            secondLife.gameObject.SetActive(true);
            thirdLife.gameObject.SetActive(true);
        }

        else if (gm.lives == 2)
        {
            firstLife.gameObject.SetActive(false);
            secondLife.gameObject.SetActive(false);
            thirdLife.gameObject.SetActive(true);
        }

        else if (gm.lives == 1)
        {
            firstLife.gameObject.SetActive(false);
            secondLife.gameObject.SetActive(false);
            thirdLife.gameObject.SetActive(false);
        }
    }
}
