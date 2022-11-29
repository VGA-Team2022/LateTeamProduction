using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CriWare;
/// <summary>
/// シーン上のサウンドマネージャーを参照して！
/// 関数がPublicになってるのはUnityEventで呼ぶため
/// イベント設定時は関数は引数の型がCriAtomSourceになってるのを選べ（ジェネリック）
/// </summary>
public class SoundManager : MonoBehaviour
{
    [Header("開始時に呼ばれるべき処理"),  SerializeField] 
    UnityEvent _onGameStart;
    [Header("ゲームオーバー時に呼ばれるべき処理"),  SerializeField] 
    UnityEvent _onGameOver;
    [Header("クリア時に呼ばれるべき処理"),SerializeField] 
    UnityEvent _onGameClear;
    [Header("枕を返した際に呼ばれるべき処理"), SerializeField]
    UnityEvent _onMakuraReverse;
    [Header("キャンセルボタンを押した際に呼ばれるべき処理"), SerializeField]
    UnityEvent _onCanceled;
    [Header("ポーズボタンを押した際に呼ばれるべき処理"), SerializeField]
    UnityEvent _onPaused;
    [Header("決定ボタンを押した際に呼ばれるべき処理"), SerializeField]
    UnityEvent _onDecited;
    [Header("掛け軸した際に呼ばれるべき処理"), SerializeField]
    UnityEvent _onKakejikued;
    [Header("カットインした際に呼ばれるべき処理"), SerializeField]
    UnityEvent _onCutined;
    /// <summary>
    /// ゲーム開始の時に一回呼んでください
    /// </summary>
    public void GameStart()
    {
        _onGameStart.Invoke();
    }

    /// <summary>
    /// ゲームオーバーの時に一回呼んでください
    /// </summary>
    public void GameOver()
    {
        _onGameOver.Invoke();
    }

    /// <summary>
    /// ゲームクリアの時に一回呼んでください
    /// </summary>
    public void GameClear()
    {
        _onGameClear.Invoke();
    }

    /// <summary>
    /// 枕を返したときに時に一回呼んでください
    /// </summary>
    public void MakuraReverse()
    {
        _onCanceled.Invoke();
    }

    /// <summary>
    /// キャンセルした時に一回呼んでください
    /// </summary>
    public void Canceled()
    {
        _onCanceled.Invoke();
    }

    /// <summary>
    /// ポーズした時に一回呼んでください
    /// </summary>
    public void Paused()
    {
        _onPaused.Invoke();
    }
    /// <summary>
    /// 決定した時に一回呼んでください
    /// </summary>
    public void Decited()
    {
        _onPaused.Invoke();
    }

    /// <summary>
    /// 掛け軸した時に一回呼んでください
    /// </summary>
    public void Kakejikued()
    {
        _onKakejikued.Invoke();
    }
    /// <summary>
    /// カットインした時に一回呼んでください
    /// </summary>
    public void Cutin()
    {
        _onCutined.Invoke();
    }
    public void AudioPlay(CriAtomSource source)
    {
        source.Play();
    }
}
