using UnityEngine;

namespace Sounds
{
    public class WalkZone : SoundZone
    {
        [SerializeField] private WalkNames _walk;

        protected override void OnAmbientEnter(EnvironmentSound sound)
        {
            sound.ChangeWalk(_walk);
        }
    }
}