using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private float _repeatRate = 5f;
    [SerializeField] private int _poolCapacity = 15;
    [SerializeField] private int _poolMaxSize = 30;

    private ObjectPool<Cube> _pool;

    private float _minPositionXZ = -4f;
    private float _maxPositionXZ = 4f;
    private float _positionY = 7f;

    private bool _isEnabled = true;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (cube) => SpawnCube(cube),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );
    }

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private void SpawnCube(Cube cube)
    {
        float positionX = Random.Range(_minPositionXZ, _maxPositionXZ);
        float positionZ = Random.Range(_minPositionXZ, _maxPositionXZ);

        Vector3 spawnPosition = new Vector3(positionX, _positionY, positionZ);

        cube.Activate(spawnPosition);

        cube.OnLifeEnded += ReturnCube;
    }

    private void GetCube()
    {
        _pool.Get();
    }

    public void ReturnCube(Cube cube)
    {
        cube.OnLifeEnded -= ReturnCube;
        _pool.Release(cube);
    }

    private IEnumerator SpawnRoutine()
    {
        while (_isEnabled)
        {
            GetCube();
            yield return new WaitForSeconds(_repeatRate);
        }
    }
}