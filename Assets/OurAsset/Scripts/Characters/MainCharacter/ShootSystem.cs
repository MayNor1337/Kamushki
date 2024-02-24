using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSystem : MonoBehaviour
{
    [SerializeField] private GameObject fireBallPrefab;
    [SerializeField] private Transform spawnFireBallPos;
    [SerializeField] private float speedFireBall;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = transform.position + transform.forward;
            Quaternion rotation = transform.rotation;

            GameObject fireball = Instantiate(fireBallPrefab, spawnFireBallPos.position, rotation);

            Rigidbody rb = fireball.GetComponent<Rigidbody>();

            rb.velocity = transform.forward * speedFireBall;

            Destroy(fireball.gameObject, 3f);
        }
    }
}
