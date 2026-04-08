using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private BombSpawner _bombSpawner;
    
    protected override void SpawnItem(Cube cube)
    {
        base.SpawnItem(cube);
        cube.Disappeared -= SpawnBomb;
        cube.Disappeared += SpawnBomb;
    }
    
    private void SpawnBomb(Vector3 position)
    {
        _bombSpawner.SpawnAt(position);
    }
}