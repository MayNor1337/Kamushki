using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnTouch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
