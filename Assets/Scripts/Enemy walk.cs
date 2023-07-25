using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static AI;

public class Enemywalk : MonoBehaviour
{
    GameObject _player;
    private NavMeshAgent _nav;
    Animator _myAnimation;
    private bool _collidedWithPlayer;
    private float distans = 10f;
    private int _attackType;
    private int _maxValue = 3;
    private int _minValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        _nav = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _myAnimation.SetBool("Walk", true);
    }

    // Update is called once per frame
    void Update()
    {
        _nav.SetDestination(_player.transform.position);
        _nav.speed = 1f;
    }
    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _myAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player)
        {
            _attackType = UnityEngine.Random.Range(_minValue, _maxValue);
            _myAnimation.SetBool("Walk", false);
            RandomStyleAttack(_attackType);
        }
        print("enter trigger with _player");
    }

    void OnCollusionEnter(Collider other)
    {
            if (other.gameObject == _player)
        {
            _collidedWithPlayer = true;     
        }
            print("enter collided with _player");
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject == _player)
        {
            _collidedWithPlayer = false;   
        }
        print("enter collided with _player");
    }

     void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _player & distans > 10)
        {
            _myAnimation.SetBool("IsNearPlayer", false);
            _myAnimation.SetBool("Walk", true);
        }
        print("enter collided with _player");
    }
    void Attack()
    {
        if (_collidedWithPlayer)
        {
            print("enter collided with _player");
        }
    }
    void RandomStyleAttack(float _attackType)
    {
        switch (_attackType)
        {
            case (0) :
                SwordAttack2();
                break;
            case (1):
                SwordAttack1();
                break;
            case (2):
                SwordAttack();
                break;
            default:
                Debug.Log("произшла злпа");
                break;
        }
    }
    void SwordAttack()
    {
        //вызов анимации и сама атака
        _myAnimation.SetBool("IsNearPlayer", true);
        Debug.Log("sword attack");
    }
    void SwordAttack1()
    {
        //вызов анимации и сама атака
        _myAnimation.SetBool("IsNearPlayer", true);
        Debug.Log("sword attack1");
    }
    void SwordAttack2()
    {
        //вызов анимации и сама атака
        _myAnimation.SetBool("IsNearPlayer", true);
        Debug.Log("sword attack2");
    }
}
