using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CriWare;
/// <summary>
/// 音源を管理するコンポーネント
/// 関数がPublicになってるのはUnityEventで呼ぶためだ
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

    public void AudioPlay(CriAtomSource source)
    {
        source.Play();
    }
}
