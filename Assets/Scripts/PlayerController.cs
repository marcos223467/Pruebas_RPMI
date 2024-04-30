using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float vel, vel_Max;
    private Rigidbody2D _rb;
    [SerializeField] private float dash_CD, t_cooldown;

    public bool double_jump, dash, habilidades;

    public GameObject Habilidades;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        double_jump = false;
        dash = false;
        dash_CD = 0f;
        habilidades = false;
    }

    private void Update()
    {
        if (dash_CD > 0)
        {
            dash_CD -= Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            habilidades = !habilidades;
            Habilidades.SetActive(habilidades);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Mover();
        Dash();
    }

    private void Mover()
    {
        Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), 0);
        _rb.drag = 1f;
        if (dir.x < -0.1)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(dir.x > 0.1)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (_rb.velocity.magnitude <= vel_Max)
            _rb.AddForce(dir * vel, ForceMode2D.Force);
    }

    private void Dash()
    {
        if (dash && dash_CD <= 0f)
        {
            Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), 0);
            if (dir.x == 0)
            {
                if (GetComponent<SpriteRenderer>().flipX)
                    dir.x = -1;
                else
                    dir.x = 1;
            }
            if (Input.GetAxis("Fire1") > 0)
            {
                _rb.drag = 5f;
                _rb.AddForce(dir * 20, ForceMode2D.Impulse);
                dash_CD = t_cooldown;
            }
        }
    }
}


