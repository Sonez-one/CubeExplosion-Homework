using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    public Rigidbody CubeRigidbody { get; private set; }
    public float CurrentSplitChance { get; private set; } = 100f;

    private readonly float _maxSplitChance = 100f;

    private Renderer _renderer;

    public event Action<Cube> Splitting;
    public event Action<Cube> Removing;

    public void Init(float splitChance) => CurrentSplitChance = splitChance;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        CubeRigidbody = GetComponent<Rigidbody>();
    }

    private void Start() => _renderer.material.color = UnityEngine.Random.ColorHSV(0f, 1f, 0.1f, 1f, 0.5f, 1f);

    private void OnMouseDown() => Explode();

    private void Explode()
    {
        if (CanSplit())
        {
            Splitting?.Invoke(this);
        }

        Removing?.Invoke(this);
        Destroy(gameObject);
    }

    private bool CanSplit() => UnityEngine.Random.Range(0, _maxSplitChance) <= CurrentSplitChance;
}
