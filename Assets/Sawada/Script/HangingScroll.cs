using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class HangingScroll : MonoBehaviour
{
    [SerializeField, Tooltip("掛け軸の画像。要素0が子供、要素1がおとな")]
    Sprite[] _scrollImages = new Sprite[2];
    [Tooltip("オブジェクトのRenderer")]
    SpriteRenderer _scrollRenderer = null;

    public void Init()
    {
        _scrollRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player) && Input.GetButtonDown("Diside"))
        {
            bool isAdultMode = !player.AdultState;
            if (isAdultMode)
            {
                _scrollRenderer.sprite = _scrollImages[0];
            }
            else
            {
                _scrollRenderer.sprite = _scrollImages[1];
            }
            player.ModeChange(isAdultMode);
        }
    }
}
