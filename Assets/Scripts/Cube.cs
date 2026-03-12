using System;
using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public Action<Cube> OnLifeEnded;

    [SerializeField] private ColorChanger _color;

    private Renderer _renderer;

    private float _minLifeTime = 2f;
    private float _maxLifeTime = 5f;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
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