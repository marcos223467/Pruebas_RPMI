using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luchador : MonoBehaviour
{
    [SerializeField] private float vel, velMax, radio_ataque;
    private int vidas;
    [SerializeField] private GameObject[] Vidas;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Animator _anim;
    private bool ataca1, ataca2;

    [SerializeField] private Transform PAtaque;

    private Vector2 inputDirection;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        inputDirection = new Vector2(0, 0);
        ataca1 = false;
        ataca2 = false;
        vidas = Vidas.Length;
    }

    // Update is called once per frame
    void Update()
    {
        inputDirection.x = Input.GetAxis("Horizontal");
        if (inputDirection.x < -0.1f)
            _spriteRenderer.flipX = true;
        if(inputDirection.x > 0.1f)
            _spriteRenderer.flipX = false;

        if (Input.GetButtonDown("Fire1"))
        {
            ataca1 = true;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            ataca2 = true;
        }
    }

    private void FixedUpdate()
    {
        Mover();
        Atacar();
    }

    private void Mover()
    {

        if (_rb.velocity.magnitude <= velMax)
            _rb.AddForce(inputDirection * vel, ForceMode2D.Force);
        
        if(_rb.velocity.magnitude <= 0.3f)
            _anim.SetBool("Movimiento", false);
        else
            _anim.SetBool("Movimiento", true);
    }

    private void Atacar()
    {
        if (ataca1)
        {
            ataca1 = false;
            _anim.SetTrigger("Ataque");
            Collider2D[] objetos = Physics2D.OverlapCircleAll(PAtaque.position, radio_ataque);

            foreach (Collider2D enemigo in objetos)
            {
                if (enemigo.CompareTag("Enemigo"))
                {
                    enemigo.gameObject.GetComponent<Contrincante>().TakeDMG();
                }
            }
        }

        if (ataca2)
        {
            ataca2 = false;
            _anim.SetTrigger("Ataque2");
            Collider2D[] objetos = Physics2D.OverlapCircleAll(PAtaque.position, radio_ataque);

            foreach (Collider2D enemigo in objetos)
            {
                if (enemigo.CompareTag("Enemigo"))
                {
                    enemigo.gameObject.GetComponent<Contrincante>().TakeDMG();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(PAtaque.position, radio_ataque);
    }
}
