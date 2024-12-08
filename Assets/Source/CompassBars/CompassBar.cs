using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CompassBars
{
    public class CompassBar : MonoBehaviour
    {
        private const float FullAngle = 360f;
        private const float RectPositionY = 0f;
        private const float RectSide = 1f;

        [SerializeField] private RawImage _compass;
        [SerializeField] private Transform _player;
        [SerializeField] private IconMarker _iconPrefab;
        [SerializeField] private float _maxDistance;
        [SerializeField] private float _minScale;
        [SerializeField] private float _maxScale;

        private List<Marker> _markers;
        private float _compassUnit;
        private bool _didInitialize;

        private void Update()
        {
            if (_didInitialize == false)
                return;

            _compass.uvRect = new Rect(_player.localEulerAngles.y / FullAngle, RectPositionY, RectSide, RectSide);
            ShowIcons();
        }

        public void Initialize(List<Marker> markers)
        {
            _markers = markers;
            _compassUnit = _compass.rectTransform.rect.width / FullAngle;
            AddIcons();
            _didInitialize = true;
        }

        private void AddIcons()
        {
            foreach (Marker marker in _markers)
            {
                IconMarker newIcon = Instantiate(_iconPrefab, _compass.transform);
                marker.Initialize(newIcon);
            }
        }

        private void ShowIcons()
        {
            foreach (Marker marker in _markers)
            {
                marker.Icon.anchoredPosition = GetPosOnCompass(marker);

                float distance = Vector2.Distance(GetPlayerPosition(), marker.GetPosition());
                float scale = _minScale;

                if (distance < _maxDistance)
                {
                    scale = Mathf.Clamp(_maxScale - (distance / _maxDistance), _minScale, _maxScale);
                }

                marker.Icon.localScale = Vector2.one * scale;
            }
        }

        private Vector2 GetPosOnCompass(Marker marker)
        {
            Vector2 playerForward = new Vector2(_player.forward.x, _player.forward.z);
            float angle = Vector2.SignedAngle(marker.GetPosition() - GetPlayerPosition(), playerForward);

            return new Vector2(_compassUnit * angle, RectPositionY);
        }

        private Vector2 GetPlayerPosition()
        {
            return new Vector2(_player.position.x, _player.position.z);
        }
    }
}