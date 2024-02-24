using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySkeletSystem : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float Health = 100f;

    private Animator _animator;
    private NavMeshAgent _agent;

    private float distance;
    private bool isAttack = false;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        _animator.SetFloat("speed", 1f);
    }

    private void Update()
    {
        distance = Vector3.Distance(transform.position, target.position);

        if (!isAttack)
        {
            _agent.SetDestination(target.position);
        }

        if (distance <= 1.5f && !isAttack)
        {
            StartCoroutine("StartAttack");

            if (target.GetComponent<PlayerStats>())
            {
                target.GetComponent<PlayerStats>().TakeDamage(10f);
            }
        }
    }

    IEnumerator StartAttack()
    {
        isAttack = true;
        _animator.SetTrigger("isAttack");
        _agent.speed = 0f;
        yield return new WaitForSeconds(1.4f);
        _agent.speed = 4f;
        isAttack = false;
    }

    public void TakeDamage(float damage)
    {
        if (Health - damage > 0) 
        {
            Health -= damage;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
