using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    [RequireComponent(typeof(Image))]
    public class PixelPerUnitFixer : MonoBehaviour
    {
        [SerializeField] private float multipler = 1;
        private Image _image;
        
        private void Update()
        {
            _image = GetComponent<Image>();
            var x = Screen.height;
            _image.pixelsPerUnitMultiplier = (4e-7f * x * x - 0.0025f * x + 4.7285f) * multipler;
        }

    }
}