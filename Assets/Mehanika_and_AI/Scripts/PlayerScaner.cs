using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScaner : MonoBehaviour
{
    private BotController _botController;

    // Start is called before the first frame update
    void Start()
    {
        _botController = GetComponentInParent<BotController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            Player player = other.GetComponent<Player>();

            _botController.PlayerEnter(player, player.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            _botController.PlayerExit();
        }
    }
}
