using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public GameObject parent;

    private bool _isPlayer;

    private BotController _botController;
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _isPlayer = false;
        
        if (parent.GetComponent<Player>())
        {
            _isPlayer = true;
            _player = parent.GetComponent<Player>();
        }
        else
        {
            _botController = parent.GetComponent<BotController>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Attack>())
        {
            Attack attack = other.GetComponent<Attack>();

            if (attack.isActive && (attack.parent != parent))
            {

                if (_isPlayer)
                {
                    _player.GetDamage(attack.damage);
                }
                else
                {
                    if (parent.GetComponent<BotController>() &&
                        attack.parent.GetComponent<BotController>())
                    {
                        return;
                    }

                    _botController.GetDamage(attack.damage);
                }

                attack.audioSource.Play();
            }
        }

        Debug.Log("Hit +1");
    }
}
