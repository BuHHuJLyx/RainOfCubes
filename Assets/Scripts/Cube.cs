using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private ColorChanger _color;

    private Renderer _renderer;
    private Rigidbody _rigidbody;

    private float _minLifeTime = 2f;
    private float _maxLifeTime = 5f;

    public Action<Cube> OnLifeEnded;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Activate (Vector3 position)
    {
        transform.position = position;

        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        gameObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform))
        {
            _renderer.material.color = _color.SetRandomColor();
            StartCoroutine(LifeRoutine());
        }
    }

    private IEnumerator LifeRoutine()
    {
        float lifeTime = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);

        yield return new WaitForSeconds(lifeTime);

        _renderer.material.color = _color.SetDefaultColor();

        OnLifeEnded?.Invoke(this);
    }
}