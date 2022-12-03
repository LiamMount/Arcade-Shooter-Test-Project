using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBoundary : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(collision.gameObject);
        }
    }
}
