using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CircleCollider2D))]
//掛け軸のクラス(起動することで子供と大人が切り替わる)
public class HangingScroll : MonoBehaviour
{
    [SerializeField, Tooltip("掛け軸の画像。要素0が子供、要素1が大人")]
    Sprite[] _scrollImages = new Sprite[2];
    [SerializeField, Tooltip("")]
    ScrollText _scroolText = null; 

    [Tooltip("プレイヤーを格納する変数")]
    PlayerController _playerController = null;
    [Tooltip("オブジェクトのRenderer")]
    SpriteRenderer _scrollRenderer = null;

    //後々修正
    //GameManager _gameManager = null;
    UIManager _uiManager = null;
    SoundManager _soundManager = null;
    float _waitTime = 1f;


    public void Init()
    {
        _scrollRenderer = GetComponentInChildren<SpriteRenderer>();
        //_gameManager = gameManager;
        _scroolText.TextActivate(_playerController);
        //_playerController = _gameManager.Player;

        //後々修正
        _uiManager = FindObjectOfType<UIManager>();
        _soundManager = FindObjectOfType<SoundManager>();
    }

    IEnumerator PlayerInCollider()
    {
        while(_playerController)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                bool isAdultMode = !_playerController.AdultState;
                _uiManager.CutIn(!isAdultMode);
                _soundManager.Kakejikued();
                yield return new WaitForSeconds(_waitTime);
                _playerController.ModeChange(isAdultMode);
                if (isAdultMode)
                {
                    _scrollRenderer.sprite = _scrollImages[0];
                }
                else
                {
                    _scrollRenderer.sprite = _scrollImages[1];
                }
                yield break;
            }
            yield return null;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            _playerController = player;
            _scroolText.TextActivate(_playerController);
            StartCoroutine(PlayerInCollider());
            Debug.Log("In");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.gameObject == _gameManager.Player.gameObject)
        //{
        //    _playerController = null;
        //    _scroolText.TextActivate(_playerController);
        //    Debug.Log("Out");
        //}
    }
}
