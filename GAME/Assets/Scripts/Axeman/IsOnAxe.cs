using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsOnAxe : MonoBehaviour
{
    [SerializeField] Rigidbody2D player;
    [SerializeField] float punchForce = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = GetComponent<Rigidbody2D>();
        StartCoroutine(WaitToPunch());
    }

    IEnumerator WaitToPunch()
    {
        yield return new WaitForSeconds(1f);
        player.velocity = new Vector2(-punchForce, 0);
    }
}
