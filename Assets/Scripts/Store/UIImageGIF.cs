using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImageGIF : MonoBehaviour
{
    [SerializeField]
    private Image _image = null;
    [SerializeField]
    private Sprite[] _spriteArr = null;
    [SerializeField]
    private Sprite _mainSprite = null;
    private float _spriteDelay = 0.1f;
    private float _spriteCurrentDelay = 0;
    private int _currentSpriteIdx = 0;
    private bool _isMoving = false;
    public bool IsMoving
    {
        get => _isMoving; 
        set
        {
            _isMoving = value;
            if (_image != null)
                _image.sprite = _mainSprite;
        }
    }
    private void OnEnable()
    {
        _currentSpriteIdx = 0;
        _spriteCurrentDelay = _spriteDelay;
        _image.sprite = _mainSprite;
    }
    private void Update()
    {
        if (!_isMoving)
        {
            _image.sprite = _mainSprite;
            return;
        }

        _spriteCurrentDelay -= Time.deltaTime;
        if (_spriteCurrentDelay < 0)
        {
            _currentSpriteIdx++;
            _spriteCurrentDelay = 0;
            if (_currentSpriteIdx >= _spriteArr.Length)
                _currentSpriteIdx = 0;

            _image.sprite = _spriteArr[_currentSpriteIdx];
            _spriteCurrentDelay = _spriteDelay;
        }
    }
} // end of class
