using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField, Tooltip("タメ時間を表すスライダー")] Slider _chargeSlider = null;
    
    [SerializeField, Tooltip("タメの完了度合いを表すオブジェクト")] GameObject _returnSign = null;
    
    [SerializeField,Tooltip("残り時間を表示するテキスト")] TextMeshProUGUI _timerText = null;
    
    [SerializeField,Tooltip("クリア時に表示するUI")] GameObject _clearUI = null;
    
    [SerializeField,Tooltip("クリアタイムを表示するテキスト")] TextMeshProUGUI _clearTimeText = null;
    
    [SerializeField,Tooltip("カットイン用のアニメーター")] Animator _cutIn = null;
    bool _isRange = false;
    PlayerController _player = null;
    Animator _returnSignAnim = null;
    // Start is called before the first frame update
    
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _returnSignAnim = _returnSign.GetComponent<Animator>();
        _clearUI.SetActive(false);
        _returnSign.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChargeSlider(float charge)
    {
        
        if(_player._returnPillowInPos)
        {
            _returnSignAnim.speed = 1;
        }
        else
        {
            _returnSignAnim.speed = 0;
        }
        if (charge == 0)
        {
            _returnSign.SetActive(false);
            _isRange = true;
            return;
        }
        if(charge >= 1) charge = 1; 
        if(_isRange)
        {
            _returnSignAnim.Play("",0,0);
            _isRange = false;
        }
        _chargeSlider.value = charge;
        _returnSign.GetComponent<Slider>().value = charge;

        if(_player.PillowEnemyObject != null) _returnSign.transform.position = _player.PillowEnemyObject.transform.position + new Vector3(0, 1, 0);
        _returnSign.SetActive(true);

        //スライダーが満タンになったらプレイヤーのboolを変える
        if (charge == 1)
        {
            
            _player.PillowEnemy.ReturnPillow = true;
            _chargeSlider.value = 0;
            
            _player.InformationReset();
            _isRange = true;
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
    public void CutIn(bool before)
    {
        _cutIn.SetBool("isChild",before);
    }
}
