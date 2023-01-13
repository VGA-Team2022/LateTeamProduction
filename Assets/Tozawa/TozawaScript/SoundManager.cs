using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CriWare;
/// <summary>
/// シーン上のサウンドマネージャーを参照して！
/// 音源を管理するコンポーネント
/// 関数がPublicになってるのはUnityEventで呼ぶためだ
/// イベント設定時は関数は引数の型がCriAtomSourceになってるのを選べ（ジェネリック）
/// </summary>
public class SoundManager : MonoBehaviour
{
    [Header("開始時に呼ばれるべき処理"),  SerializeField,Tooltip("開始時の処理が格納されたUnityEvent")] 
    UnityEvent _onGameStart;
    [Header("ゲームオーバー時に呼ばれるべき処理"),  SerializeField, Tooltip("ゲームオーバー時の処理が格納されたUnityEvent")] 
    UnityEvent _onGameOver;
    [Header("クリア時に呼ばれるべき処理"),SerializeField, Tooltip("クリア時の処理が格納されたUnityEvent")] 
    UnityEvent _onGameClear;
    [Header("枕を返した際に呼ばれるべき処理"), SerializeField, Tooltip("枕を返した時の処理が格納されたUnityEvent")]
    UnityEvent _onMakuraReverse;
    [Header("キャンセルボタンを押した際に呼ばれるべき処理"), SerializeField, Tooltip("キャンセルボタンを押した時の処理が格納されたUnityEvent")]
    UnityEvent _onCanceled;
    [Header("ポーズボタンを押した際に呼ばれるべき処理"), SerializeField, Tooltip("ポーズボタンを押したの処理が格納されたUnityEvent")]
    UnityEvent _onPaused;
    [Header("決定ボタンを押した際に呼ばれるべき処理"), SerializeField,Tooltip("決定ボタンを時の処理が格納されたUnityEvent")]
    UnityEvent _onDecited;
    [Header("掛け軸した際に呼ばれるべき処理"), SerializeField, Tooltip("掛け軸時の処理が格納されたUnityEvent")]
    UnityEvent _onKakejikued;
    [Header("カットインした際に呼ばれるべき処理"), SerializeField, Tooltip("カットインした時の処理が格納されたUnityEvent")]
    UnityEvent _onCutined;
    [Header("寝てる人接近際に呼ばれるべき処理"), SerializeField, Tooltip("寝てる人接近時の処理が格納されたUnityEvent")]
    UnityEvent _onSleepNear;
    [Header("寝てる人接非接近に呼ばれるべき処理"), SerializeField, Tooltip("寝てる人非接近時の処理が格納されたUnityEvent")]
    UnityEvent _onSleepFar;
    [Header("発見された時に呼ばれるべき処理"), SerializeField, Tooltip("発見された時の処理が格納されたUnityEvent")]
    UnityEvent _onDiscovered;
    [Header("ゲージ上昇中に呼ばれるべき処理"), SerializeField, Tooltip("ゲージ上昇中の処理が格納されたUnityEvent")]
    UnityEvent _onGauging;
    [Header("ゲージ上昇停止時に呼ばれるべき処理"), SerializeField, Tooltip("ゲージ上昇停止時の処理が格納されたUnityEvent")]
    UnityEvent _onGaugeStop;
    private void Start()
    {
        GameStart();
    }
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
        _onMakuraReverse.Invoke();
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
    /// <summary>
    /// 寝てる奴の近くいる時？に一回呼んでください
    /// </summary>
    public void SleepingVoice()
    {
        _onSleepNear.Invoke();
    }
    /// <summary>
    /// 寝てる奴の近くから離れたか殺した時に一回呼んでください
    /// </summary>
    public void KillSleeping()
    {
        _onSleepFar.Invoke();
    }
    /// <summary>
    /// 発見された時に一回呼んでください
    /// </summary>
    public void Discoverd()
    {
        _onDiscovered.Invoke();
    }
    /// <summary>
    /// ゲージ開始時に一回呼んでください
    /// </summary>
    public void Gauging()
    {
        _onGauging.Invoke();
    }
    /// <summary>
    /// ゲージ停止時に一回呼んでください
    /// </summary>
    public void GaugeStop()
    {
        _onGaugeStop.Invoke();
    }
    public void AudioPlay(CriAtomSource source)
    {
        source.Play();
    }
}
