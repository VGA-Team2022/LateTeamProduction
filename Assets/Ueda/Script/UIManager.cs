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
    //bool _isRange = false;
    PlayerController _player = null;

    //Animator _chargeAnim = null;
    // Start is called before the first frame update

    void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        _clearUI.SetActive(false);
        _gameOverUI.SetActive(false);
        GameManager.Instance.Initialize(this);
    }
    private void Update()
    {
        GameManager.Instance.Timer();
    }

    public void ChargeSlider(float charge ) // スライダーとひっくり返す対象のアニメーターを制御
    {

        //if (_player._returnPillowInPos)
        //{
        //    _chargeAnim.speed = 1;
        //}
        //else
        //{
        //    _chargeAnim.speed = 0;
        //}
        //if (charge == 0)
        //{
        //    _isRange = true;
        //    return;
        //}
        if (charge >= _chargeSlider.maxValue) charge = _chargeSlider.maxValue;
        //if (_isRange)
        //{
        //    _chargeAnim.Play("", 0, 0);
        //    _isRange = false;
        //}
        _chargeSlider.value = charge;

        //スライダーが満タンになったらプレイヤーのboolを変える
        if (charge == _chargeSlider.maxValue)
        {
            _player.PillowEnemy.ObjectRevers();
            _chargeSlider.value = 0;
            GameManager.Instance.CheckSleepingEnemy();
            _player.InformationReset();
            //_isRange = true;
        }
    }
    public void TimerText(float time)
    {
        _timerText.text = time.ToString("F0");
    }
    public void Clear(float clearTime)
    {
        _clearTimeText.text = clearTime.ToString("F0")+" 秒";
        _clearUI.SetActive(true);
    }
    public void GameOver() 
    {
        _gameOverUI.SetActive(true);
    }
    public void CutIn(bool before)
    {
        _cutIn.SetBool("isChild",before);
    }
}
