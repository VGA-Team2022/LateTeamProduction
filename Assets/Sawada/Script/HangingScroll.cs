using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class HangingScroll : MonoBehaviour
{
    [SerializeField, Tooltip("掛け軸の画像。要素0が子供、要素1がおとな")]
    Sprite[] _scrollImages = new Sprite[2];

    [Tooltip("プレイヤーを格納する変数")]
    PlayerController _playerController = null;
    [Tooltip("オブジェクトのRenderer")]
    SpriteRenderer _scrollRenderer = null;
    [Tooltip("オブジェクトのCollider")]
    Collider2D[] _scrollCollider = null;


    private void Start()
    {
        Init();
    }
    public void Init()
    {
        _scrollRenderer = GetComponentInChildren<SpriteRenderer>();
        _scrollCollider = GetComponents<Collider2D>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            _playerController = player;
            StartCoroutine(PlayerInCollider());
            Debug.Log("In");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            _playerController = null;
            Debug.Log("Out");
        }
    }

    IEnumerator PlayerInCollider()
    {
        while(_playerController != null)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                bool isAdultMode = !_playerController.AdultState;
                if (isAdultMode)
                {
                    _scrollRenderer.sprite = _scrollImages[0];
                }
                else
                {
                    _scrollRenderer.sprite = _scrollImages[1];
                }
                _playerController.ModeChange(isAdultMode);
                yield break;
            }
            yield return null;
        }
    }
}
