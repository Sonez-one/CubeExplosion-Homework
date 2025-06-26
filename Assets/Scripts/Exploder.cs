using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    public void Explode(Rigidbody cube) 
    {
        cube.AddExplosionForce(_explosionForce, cube.position, _explosionRadius);
    }
}
