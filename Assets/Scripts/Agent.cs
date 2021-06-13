using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody), typeof(MeshRenderer))]
[RequireComponent(typeof(Animator))]
public class Agent : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] float forseMult = 10f;
    [SerializeField] int startHP = 3;

    int hp;
    Collider _collider;
    Rigidbody _rigidbody;
    MeshRenderer _meshRenderer;
    Animator _animator;

    public int HP { get { return hp; } }

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        hp = startHP;
        StartMove();
    }

    void StartMove()
    {
        _collider.enabled = true;

        _rigidbody.AddForce(
            new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * forseMult, 
            ForceMode.Impulse);
        _meshRenderer.material.color = Random.ColorHSV();
    }

    void StopMove()
    {
        _collider.enabled = false;
        _rigidbody.velocity = Vector3.zero;
        _animator.SetTrigger("fadeIn");
    }

    void ActiveOff()
    {
        EventsManager.instance.AgentOffTrigger(gameObject);
        gameObject.SetActive(false);

#if UNITY_EDITOR
        Debug.Log("ActiveOff()");
# endif
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(gameObject.tag))
        {
            hp--;
            if(hp <= 0)
            {
                StopMove();
            }
        }
    }

    //private void OnMouseDown()
    //{
    //    UI_Manager.instance.ShowInfoText(gameObject.name, hp);
    //}
}
