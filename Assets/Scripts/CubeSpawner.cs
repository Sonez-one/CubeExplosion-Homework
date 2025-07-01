using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private List<Cube> _cubes;
    [SerializeField] private Exploder _exploder;
    [SerializeField] private Cube _cubePrefab;

    private readonly int _minCubeValue = 2;
    private readonly int _maxCubeValue = 6;
    private readonly int _scaleDivider = 2;
    private readonly int _chanceDivider = 2;
    private readonly float _maxSplitChance = 100f;

    private void OnEnable()
    {
        if (_cubes != null)
        {
            foreach (var cube in _cubes)
                cube.OnCubeClicking += Spawn;
        }
    }

    private void OnDisable()
    {
        foreach (var cube in _cubes)
        {
            if (cube != null)
                cube.OnCubeClicking -= Spawn;
        }
    }

    private Cube CreateCube(Cube cube)
    {
        float newSplitChance = cube.CurrentSplitChance / _chanceDivider;
        float newGeneration = cube.Generation + 1;
        Vector3 newScale = cube.transform.localScale / _scaleDivider;

        Cube newCube = Instantiate(_cubePrefab, cube.transform.position, Quaternion.identity);
        newCube.Construct(cube.transform.position, newScale, newSplitChance, newGeneration);
        newCube.OnCubeClicking += Spawn;

        return newCube;
    }

    private void SplitCube(Cube cube)
    {
        List<Cube> cubes = new();
        int countNewCubes = Random.Range(_minCubeValue, _maxCubeValue + 1);

        for (int i = 0; i < countNewCubes; i++)
            cubes.Add(CreateCube(cube));
    }

    private bool CanSplit(Cube cube)
        => Random.Range(0, _maxSplitChance) <= cube.CurrentSplitChance;

    private void Spawn(Cube explodedCube)
    {
        explodedCube.OnCubeClicking -= Spawn;

        if (CanSplit(explodedCube))
        {
            SplitCube(explodedCube);
            _exploder.Explode(explodedCube.CubeRigidbody);
        }
        else
        {
            if (_exploder != null)
            _exploder.Explode(explodedCube.CubeRigidbody, explodedCube.Generation);
        }
    }
}
