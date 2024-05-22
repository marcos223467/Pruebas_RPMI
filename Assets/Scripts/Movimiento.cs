using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public float vel, vel_max;
    [SerializeField] private GameObject _bala;
    [SerializeField] private Transform PDisparo;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Disparar();
    }

    private void FixedUpdate()
    {
        Mover();
    }

    void Mover()
    {
        Vector2 direccion = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (rb.velocity.magnitude <= vel_max)
        {
            rb.AddForce(direccion * vel, ForceMode2D.Force);
        }

    }

    private void Disparar()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bala = Instantiate(_bala, PDisparo);
            Vector2 puntodisparo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            bala.GetComponent<Bala>().Disparo(puntodisparo);
        }

    }
}
