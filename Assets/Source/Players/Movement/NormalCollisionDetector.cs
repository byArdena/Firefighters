using System;
using UnityEngine;

namespace Players
{
    public class NormalCollisionDetector: MonoBehaviour
    {
        public event Action<Vector3> Detected;

        private void OnCollisionEnter(Collision collision)
        {
            Vector3 normal = collision.contacts[0].normal;
            Detected?.Invoke(normal);
        }
    }
}