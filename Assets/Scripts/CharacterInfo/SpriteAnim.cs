using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FragileReflection
{
    public class SpriteAnim : MonoBehaviour
    {
        private float _speed = 1f;
        private bool _loop = true;
        private Image _image = null;

        private Sprite[] _sprites = null;
        private float _timePerFrame = 0f;
        private float _ElapsedTime = 0f;
        private int _currentFrame = 0;

        private void Start()
        {
            _image = GetComponent<Image>();
            enabled = true;
            UpdateSprite("pulse_green_sprite", 30);

            GameEvents.onHealthStatusChanged += UpdateSprite;
        }

        private void OnDestroy()
        {
            GameEvents.onHealthStatusChanged -= UpdateSprite;
        }

        private void UpdateSprite(string healthStatus, int fps)
        {
            _sprites = Resources.LoadAll<Sprite>(healthStatus);
            if (_sprites != null && _sprites.Length > 0)
            {
                _timePerFrame = 1f / fps;
                Play();
            }
            else
                Debug.Log("Failed to load sprite sheet");
        }

        public void Play()
        {
            enabled = true;  
        }

        private void Update()
        {
            _ElapsedTime += Time.deltaTime * _speed;
            if (_ElapsedTime >= _timePerFrame)
            {
                _ElapsedTime = 0f;
                ++_currentFrame;
                SetSprite();
                if (_currentFrame >= _sprites.Length)
                {
                    if (_loop)
                        _currentFrame = 0;
                    else
                        enabled = false;
                }
            }
        }

        private void SetSprite()
        {
            if (_currentFrame >= 0 && _currentFrame < _sprites.Length)
                _image.sprite = _sprites[_currentFrame];
        }
    }
}
