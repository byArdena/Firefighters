using FireSystem;
using UnityEngine;

public class CollisionFireTester : MonoBehaviour
{
    [SerializeField] private FireSetup _fire;
    [SerializeField] private FireCollisionDetector _detector;

    private void Start()
    {
        _fire
            .Initialize(Instantiate(_detector, _fire.transform), null)
            .GetComponent<ParticlePlayer>()
            .Play();
    }
}