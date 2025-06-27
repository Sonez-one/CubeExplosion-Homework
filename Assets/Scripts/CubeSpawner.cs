using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private List<Cube> _cubes;

    private readonly int _minCubeValue = 2;
    private readonly int _maxCubeValue = 6;
    private readonly int _scaleDivider = 2;
    private readonly int _chanceDivider = 2;

    private void OnEnable()
    {
        foreach (var cube in _cubes)
        {
            cube.Splitting += Spawn;
            cube.Removing += cube => _cubes.Remove(cube);
        }
    }

    private void OnDisable()
    {
        foreach (var cube in _cubes)
        {
            cube.Splitting -= Spawn;
            cube.Removing -= cube => _cubes.Remove(cube);
        }
    }

    private void Spawn(Cube explodedCube)
    {
        float splitChance = explodedCube.CurrentSplitChance;
        explodedCube.transform.localScale /= _scaleDivider;

        explodedCube.Splitting -= Spawn;
        explodedCube.Removing -= cube => _cubes.Remove(cube);
        _cubes.Remove(explodedCube);

        int cubeValue = Random.Range(_minCubeValue, _maxCubeValue + 1);

        for (int i = 0; i < cubeValue; i++)
        {
            Cube cube = Instantiate(explodedCube, explodedCube.transform.position, Quaternion.identity);

            cube.Splitting += Spawn;

            _cubes.Add(cube);
            cube.Init(splitChance / _chanceDivider);
        }
    }
}
