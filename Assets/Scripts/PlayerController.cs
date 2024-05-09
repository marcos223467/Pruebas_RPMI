using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float vel, velMax, f_salto, f_dash, dash_CD, cool_down_dash;
    private bool canJump, salto, dash;
    [SerializeField] private LayerMask suelo, pared;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Animator _anim;

    private Vector2 inputDirection;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        canJump = true;
        salto = false;
        inputDirection = new Vector2(0, 0);
        dash_CD = 0f;
        dash = false;
    }

    // Update is called once per frame
    void Update()
    {
        inputDirection.x = Input.GetAxis("Horizontal");
        if (inputDirection.x < -0.1f)
            _spriteRenderer.flipX = true;
        if(inputDirection.x > 0.1f)
            _spriteRenderer.flipX = false;
        
        if (Input.GetAxis("Jump") > 0f && canJump)
            salto = true;

        if (dash_CD > 0f)
        {
            dash_CD -= Time.deltaTime;
        }

        if (Input.GetAxis("Fire1") > 0f && dash_CD <= 0f)
            dash = true;
    }

    private void FixedUpdate()
    {
        Mover();
        Jump();
        Dash();
    }

    private void Mover()
    {

        if (_rb.velocity.magnitude <= velMax)
            _rb.AddForce(inputDirection * vel, ForceMode2D.Force);
        
        if(_rb.velocity.magnitude <= 0.3f)
            _anim.SetBool("Run", false);
        else
            _anim.SetBool("Run", true);
    }

    private void Jump()
    {
        if (salto)
        {
            _rb.AddForce(new Vector2(0,1) * f_salto, ForceMode2D.Impulse);
            canJump = false;
            salto = false;
            _anim.SetBool("Salto", true);
        }
    }

    private void Dash()
    {
        if (dash)
        {
            _rb.AddForce(inputDirection * f_dash, ForceMode2D.Impulse);
            dash_CD = cool_down_dash;
            dash = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == Mathf.Log(suelo.value, 2f)) // Chocamos con el suelo
        {
            canJump = true;
            _anim.SetBool("Salto", false);
            _anim.SetBool("Wall", false);
        }
        
        if (other.gameObject.layer == Mathf.Log(pared.value, 2)) // Chocamos con el suelo
        {
            canJump = true;
            _anim.SetBool("Salto", false);
            _anim.SetBool("Wall", true);
        }
    }
}
