using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public int maxHP;
    public int hp;

    public int damage;
    
    private PlayerInput _playerInput;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        hp = maxHP;

        _playerInput = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();

        _playerInput.ActivateInput();
    }

    // Update is called once per frame
    void Update()
    {
        //if()
    }

    private void FixedUpdate()
    {
        if (hp <= 0)
        {
            _animator.SetBool("Death", true);
            _playerInput.DeactivateInput();
        }
    }

    public void GetDamage(int damage)
    {
        hp = hp - damage;
    }
}
