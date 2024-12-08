using UnityEngine;
using UnityEngine.UI;

namespace CompassBars
{
    public class IconMarker: MonoBehaviour
    {
        private Image _image;

        public void Initialize(Sprite sprite)
        {
            _image = GetComponent<Image>();
            _image.sprite = sprite;
        }
    }
}