using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImageGIF : MonoBehaviour
{
    [SerializeField]
    private bool _isItemDetail = false;
    [SerializeField]
    private bool _useGif = true;

    public Image Image = null;
    public bool UseGIF { get => _useGif; set => _useGif = value; }
    [SerializeField]
    public Sprite MainSprite = null;
    [SerializeField]
    public Sprite[] SpriteArr = null;

    [SerializeField]
    public bool useItemDetailSpriteArr = false;
    [SerializeField]
    public Sprite[] ItemDetailSpriteArr = null;
    [SerializeField]
    public Sprite ItemDetailMainSprite = null;

    private float _spriteDelay = 0.1f;
    private float _spriteCurrentDelay = 0;
    private int _currentSpriteIdx = 0;
    private bool _isMoving = false;
    public bool IsItemDetail { get => _isItemDetail; set => _isItemDetail = value; }

    public bool IsMoving
    {
        get => _isMoving;
        set
        {
            if (IsItemDetail == true && value == false)
            {
                _isMoving = true;
                return;
            }
            _isMoving = value;

            SetSprite();
        }
    }

    private void OnEnable()
    {
        Image = GetComponent<Image>();
        if (Image == null) return;

        //if (SpriteArr == null || SpriteArr.Length == 0 && IsItemDetail == true)
        //    IsItemDetail = false;

        _currentSpriteIdx = 0;
        _spriteCurrentDelay = _spriteDelay;

        SetSprite();
    }
    private void Update()
    {
        if (Image == null) return;
        if (!_isMoving)
        {
            SetSprite();
            return;
        }

        if (SpriteArr == null || SpriteArr.Length == 0) return;

        _spriteCurrentDelay -= Time.deltaTime;
        if (_spriteCurrentDelay < 0)
        {
            _currentSpriteIdx++;
            _spriteCurrentDelay = 0;
            if (_currentSpriteIdx >= SpriteArr.Length)
                _currentSpriteIdx = 0;

            Image.sprite = SpriteArr[_currentSpriteIdx];
            _spriteCurrentDelay = _spriteDelay;
        }
    }
    private void SetSprite()
    {
        if (Image == null || MainSprite == null) return;

        if (IsItemDetail && useItemDetailSpriteArr)
        {
            if (MainSprite != ItemDetailMainSprite)
                MainSprite = ItemDetailMainSprite;

            if (MainSprite != Image.sprite)
                Image.sprite = ItemDetailMainSprite;
        }
        else
        {
            if (MainSprite != Image.sprite)
                Image.sprite = MainSprite;
        }
    }
} // end of class
