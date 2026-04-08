using System;
using System.Collections;
using UnityEngine;

public abstract class Item<T> : MonoBehaviour where T : Item<T>
{
    protected Renderer Renderer;

    protected float MinLifeTime = 2f;
    protected float MaxLifeTime = 5f;
    
    private Rigidbody _rigidbody;
    
    public Action<T> LifeEnded;
    
    protected virtual void Awake()
    {
        Renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Activate (Vector3 position)
    {
        transform.position = position;

        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        gameObject.SetActive(true);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out _))
            StartCoroutine(LifeRoutine());
    }

    protected abstract IEnumerator LifeRoutine();
}