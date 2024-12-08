using UnityEngine;

namespace CompassBars
{
    public class Marker : MonoBehaviour
    {
        [SerializeField] private Sprite _sprite;
        
        private RectTransform _icon;
        private Transform _transform;
        
        public RectTransform Icon => _icon;

        public void Initialize(IconMarker icon)
        {
            _transform = transform;
            
            icon.Initialize(_sprite);
            _icon = icon.transform as RectTransform;
        }

        public Vector2 GetPosition()
        {
            return new Vector2(_transform.position.x, _transform.position.z);
        }
    }
}
