using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Axeman : MonoBehaviour
{
    [SerializeField] float initialSpeed = 1f;
    [SerializeField] float attackRange = 3f;
    [SerializeField] float cooldown = 10f;
    [SerializeField] Transform player;

    Vector2 target;
    Vector2 newPos;
    new Rigidbody2D rigidbody;
    Axe axe;
    float timerForNextAttack;
    private float speed;

    void Start()
    {
        rigidbody= GetComponent<Rigidbody2D>();
        axe = GetComponentInChildren<Axe>();
        timerForNextAttack = -1;
        speed = initialSpeed;
    }
    
    void Update()
    {
        if(axe.AxeisRaised && Vector2.Distance(player.position, rigidbody.position) >= attackRange)
        {
            timerForNextAttack = -1;
            MoveTowardsPlayer();
        }
        
        if(ShouldAttackPlayer())
        {
            speed = 0f;
            if (timerForNextAttack > 0)
            {
                timerForNextAttack -= Time.deltaTime;
            }
            else if (timerForNextAttack <= 0 && axe.AxeisRaised)
            {
                Attack();
                timerForNextAttack = cooldown;
            }
        }
        else
        {
            speed = initialSpeed;
        }
    }

    private void Attack()
    {
        axe.SwingAxe();
    }

    void MoveTowardsPlayer()
    {
        target = new Vector2(player.position.x, rigidbody.position.y);
        newPos = Vector2.MoveTowards(rigidbody.position, target, speed * Time.fixedDeltaTime);
        rigidbody.MovePosition(newPos);
    }
    IEnumerator WaitBetweenAttacks()
    {
        yield return new WaitForSeconds(10f);   
    }

    bool ShouldAttackPlayer()
    {
        return Vector2.Distance(player.position, rigidbody.position) <= attackRange;
    }
}
