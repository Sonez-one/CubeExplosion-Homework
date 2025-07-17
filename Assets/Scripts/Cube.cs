using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private readonly float _maxSplitChance = 100f;

    private Renderer _renderer;

    public float CurrentSplitChance { get; private set; } = 100f;
    public int Generation { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        Rigidbody = GetComponent<Rigidbody>();

        _renderer.material.color = Random.ColorHSV(0f, 1f, 0.1f, 1f, 0.5f, 1f);
    }

    public void Construct(Vector3 position, Vector3 scale, float splitChance, int generation)
    {
        transform.position = position;
        transform.localScale = scale;
        CurrentSplitChance = splitChance;
        Generation = generation;
    }

    public bool CanSplit()
        => Random.Range(0, _maxSplitChance) <= CurrentSplitChance;
}
