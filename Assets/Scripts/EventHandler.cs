using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    [SerializeField] private CubeSpawner _spawner;
    [SerializeField] private Exploder _exploder;
    [SerializeField] private Raycaster _raycaster;
    [SerializeField] private List<Cube> _cubes;

    private void OnEnable()
    {
        _raycaster.MouseButtonPressing += MouseButtonPressed;
    }

    private void OnDisable()
    {
        _raycaster.MouseButtonPressing -= MouseButtonPressed;
    }

    private void MouseButtonPressed(Cube cube)
    {
        if (cube.CanSplit(cube))
        {
            if (_spawner != null)
            {
                _spawner.SplitCube(cube);
                _exploder.Explode(cube.CubeRigidbody);
            }
        }
        else
        {
            if (_exploder != null)
            {
                _exploder.Explode(cube.CubeRigidbody, cube.Generation);
            }
        }

        Destroy(cube.gameObject);
    }
}
