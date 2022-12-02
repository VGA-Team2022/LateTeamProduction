using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{//配列やリストにしていないのは各自連携を取りやすくするため
    [SerializeField, Header("子供状態のanimationstateの名前{待機上向き}")]
    string _childStandbyUp = "";
    [SerializeField, Header("子供状態のanimationstateの名前{待機下向き}")]
    string _childStandbyDown = "";
    [SerializeField, Header("子供状態のanimationstateの名前{待機右向き}")]
    string _childStandbyRight = "";
    [SerializeField, Header("子供状態のanimationstateの名前{待機左向き}")]
    string _childStandbyLeft = "";
    [SerializeField, Header("子供状態のanimationstateの名前{行動上向き}")]
    string _childActionUp = "";
    [SerializeField, Header("子供状態のanimationstateの名前{行動下向き}")]
    string _childActionDown = "";
    [SerializeField, Header("子供状態のanimationstateの名前{行動右向き}")]
    string _childActionRight = "";
    [SerializeField, Header("子供状態のanimationstateの名前{行動左向き}")]
    string _childActionLeft = "";
    [SerializeField, Header("子供状態のanimationstateの名前{カットイン用}")]
    string _childCutIn = "";

    [SerializeField, Header("大人状態のanimationstateの名前{待機上向き}")]
    string _adultStandbyUp = "";
    [SerializeField, Header("大人状態のanimationstateの名前{待機下向き}")]
    string _adultStandbyDown = "";
    [SerializeField, Header("大人状態のanimationstateの名前{待機右向き}")]
    string _adultStandbyRight = "";
    [SerializeField, Header("大人状態のanimationstateの名前{待機左向き}")]
    string _adultStandbyLeft = "";
    [SerializeField, Header("大人状態のanimationstateの名前{行動上向き}")]
    string _adultActionUp = "";
    [SerializeField, Header("大人状態のanimationstateの名前{行動下向き}")]
    string _adultActionDown = "";
    [SerializeField, Header("大人状態のanimationstateの名前{行動右向き}")]
    string _adultActionRight = "";
    [SerializeField, Header("大人状態のanimationstateの名前{行動左向き}")]
    string _adultActionLeft = "";
    [SerializeField, Header("子供状態のanimationstateの名前{カットイン用}")]
    string _adultCutIn = "";
    PlayerController _player;
    Animator _anim;
    /// <summary>止まる直前の速度方向</summary>
    Vector2 _lastMovejoyStick;
    private void Start()
    {
        _player = GetComponent<PlayerController>();
        _anim = GetComponent<Animator>();
    }

    //private void Update()
    //{
    //    if (_player.JoyX != 0 && _player.JoyY != 0)
    //    {
    //        _lastMovejoyStick.x = _player.JoyX;
    //        _lastMovejoyStick.y = _player.JoyY;
    //    }
    //}
    /// <summary>ModeChange()関数で使用</summary>
    public void ModeChangeAnim()//Downの方向の待機状態にする
    {
        if (!_anim)
        {
            return;
        }
        if (!_player.AdultState)//子供だったら
        {
            _anim.Play(_adultStandbyDown);
            _anim.Play(_adultCutIn);
        }
        else
        {
            _anim.Play(_childStandbyDown);
            _anim.Play(_childCutIn);
        }

    }
    /// <summary>AnimPlay関数で使用</summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void MoveAnim(float x, float y)
    {
        if (!_anim)
        {
            return;
        }
        if (x != 0 && y != 0)//動いている時
        {
            if (-0.5 < y && y < 0.5)//左右
            {
                if (0.5 < x && x < 1)//右
                {
                    if (!_player.AdultState)
                        _anim.Play(_childActionRight);
                    else
                        _anim.Play(_adultActionRight);
                }
                else if (-1 < x && x < -0.5)//左
                {
                    if (!_player.AdultState)
                        _anim.Play(_childActionLeft);
                    else
                        _anim.Play(_adultActionLeft);
                }
            }
            if (-0.5 < x && x < 0.5)//上下共通の条件
            {
                if (0.5 < y && y < 1)//上
                {
                    if (!_player.AdultState)
                        _anim.Play(_childActionUp);
                    else
                        _anim.Play(_adultActionUp);
                }
                else if (-1 < y && y < -0.5)//下
                {
                    if (!_player.AdultState)
                        _anim.Play(_childActionDown);
                    else
                        _anim.Play(_adultActionDown);
                }
            }
        }
        else//止まっている時
        {
            if (-0.5 < _lastMovejoyStick.y && _lastMovejoyStick.y < 0.5)//左右
            {
                if (0.5 < _lastMovejoyStick.x && _lastMovejoyStick.x < 1)//右
                {
                    if (!_player.AdultState)
                        _anim.Play(_childStandbyRight);
                    else
                        _anim.Play(_adultStandbyRight);
                }
                else if (-1 < _lastMovejoyStick.x && _lastMovejoyStick.x < -0.5)//左
                {
                    if (!_player.AdultState)
                        _anim.Play(_childStandbyLeft);
                    else
                        _anim.Play(_adultStandbyLeft);
                }
            }
            if (-0.5 < _lastMovejoyStick.x && _lastMovejoyStick.x < 0.5)//上下
            {
                if (0.5 < _lastMovejoyStick.y && _lastMovejoyStick.y < 1)//上
                {
                    if (!_player.AdultState)
                        _anim.Play(_childStandbyUp);
                    else
                        _anim.Play(_adultStandbyUp);
                }
                else if (-1 < _lastMovejoyStick.y && _lastMovejoyStick.y < -0.5)//下
                {
                    if (!_player.AdultState)
                        _anim.Play(_childStandbyDown);
                    else
                        _anim.Play(_adultStandbyDown);
                }
            }
        }
    }

    /// <summary>プレイヤーをEnemyの左右どちらかに移動させてアニメーション再生</summary>
    public void TranslatePlayerPosAnimPlay(float speed, float toleranceDis)
    {
        if (!_anim)
        {
            return;
        }
        if (Mathf.Abs(transform.position.x - _player.ReturnPillowPos.x) > toleranceDis)
        {
            if (transform.position.x > _player.ReturnPillowPos.x)
            {
                transform.Translate(Vector2.left * Time.deltaTime * speed);
                if (!_player.AdultState)
                    _anim.Play(_childActionLeft);
                else
                    _anim.Play(_adultActionLeft);
            }
            else if (transform.position.x < _player.ReturnPillowPos.x)
            {
                transform.Translate(Vector2.right * Time.deltaTime * speed);
                if (!_player.AdultState)
                    _anim.Play(_childActionRight);
                else
                    _anim.Play(_adultActionRight);
            }
        }
        else
        {
            if (transform.position.y > _player.ReturnPillowPos.y)
            {
                transform.Translate(Vector2.down * Time.deltaTime * speed);
                if (!_player.AdultState)
                    _anim.Play(_childActionDown);
                else
                    _anim.Play(_adultActionDown);
            }
            else if (transform.position.y < _player.ReturnPillowPos.y)
            {
                transform.Translate(Vector2.up * Time.deltaTime * speed);
                if (!_player.AdultState)
                    _anim.Play(_childActionUp);
                else
                    _anim.Play(_adultActionUp);
            }
        }
    }
    /// <summary>枕を返す際に右側なのか左側なのか</summary>
    /// <param name="pos">敵の位置</param>
    public void RightorLeft(Vector2 pos)
    {
        if (!_anim)
        {
            return;
        }
        if (pos.x > transform.position.x)//右
        {
            if (!_player.AdultState)
                _anim.Play(_childStandbyRight);//子供
            else
                _anim.Play(_adultStandbyRight);
        }
        else//左
        {
            if (!_player.AdultState)
                _anim.Play(_childStandbyLeft);//子供
            else
                _anim.Play(_adultStandbyLeft);
        }
    }
}
