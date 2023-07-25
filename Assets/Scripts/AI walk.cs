using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public Transform Character;
    public float speed = 0.1F;
    private Vector3 directionOfCharacter;
    private bool challenged = false;
    Animator myAnimation;

    private void Start()
    {
        myAnimation = GetComponent<Animator>();
    }

    void Update()
    {
        if (challenged)
        {
            directionOfCharacter = Character.transform.position - transform.position;
            directionOfCharacter = directionOfCharacter.normalized;
            transform.Translate (directionOfCharacter * speed, Space.World);
            myAnimation.SetBool("Walk", true); 
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            challenged = true;
        }
    }
}
