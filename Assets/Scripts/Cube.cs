using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    public float CurrentSplitChance { get; private set; } = 100f;
    public float Generation { get; private set; }
    public Rigidbody CubeRigidbody { get; private set; }

    private readonly float _maxSplitChance = 100f;

    private ColorChanger _colorChanger;
    private Renderer _renderer;

    public void Construct(Vector3 position, Vector3 scale, float splitChance, float generation)
    {
        transform.position = position;
        transform.localScale = scale;
        CurrentSplitChance = splitChance;
        Generation = generation;

        Color color = _colorChanger.GenerateColor();
        _renderer.material.color = color;
    }

    public bool CanSplit(Cube cube)
        => UnityEngine.Random.Range(0, _maxSplitChance) <= cube.CurrentSplitChance;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        CubeRigidbody = GetComponent<Rigidbody>();
        _colorChanger = GetComponent<ColorChanger>();
    }
}
