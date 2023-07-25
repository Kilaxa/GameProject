using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public float seeDistans = 5f;
    public float attackDistanse = 2f;
    public float speed = 6f;
    private GameObject _player;
    Animator _myAnimation;
    private float _attackType;
    private float _maxValue = 3f;
    private float _minValue = 0f;

    public enum AttackType
    {
        Ax,
        Bow,
        Sword
    }


    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _myAnimation.SetBool("Walk", true);
    }

    void Update()
    {
           
        
           _attackType = Time.time + UnityEngine.Random.Range(_minValue, _maxValue);
        

        if(Vector3.Distance (transform.position,_player.transform.position) < seeDistans)
        {
            if (Vector3.Distance (transform.position, _player.transform.position) > attackDistanse)
            {
                RandomStyleAttack(_attackType);
                transform.LookAt (_player.transform);
                transform.Translate (new Vector3 (0,0,speed * Time.deltaTime));
            }
        }
        else { }
    }
    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _myAnimation = GetComponent<Animator>();
    }
    void RandomStyleAttack(float _attackType)
    {
        switch (_attackType)
        {
            case (float)AttackType.Ax:
                break;
            case (float)AttackType.Bow:
                break;
            case (2):
                SwordAttack();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    void SwordAttack()
    {
        //вызов анимации и сама атака
        Debug.Log("sword attack");
    }
}
