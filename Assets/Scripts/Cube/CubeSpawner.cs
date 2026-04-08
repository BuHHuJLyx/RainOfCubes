using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private BombSpawner _bombSpawner;
    
    private void Start()
    {
        StartCoroutine(Spawn());
    }
    
    protected override void SpawnItem(Cube cube)
    {
        base.SpawnItem(cube);
        cube.Disappeared += OnDisappeared;
    }
    
    private void OnDisappeared(Cube cube)
    {
        cube.Disappeared -= OnDisappeared;
        _bombSpawner.SpawnAt(cube.transform.position);
    }
}