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

    PlayerController _player = null;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerController>();
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
            //_player.PillowEnemy.ReturnPillow = true;
            _chargeSlider.value = 0;
        }
    }
    public void TimerText(float time)
    {
        _timerText.text = time.ToString("F0");
    }
}
