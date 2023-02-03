using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float decceleration = 1f;
    [SerializeField] float acceleration = 1f;
    [SerializeField] float speed = 1f;
    [SerializeField] float jumpVelocity = 1f;
    [SerializeField] Transform feet;
    [SerializeField] GameObject sword;

    new Rigidbody2D rigidbody;
    string readHorizontal;
    string jumpButton;
    string attackButton;
    float horizontal;
    bool isGrounded;
    int layerMask;
    int jumpsRemaining = 1;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        readHorizontal = $"PHorizontal";
        jumpButton = $"PJump";
        layerMask = LayerMask.GetMask("Default");
    }

    void Update()
    {
        UpdateIsGrounded();
        ReadHorizontalInput();
        MoveHorizontal();
        if (ShouldStartJump() && isGrounded)
        {
            Jump();
        }
        if (isGrounded)
        {
            Debug.Log("is grounded");
            jumpsRemaining = 1;
        }
        if (ShouldAttack())
        {
            Attack();
        }
    }

    bool ShouldAttack()
    {
        return Input.GetMouseButtonDown(0);
    }
    void Attack()
    {
        sword.SetActive(true);
        StartCoroutine(SwordSheathe());
    }
    IEnumerator SwordSheathe()
    {
        yield return new WaitForSeconds(0.5f);
        sword.SetActive(false);
    }

    void Jump()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpVelocity);
        jumpsRemaining--;
    }

    bool ShouldStartJump()
    {
        return Input.GetButtonDown(jumpButton) && jumpsRemaining > 0;
    }

    void ReadHorizontalInput()
    {
        horizontal = Input.GetAxis(readHorizontal) * speed;
    }
    void MoveHorizontal()
    {
        float smoothnessMultiplier = horizontal == 0 ? decceleration : acceleration;
        float newHorizontal = Mathf.Lerp(rigidbody.velocity.x, horizontal * speed, Time.deltaTime * smoothnessMultiplier);
        rigidbody.velocity = new Vector2(newHorizontal, rigidbody.velocity.y);
    }
    void UpdateIsGrounded()
    {
        var hit = Physics2D.OverlapCircle(feet.position, 0.1f, layerMask);
        isGrounded = hit != null;
    }
}
