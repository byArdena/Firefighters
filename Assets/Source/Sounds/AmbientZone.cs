using UnityEngine;

namespace Sounds
{
    public class AmbientZone : SoundZone
    {
        [SerializeField] private AmbientNames _ambient;
        
        protected override void OnAmbientEnter(EnvironmentSound sound)
        {
            sound.ChangeAmbient(_ambient);
        }
    }
}