using UnityEngine;

public class Explosion : MonoBehaviour
{
    public void Explode(Vector3 position, float radius, float force)
    {
        Collider[] hits = Physics.OverlapSphere(position, radius);

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                hit.attachedRigidbody.AddExplosionForce(force, position, radius);
    }
}