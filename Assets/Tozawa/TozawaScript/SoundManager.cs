using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CriWare;
/// <summary>
/// 音源を管理するコンポーネント
/// 関数がPublicになってるのはUnityEventで呼ぶためだ
/// イベントやボタン設定時は関数は引数の型がCriAtomSourceになってるのを選べ（ジェネリック）
/// </summary>
public class SoundManager : MonoBehaviour
{
    [Header("開始時に呼ばれるべき処理"),  SerializeField] 
    UnityEvent _onGameStart;
    [Header("ゲームオーバー時に呼ばれるべき処理"),  SerializeField] 
    UnityEvent _onGameOver;
    [Header("クリア時に呼ばれるべき処理"),SerializeField] 
    UnityEvent _onGameClear;
    public void GameStart()
    {
        _onGameStart.Invoke();
    }
    public void GameOver()
    {
        _onGameOver.Invoke();
    }
    public void GameClear()
    {
        _onGameClear.Invoke();
    }

    public void AudioPlay(CriAtomSource source)
    {
        source.Play();
    }
}
