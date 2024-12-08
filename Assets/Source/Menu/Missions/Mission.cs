using System;
using UnityEngine;

namespace Menu
{
    [Serializable]
    public struct Mission
    {
        [SerializeField] private Missions _type;
        [SerializeField] private string _name;
        [SerializeField] private int _reward;
        [SerializeField] private Sprite _sprite;
        
        public Missions Type => _type;
        public string Name => _name;
        public int Reward => _reward;
        public Sprite Sprite => _sprite;
    }
}