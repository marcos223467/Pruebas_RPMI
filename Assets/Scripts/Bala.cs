using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] private float F_bala;
    private Rigidbody2D rb;
    [SerializeField] private float TTL;

    private int index;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Disparo(Vector2 puntoDisparo)
    {
        Vector2 dir = new Vector2(puntoDisparo.x - transform.position.x, puntoDisparo.y - transform.position.y);
        dir.Normalize();
        Debug.Log(dir);
        rb.AddForce(dir * F_bala, ForceMode2D.Impulse);
        Destroy(this.gameObject, TTL * 1000f);
    }
    
}
