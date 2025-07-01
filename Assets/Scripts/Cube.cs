using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    public float CurrentSplitChance { get; private set; } = 100f;
    public float Generation { get; private set; }
    public Rigidbody CubeRigidbody { get; private set; }

    private Renderer _renderer;

    public event Action<Cube> OnCubeClicking;

    public void Construct(Vector3 position, Vector3 scale, float splitChance, float generation)
    {
        transform.position = position;
        transform.localScale = scale;
        CurrentSplitChance = splitChance;
        Generation = generation;
        _renderer.material.color = UnityEngine.Random.ColorHSV(0f, 1f, 0.1f, 1f, 0.5f, 1f);
    }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        CubeRigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseUpAsButton()
    {
        OnCubeClicking?.Invoke(this);

        Destroy(gameObject);
    }
}
