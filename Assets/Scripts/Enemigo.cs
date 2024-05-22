using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemigo : MonoBehaviour
{
    [Header("AI detection")]
    public bool detectado;
    public LayerMask layer_player;
    [Range(0f, 20f)]
    public float radioDetector;
    
    [Header("Mele Detector")] 
    private float timer_ataque;
    public float attack_time;

    [Header("Wander")] 
    public float rapidez;
    public float maxDist;
    private Vector2 dir;

    private bool isWandering = false;
    private bool move = false;

    [Header("Gizmo Parameters")]
    public Color find = Color.red;
    public bool mostrar = true;
    

    private Rigidbody2D rb;
    private Animator _animator;
    private GameObject target;
    private bool seek;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        seek = false;
    }

    private void FixedUpdate()
    {
        DetectaJugador();
        if (!detectado)
        {
            if (!isWandering)
            {
                StartCoroutine(Wander());
            }
            Vagabundear();
        }
        if (seek)
        {
            Seek();
        }
    }

    public void DetectaJugador()
    {
        Collider2D jugador = Physics2D.OverlapCircle(transform.position, radioDetector, layer_player);
        if (jugador != null)
        {
            detectado = true;
            seek = true;
            target = jugador.gameObject;
        }
        else
        {
            detectado = false;
            seek = false;
        }
    }
    
    IEnumerator Wander()
    {
        int walkWait = Random.Range(1, 8);
        int walkTime = Random.Range(1, 5);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);
        move = true;
        dir = new Vector2(Random.Range(-1, 1), 0);
        yield return new WaitForSeconds(walkTime);
        move = false;

        isWandering = false;

    }
    
    public void Vagabundear()
    {
        _animator.SetBool("Movimiento", move);
        if (move)
        {
            rb.MovePosition(rb.position + dir * rapidez * Time.fixedDeltaTime);
        }
    }
    
    public void Seek()
    {
        if (Vector2.Distance(transform.position, target.transform.position) > maxDist)
        {
            rb.MovePosition(Vector2.MoveTowards(rb.position, target.transform.position, rapidez * Time.fixedDeltaTime));
            _animator.SetBool("Movimiento", true);
        }
        else
        {
            rb.velocity = Vector2.zero;
            _animator.SetBool("Movimiento", false);
        }
        
    }
    
    private void OnDrawGizmos()
    {
        if (mostrar)
        {
            Gizmos.color = Color.green;
            if (detectado)
                Gizmos.color = find;
            Gizmos.DrawWireSphere(transform.position, radioDetector);
        }
    }

    /*public void Ataque()
    {
        
        Collider2D[] jugador = Physics2D.OverlapCircleAll(p_ataque.position, radio, layer_player);
        for (int i = 0; i < jugador.Length; i++)
        {
            //jugador[i].GetComponent<PlayerController>().TakeDMG();
        }
    }*/

    public void Rebuscar()
    {
        detectado = false;
    }
}
