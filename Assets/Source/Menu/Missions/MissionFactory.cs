using System;
using System.Collections.Generic;

namespace Menu
{
    public class MissionFactory : IFactory<MissionView>
    {
        private readonly float LifeTime;
        private readonly TemplatePulled OnPulled;
        private readonly Action<MissionView> OnPushed;
        private readonly List<Mission> Rewards;

        public MissionFactory(TemplatePulled pulled, Action<MissionView> onPushed, float lifeTime,
            List<Mission> rewards)
        {
            OnPulled = pulled;
            OnPushed = onPushed;
            LifeTime = lifeTime;
            Rewards = rewards;
        }

        public delegate MissionView TemplatePulled();
        
        public MissionView Create()
        {
            return OnPulled().Initialize(Rewards.GetRandom(), LifeTime, OnPushed);
        }
    }
}