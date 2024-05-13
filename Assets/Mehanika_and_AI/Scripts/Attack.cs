using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject parent;
    
    [NonSerialized]
    public AudioSource audioSource;

    public int damage;

    public bool isActive;

    private Animator _animator;
    private bool _isPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _isPlayer = false;

        if (parent.GetComponent<BotController>())
        {
            BotController botController = parent.GetComponent<BotController>();
            damage = botController.damage;
        } 

        if (parent.GetComponent<Player>())
        {
            Player player = parent.GetComponent<Player>();
            damage = player.damage;

            _isPlayer = true;
        }

        _animator = parent.GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        isActive = _animator.GetBool("Attack");
    }
}
