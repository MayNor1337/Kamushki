using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

using Random = UnityEngine.Random;

public class BotController : MonoBehaviour
{
    public float maxTimeState;
    public float minTimeState;
    public int maxHP;
    public float maxOffsetGoal;

    public int damage;

    public float dampingRotate;

    private Animator _animator; 
    private IEnumerator _coroutine;

    private Vector3 _goalPos;

    private Vector3 _posZone;
    private float _rZone; 

    private float _timeState;
    private bool _runCorutine;

    private Transform _tr_player;
    private Player _player;
    private bool _attackPlayer;
    private bool _life;

    public int hp;

    void Start()
    {
        _posZone = transform.parent.position;
        _rZone = transform.parent.GetComponent<SphereCollider>().radius;

        _animator = GetComponent<Animator>();

        _attackPlayer = false;
        _runCorutine = false;

        _goalPos = transform.position;

        hp = maxHP;
        _life = true;
    }

    private void Update()
    {
        if (_life)
        {
            _goalPos.y = transform.position.y;

            if ((_goalPos - transform.position).sqrMagnitude > maxOffsetGoal * maxOffsetGoal)
            {
                if (_attackPlayer)
                {
                    _animator.SetBool("run", true);
                    _animator.SetBool("walk", false);
                    _animator.SetBool("Attack", false);
                }
                else
                {
                    _animator.SetBool("walk", true);
                    _animator.SetBool("run", false);
                    _animator.SetBool("Attack", false);
                }

                LookToGoal();
                //Debug.Log((_goalPos - transform.position).magnitude);
            }
            else
            {
                if (_attackPlayer)
                {
                    _animator.SetBool("walk", false);
                    _animator.SetBool("run", false);
                    _animator.SetBool("Attack", true);

                    LookToGoal();
                }
                else
                {
                    _animator.SetBool("walk", false);
                    _animator.SetBool("run", false);
                    _animator.SetBool("Attack", false);
                }
            }

            Debug.DrawRay(_goalPos, Vector3.up * 100);
        }
        
    }

    private void FixedUpdate()
    {
        if (_life)
        {
            if (!_attackPlayer && !_runCorutine)
            {
                _timeState = Random.RandomRange(minTimeState, maxTimeState);

                _coroutine = SetNewGoal(_timeState);
                StartCoroutine(_coroutine);

            }

            if (_attackPlayer)
            {
                //Go to Player
                _goalPos = _tr_player.position;

                //Player Death
                if (_player.hp <= 0)
                {
                    _attackPlayer = false;
                }
            }

            if (hp <= 0)
            {
                _animator.SetBool("Death", true);
                _animator.SetBool("walk", false);
                _animator.SetBool("run", false);
                _animator.SetBool("Attack", false);

                _life = false;
            }
        }
        
    }

    void LookToGoal()
    {
        var rotation = Quaternion.LookRotation(_goalPos - transform.position);
        rotation.x = 0; rotation.z = 0;

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * dampingRotate);
    }

    public void GetDamage(int damage)
    {
        if (_life)
        {
            hp = hp - damage;
        }
    }

    private IEnumerator SetNewGoal(float waitTime)
    {
        _runCorutine = true;
        print("Swap time: " + waitTime + " seconds");

        float locX = Random.RandomRange(-_rZone, _rZone);
        float locZ = Random.RandomRange(-_rZone, _rZone);
        
        _goalPos = _posZone;
        _goalPos[0] += locX;
        _goalPos[2] += locZ;
           
        yield return new WaitForSeconds(waitTime);
        _runCorutine = false;
    }

    public void PlayerEnter(Player player, Transform trPlayer)
    {
        _attackPlayer = true;

        _tr_player = trPlayer;
        _player = player;
        Debug.Log("Игрок рядом");
    }
    public void PlayerExit()
    {
        _attackPlayer = false;

        Debug.Log("Игрок ушел");
    }
}
