using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImageRollUp : MonoBehaviour
{
    [SerializeField,Header("Ø‚è‘Ö‚¦‚½‚¢‰æ‘œ‚ğ‚±‚±‚É“ü‚ê‚Ä‚­‚¾‚³‚¢")]
    Sprite[] _asobikata;
    [SerializeField]
    Image _thisImage;
    int _indexNum = 0;
    public void ChangeImage()
    {
        _indexNum++;
        _thisImage.sprite = _asobikata[_indexNum % _asobikata.Length];
    }
}
