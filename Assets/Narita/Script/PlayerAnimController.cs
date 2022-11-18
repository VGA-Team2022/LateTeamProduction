using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    PlayerController _player;
    Animator _anim;
    /// <summary>止まる直前の速度方向</summary>
    Vector2 _lastMovejoyStick;
    private void Start()
    {
        _player = GetComponent<PlayerController>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_player.JoyX != 0 && _player.JoyY != 0)
        {
            _lastMovejoyStick.x = _player.JoyX;
            _lastMovejoyStick.y = _player.JoyY;
        }
    }
    /// <summary>ModeChange関数で使用</summary>
    public void ModeChangeAnim()
    {
        if (!_anim)
        {
            return;
        }
        if (!_player.AdultState)
        {
            _anim.Play("");//子供用待機アニメーション
        }
        else
        {
            _anim.Play("");//大人用待機アニメーション
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
        if (!_player.AdultState)
        {
            if (x != 0 && y != 0)
            {
                if (-0.5 < y && y < 0.5)//左右
                {
                    if (0.5 < x && x < 1)//右
                    {
                        _anim.Play("");
                    }
                    else if (-1 < x && x < -0.5)//左
                    {
                        _anim.Play("");
                    }
                }
                if (-0.5 < x && x < 0.5)//上下
                {
                    if (0.5 < y && y < 1)//上
                    {
                        _anim.Play("");
                    }
                    else if (-1 < y && y < -0.5)//下
                    {
                        _anim.Play("");
                    }
                }
            }
            else
            {
                if (-0.5 < _lastMovejoyStick.y && _lastMovejoyStick.y < 0.5)//左右
                {
                    if (0.5 < _lastMovejoyStick.x && _lastMovejoyStick.x < 1)//右
                    {
                        _anim.Play("");
                    }
                    else if (-1 < _lastMovejoyStick.x && _lastMovejoyStick.x < -0.5)//左
                    {
                        _anim.Play("");
                    }
                }
                if (-0.5 < _lastMovejoyStick.x && _lastMovejoyStick.x < 0.5)//上下
                {
                    if (0.5 < _lastMovejoyStick.y && _lastMovejoyStick.y < 1)//上
                    {
                        _anim.Play("");
                    }
                    else if (-1 < _lastMovejoyStick.y && _lastMovejoyStick.y < -0.5)//下
                    {
                        _anim.Play("");
                    }
                }
            }
        }
        else
        {
            if (x != 0 && y != 0)
            {
                if (-0.5 < y && y < 0.5)//左右
                {
                    if (0.5 < x && x < 1)//右
                    {
                        _anim.Play("");
                    }
                    else if (-1 < x && x < -0.5)//左
                    {
                        _anim.Play("");
                    }
                }
                if (-0.5 < x && x < 0.5)//上下
                {
                    if (0.5 < y && y < 1)//上
                    {
                        _anim.Play("");
                    }
                    else if (-1 < y && y < -0.5)//下
                    {
                        _anim.Play("");
                    }
                }
            }
            else
            {
                if (-0.5 < _lastMovejoyStick.y && _lastMovejoyStick.y < 0.5)//左右
                {
                    if (0.5 < _lastMovejoyStick.x && _lastMovejoyStick.x < 1)//右
                    {
                        _anim.Play("");
                    }
                    else if (-1 < _lastMovejoyStick.x && _lastMovejoyStick.x < -0.5)//左
                    {
                        _anim.Play("");
                    }
                }
                if (-0.5 < _lastMovejoyStick.x && _lastMovejoyStick.x < 0.5)//上下
                {
                    if (0.5 < _lastMovejoyStick.y && _lastMovejoyStick.y < 1)//上
                    {
                        _anim.Play("");
                    }
                    else if (-1 < _lastMovejoyStick.y && _lastMovejoyStick.y < -0.5)//下
                    {
                        _anim.Play("");
                    }
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
                {
                    _anim.Play("");
                }
                else
                {
                    _anim.Play("");
                }
            }
            else if (transform.position.x < _player.ReturnPillowPos.x)
            {
                transform.Translate(Vector2.right * Time.deltaTime * speed);
                if (!_player.AdultState)
                {
                    _anim.Play("");
                }
                else
                {
                    _anim.Play("");
                }
            }
        }
        else
        {
            if (transform.position.y > _player.ReturnPillowPos.y)
            {
                transform.Translate(Vector2.down * Time.deltaTime * speed);
                if (!_player.AdultState)
                {
                    _anim.Play("");
                }
                else
                {
                    _anim.Play("");
                }

            }
            else if (transform.position.y < _player.ReturnPillowPos.y)
            {
                transform.Translate(Vector2.up * Time.deltaTime * speed);
                if (!_player.AdultState)
                {
                    _anim.Play("");
                }
                else
                {
                    _anim.Play("");
                }
            }
        }
    }
    /// <summary>枕を返す際に右側なのか左側なのか</summary>
    /// <param name="pos"></param>
    public void RightorLeft(Vector2 pos)
    {
        if (!_anim)
        {
            return;
        }
        if (pos.x > 0)//右
        {
            if (!_player.AdultState)
            {
                _anim.Play("");//子供
            }
            else
            {
                _anim.Play("");
            }
        }
        else//左
        {
            if (!_player.AdultState)
            {
                _anim.Play("");//子供
            }
            else
            {
                _anim.Play("");
            }
        }
    }
}
