using System;

namespace FireSystem
{
    public class FirePresenter
    {
        private readonly Fire Model;
        private readonly FireCollisionDetector CollisionDetector;
        private readonly ParticlePlayer ParticlePlayer;
        private readonly Burner Burner;
        private readonly Action<ParticlePlayer> Burned;

        public FirePresenter(Fire model, FireCollisionDetector collisionDetector, ParticlePlayer particlePlayer, Burner burner,
            Action<ParticlePlayer> burned)
        {
            Model = model;
            CollisionDetector = collisionDetector;
            ParticlePlayer = particlePlayer;
            Burner = burner;
            Burned = burned;
        }

        public void Enable()
        {
            Model.Extinguished += OnExtinguished;
            Model.Burned += OnBurned;

            ParticlePlayer.Played += OnPlayed;
            ParticlePlayer.StartPlaying += OnStartPlaying;
            CollisionDetector.Extinguishing += OnExtinguishing;
        }

        public void Disable()
        {
            Model.Extinguished -= OnExtinguished;
            Model.Burned -= OnBurned;

            ParticlePlayer.Played -= OnPlayed;
            ParticlePlayer.StartPlaying -= OnStartPlaying;
            CollisionDetector.Extinguishing -= OnExtinguishing;
        }

        private void OnExtinguished()
        {
            CollisionDetector.DisallowDetect();
            ParticlePlayer.Stop();
        }

        private void OnBurned()
        {
            ParticlePlayer.DisallowPlay();
            Burner.Burn();
            Burned?.Invoke(ParticlePlayer);
        }

        private void OnPlayed()
        {
            Model.FlareUp();
        }

        private void OnStartPlaying()
        {
            CollisionDetector.AllowDetect();
        }

        private void OnExtinguishing()
        {
            Model.Extinguish();
        }
    }
}