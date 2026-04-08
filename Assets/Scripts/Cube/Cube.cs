using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer), typeof(Rigidbody), typeof(ColorChanger))]
public class Cube : Item<Cube>
{
    private ColorChanger _color;

    public Action<Vector3> Disappeared;

    protected override void Awake()
    {
        base.Awake();
        _color = GetComponent<ColorChanger>();
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out _))
        {
            Renderer.material.color = _color.SetRandomColor();
            StartCoroutine(LifeRoutine());
        }
    }

    protected override IEnumerator LifeRoutine()
    {
        float lifeTime = Random.Range(MinLifeTime, MaxLifeTime);

        yield return new WaitForSeconds(lifeTime);

        Renderer.material.color = _color.SetDefaultColor();

        Disappeared?.Invoke(transform.position);
        LifeEnded?.Invoke(this);
    }
}