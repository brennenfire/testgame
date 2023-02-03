using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField] Vector2 direction = Vector2.down;
    [SerializeField] float force = 2f;
    [SerializeField] float axeTimer = 1f;
    [SerializeField] Axeman axeman;

    Vector2 startingPosition;
    Vector2 currentPosition;
    private SpriteRenderer spriteRenderer;
    new Collider2D collider2D;
    bool isRaised = true;
    
    public bool AxeisRaised => isRaised;

    void Awake()
    {
        startingPosition = axeman.transform.position + new Vector3(-2.5f, 1.5f);
        transform.position = startingPosition;
        spriteRenderer= GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
    }
    void Update()
    {
        currentPosition = axeman.transform.position + new Vector3(-2.5f, 1.5f);
    }

    [ContextMenu("Swing axe")]
    public void SwingAxe()
    {
        if (isRaised)
        {
            isRaised = false;
            spriteRenderer.enabled = true;
            collider2D.enabled = true;
            transform.Translate(direction.normalized * force);
            StartCoroutine(RaiseAxe());
        }
    }

    IEnumerator RaiseAxe()
    {
        yield return new WaitForSeconds(axeTimer);
        transform.position = currentPosition;
        spriteRenderer.enabled = false;
        collider2D.enabled = false;
        isRaised = true;
    }
}
