using System.Collections.Generic;
using System.Linq;
using FireSystem;
using Menu;
using UnityEngine;

namespace Level
{
    public class HouseFactory : IFactory<HouseSetup>
    {
        private readonly List<SerializedPair<Missions, HouseSetup[]>> Templates;
        private readonly Vector3 Position;
        private readonly Setup OnCreated;
        private readonly Missions Type;
        
        public HouseFactory(List<SerializedPair<Missions, HouseSetup[]>> templates, Vector3 position, Setup onCreated)
        {
            Templates = templates;
            Position = position;
            OnCreated = onCreated;
            Type = PlayerPrefs.HasKey(nameof(Missions))
                ? (Missions)PlayerPrefs.GetInt(nameof(Missions))
                : Missions.Default;
            Debug.Log(Type);
        }

        public delegate HouseSetup Setup(HouseSetup setup, Vector3 position, Quaternion quaternion);
        
        public HouseSetup Create()
        {
            return OnCreated?.Invoke(Templates
                    .Where(item => item.Key == Type)
                    .ToList().GetRandom().Value.GetRandom(),
                Position, Quaternion.identity);
        }
    }
}