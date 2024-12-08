using HealthSystem;
using UnityEngine;

public class ParticleDamageApplier : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent(out HealthCollisionDetector detector) == false)
            return;
        
        detector.Detect();
    }
}