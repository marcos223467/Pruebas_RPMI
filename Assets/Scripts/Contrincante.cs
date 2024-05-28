using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contrincante : MonoBehaviour
{
    [SerializeField] public GameObject[] Vidas;
    private int vida;
    private Animator _anim;
    void Start()
    {
        _anim = GetComponent<Animator>();
        vida = Vidas.Length;
    }

    public void TakeDMG()
    {
        if (vida > 0)
        {
            vida--;
            Vidas[vida].SetActive(false);
            if (vida <= 0)
            {
                _anim.SetTrigger("Muere");
                Destroy(gameObject, 3f);
            }
            else
            {
                _anim.SetTrigger("Golpe");
            }
        }
        
    }
}
