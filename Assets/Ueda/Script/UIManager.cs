using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    /// <summary>タメ時間を表すスライダー</summary>
    [SerializeField] Slider _chargeSlider = null;
    /// <summary>残り時間を表示するテキスト</summary>
    [SerializeField] TextMeshProUGUI _timerText = null;
    /// <summary>クリア時に表示するUI</summary>
    [SerializeField] GameObject _clearUI = null;
    /// <summary>クリアタイムを表示するテキスト</summary>
    [SerializeField] TextMeshProUGUI _clearTimeText = null;
    /// <summary>ゲームオーバー時に表示するテキスト</summary>
    [SerializeField] GameObject _gameOverUI = null;

    PlayerController _player = null;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerController>();
        _clearUI.SetActive(false);
        _gameOverUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChargeSlider(float charge)
    {
        if(charge >= 1) charge = 1; 
        _chargeSlider.value = charge;
        //スライダーが満タンになったらプレイヤーのboolを変える
        if (_chargeSlider.value == _chargeSlider.maxValue)
        {
            _player.PillowEnemy.ReturnPillow = true;
            _chargeSlider.value = 0;
        }
    }
    public void TimerText(float time)
    {
        _timerText.text = time.ToString("F0");
    }
    public void Clear(float clearTime)
    {
        _clearTimeText.text = clearTime.ToString("F0");
        _clearUI.SetActive(true);
    }
    public void GameOver()
    {
        _gameOverUI.SetActive(true);
    }
}
