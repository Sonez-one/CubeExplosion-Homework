using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Exploder _exploder;

    public float CurrentSplitChance { get; private set; } = 100f;

    private readonly float _maxSplitChance = 100f;

    private Renderer _renderer;
    private Rigidbody _cubeRigidbody;

    public event Action<Cube> Splitting;
    public event Action<Cube> Removing;

    public void Init(float splitChance)
        => CurrentSplitChance = splitChance;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _cubeRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
        => _renderer.material.color = UnityEngine.Random.ColorHSV(0f, 1f, 0.1f, 1f, 0.5f, 1f);

    private void OnMouseDown()
        => Explode();

    private void Explode()
    {
        if (CanSplit())
        {
            _exploder.Explode(_cubeRigidbody);
            Splitting?.Invoke(this);
        }
        else
        {
            _exploder.Explode(_cubeRigidbody, _renderer.bounds.size.y);
        }

        Removing?.Invoke(this);
        Destroy(gameObject);
    }

    private bool CanSplit()
        => UnityEngine.Random.Range(0, _maxSplitChance) <= CurrentSplitChance;
}
