using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILookTo : MonoBehaviour
{

    private Transform _parent;
    private TextMeshPro m_TextMeshPro;
    private Transform _playerCamera;

    private int maxHP;
    private int currHP; 

    private bool isPlayer;
    private BotController botcontroller;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        _playerCamera = Camera.main.transform;
        _parent = transform.parent;
        m_TextMeshPro = GetComponent<TextMeshPro>();

        if (_parent.GetComponent<BotController>())
        {
            botcontroller = _parent.GetComponent<BotController>();
            isPlayer = false;
        }

        if (_parent.GetComponent<Player>())
        {
            player = _parent.GetComponent<Player>();
            isPlayer = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_playerCamera.position);
    }

    private void FixedUpdate()
    {
        if (isPlayer)
        {
            maxHP = player.maxHP;
            currHP = player.hp;
        }
        else
        {
            maxHP = botcontroller.maxHP;
            currHP = botcontroller.hp;
        }

        m_TextMeshPro.text = currHP.ToString()+"/"+maxHP.ToString();
    }
}
