using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody), typeof(MeshRenderer))]
[RequireComponent(typeof(Animator))]
public class Agent : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] float forseMult = 200f;
    [SerializeField] int startHP = 3;

    int hp;

    private void OnEnable()
    {
        hp = startHP;
        StartMove();
    }

    void StartMove()
    {
        GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().AddForce(
            new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * forseMult, 
            ForceMode.Impulse);
        GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
    }

    void StopMove()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Animator>().SetTrigger("fadeIn");
    }

    void ActiveOff()
    {
        gameObject.SetActive(false);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(gameObject.tag))
        {
            hp--;
            if(hp <= 0)
            {
                StopMove();
                ActiveOff();
            }
        }
    }
}
