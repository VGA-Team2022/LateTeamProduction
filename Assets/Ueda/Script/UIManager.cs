using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using IsGame;

public class UIManager : MonoBehaviour
{
    [SerializeField, Tooltip("タメ時間を表すスライダー")] Slider _chargeSlider = null;
    
    [SerializeField,Tooltip("残り時間を表示するテキスト")] TextMeshProUGUI _timerText = null;
    
    [SerializeField,Tooltip("クリア時に表示するUI")] GameObject _clearUI = null;

    [SerializeField, Tooltip("ゲームオーバー時に表示するUI")] GameObject _gameOverUI = null;

    [SerializeField,Tooltip("クリアタイムを表示するテキスト")] TextMeshProUGUI _clearTimeText = null;
    
    [SerializeField,Tooltip("カットイン用のアニメーター")] Animator _cutIn = null;

    [SerializeField, Tooltip("サウンドマネージャー")] SoundManager _soundManager = null;
    
    PlayerController _player = null;

    //Animator _chargeAnim = null;
    // Start is called before the first frame update

    void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        _clearUI.SetActive(false);
        _gameOverUI.SetActive(false);
        GameManager.Instance.UIManagerSet(this);
    }
    private void Update()
    {
        GameManager.Instance.Timer();
    }

    public void ChargeSlider(float charge ) // スライダーとひっくり返す対象のアニメーターを制御
    {
        if (charge >= _chargeSlider.maxValue) charge = _chargeSlider.maxValue;
        _chargeSlider.value = charge;

        //スライダーが満タンになったらプレイヤーのboolを変える
        if (charge == _chargeSlider.maxValue)
        {
            _player.PillowEnemy.ObjectRevers();
            _chargeSlider.value = 0;
            GameManager.Instance.CheckSleepingEnemy();
            _player.InformationReset();
        }
    }
    public void TimerText(float time)
    {
        _timerText.text = time.ToString("F0");
    }
    public void Clear(float clearTime)
    {
        _clearTimeText.text = clearTime.ToString("F0")+" 秒";
        _soundManager.GameClear();
        _clearUI.SetActive(true);
    }
    public void GameOver() 
    {
        _soundManager.GameOver();
        _gameOverUI.SetActive(true);
    }
    public void CutIn(bool before)
    {
        _soundManager.Cutin();
        _cutIn.SetBool("isChild",before);
    }
}
