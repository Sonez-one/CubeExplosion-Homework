using System;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    public event Action<Cube> MouseButtonPressing;

    private void Update()
    {
        var userInput = Input.GetMouseButtonDown(0);

        if (userInput)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 1000))
            {
                GameObject hitObject = hit.collider.gameObject;

                if (hitObject.TryGetComponent<Cube>(out var cube))
                    MouseButtonPressing?.Invoke(cube);
            }
        }
    }
}
