using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TaskBars;
using UnityEngine;

namespace FireSystem
{
    public class FireSpawner : MonoBehaviour
    {
        private const float CheckStep = 0.1f;
        
        private readonly List<ParticlePlayer> Furniture = new();
        private readonly List<ParticlePlayer> Burned = new();
        
        [SerializeField] private List<FireSetup> _fires;
        [SerializeField] private FireCollisionDetector _particleTemplate;
        [SerializeField] private int _startFireCount;

        private Coroutine _routine;
        private IProgress _task;

        public void Initialize(IProgress task)
        {
            _task = task;
            
            for (int i = 0; i < _startFireCount; i++)
            {
                FireSetup fire = _fires.GetRandom();
                
                Furniture.Add(fire
                    .Initialize(Instantiate(_particleTemplate, fire.transform), BurnFire)
                    .GetComponent<ParticlePlayer>());

                _fires.Remove(fire);
            }

            foreach (ParticlePlayer player in Furniture)
                player.Play();

            _routine = StartCoroutine(CheckRoutine());
        }

        private IEnumerator CheckRoutine()
        {
            var wait = new WaitForSeconds(CheckStep);

            while (GetBurned().Count - Burned.Count > 0)
            {
                yield return wait;
                
                _task.Report(Furniture.Count - GetBurned().Count, Furniture.Count - Burned.Count);
                
                if (Burned.Count == Furniture.Count)
                    Lose();
            }
            
            Win();
        }

        private List<ParticlePlayer> GetBurned()
        {
            return Furniture.Where(element => element.CanPlay == false).ToList();
        }

        private void BurnFire(ParticlePlayer player)
        {
            Burned.Add(player);
        }

        private void Lose()
        {
            if (_routine != null)
                StopCoroutine(_routine);
            
            _task.Lose();
        }
        
        private void Win()
        {
            _task.Win();
        }
    }
}