using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class Spawner<T> : MonoBehaviour where T : Item<T>
{
    [SerializeField] private T _prefab;
    [SerializeField] private float _repeatRate;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;

    [SerializeField] private bool _shouldSpawn = true;
    [SerializeField] private SpawnerStatsView _statsView;

    private ObjectPool<T> _pool;
    private WaitForSeconds _delay;

    private float _minPositionXZ = -4f;
    private float _maxPositionXZ = 4f;
    private float _positionY = 7f;

    private int _spawnedAmount;
    private int _createdAmount;

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: () =>
            {
                _createdAmount++;
                return Instantiate(_prefab);
            },
            actionOnGet: (item) => SpawnItem(item),
            actionOnRelease: (item) => item.gameObject.SetActive(false),
            actionOnDestroy: (item) => Destroy(item.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );

        _delay = new WaitForSeconds(_repeatRate);
    }

    private void Start()
    {
        if (_shouldSpawn)
            StartCoroutine(Spawn());
    }

    private void Update()
    {
        _statsView.UpdateView(_spawnedAmount, _createdAmount, _pool.CountActive);
    }

    public void SpawnAt(Vector3 position)
    {
        _spawnedAmount++;
        T item = _pool.Get();
        item.Activate(position);
    }

    protected virtual void SpawnItem(T item)
    {
        float positionX = Random.Range(_minPositionXZ, _maxPositionXZ);
        float positionZ = Random.Range(_minPositionXZ, _maxPositionXZ);

        Vector3 spawnPosition = new Vector3(positionX, _positionY, positionZ);

        item.Activate(spawnPosition);

        item.LifeEnded += ReturnToPool;
    }

    private void GetFromPool()
    {
        _spawnedAmount++;
        _pool.Get();
    }

    protected void ReturnToPool(T item)
    {
        item.LifeEnded -= ReturnToPool;
        _pool.Release(item);
    }

    private IEnumerator Spawn()
    {
        while (enabled)
        {
            GetFromPool();
            yield return _delay;
        }
    }
}