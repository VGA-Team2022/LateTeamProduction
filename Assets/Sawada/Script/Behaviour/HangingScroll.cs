using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CircleCollider2D))]
//掛け軸のクラス(起動することで子供と大人が切り替わる)
public class HangingScroll : MonoBehaviour,IRevers
{
    [SerializeField, Tooltip("掛け軸の画像。要素0が子供、要素1が大人")]
    Sprite[] _scrollImages = new Sprite[2];
    [SerializeField, Tooltip("案内のテキスト")]
    ScrollText _scroolText = null; 

    [Tooltip("プレイヤーを格納する変数")]
    PlayerController _playerController = null;
    [Tooltip("オブジェクトのRenderer")]
    SpriteRenderer _scrollRenderer = null;

    //後々修正
    UIManager _uiManager = null;
    SoundManager _soundManager = null;
    float _waitTime = 1f;

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
        _scrollRenderer = GetComponentInChildren<SpriteRenderer>();
        _scroolText.TextActivate(_playerController);

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
        if (collision.gameObject.tag == "Player")
        {
            _playerController = null;
            _scroolText.TextActivate(_playerController);
            Debug.Log("Out");
        }
    }

    public void ObjectRevers()
    {
        throw new NotImplementedException();
    }

    public void RendererChange(PlayerController player)
    {
        if (player.AdultState)
        {
            _scrollRenderer.sprite = _scrollImages[0];
        }
        else
        {
            _scrollRenderer.sprite = _scrollImages[1];
        }
    }
}
