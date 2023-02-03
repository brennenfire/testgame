using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxemanHead : MonoBehaviour
{
    [SerializeField] Axeman enemy;

    void OnTriggerEnter2D(Collider2D collision)
    {
        var sword = collision.GetComponent<PlayerSword>();
        if (sword == null)
        {
            return;
        }
        enemy.gameObject.SetActive(false);
    }
}
