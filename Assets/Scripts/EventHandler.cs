using UnityEngine;

public class EventHandler : MonoBehaviour
{
    [SerializeField] private CubeSpawner _spawner;
    [SerializeField] private Exploder _exploder;
    [SerializeField] private Raycaster _raycaster;

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
        if (cube.CanSplit())
        {
            _spawner.SplitCube(cube);
            _exploder.Explode(cube.Rigidbody);
        }
        else
        {
            _exploder.Explode(cube.Rigidbody, cube.Generation);
        }

        Destroy(cube.gameObject);
    }
}
