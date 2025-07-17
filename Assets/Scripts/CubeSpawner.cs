using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    private readonly int _minCubeValue = 2;
    private readonly int _maxCubeValue = 6;
    private readonly int _scaleDivider = 2;
    private readonly int _chanceDivider = 2;

    public void SplitCube(Cube cube)
    {
        List<Cube> cubes = new();
        int countNewCubes = Random.Range(_minCubeValue, _maxCubeValue + 1);

        for (int i = 0; i < countNewCubes; i++)
            cubes.Add(CreateCube(cube));
    }

    private Cube CreateCube(Cube cube)
    {
        float newSplitChance = cube.CurrentSplitChance / _chanceDivider;
        float newGeneration = cube.Generation + 1;
        Vector3 newScale = cube.transform.localScale / _scaleDivider;

        Cube newCube = Instantiate(_cubePrefab, cube.transform.position, Quaternion.identity);
        newCube.Construct(cube.transform.position, newScale, newSplitChance, newGeneration);
        
        return newCube;
    }
}
