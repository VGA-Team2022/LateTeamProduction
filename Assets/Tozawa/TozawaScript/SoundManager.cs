using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CriWare;
/// <summary>
/// 音源を管理するコンポーネント
/// 関数がPublicになってるのはUnityEventで呼ぶためだ
/// イベント設定時は関数の引数の型がCriAtomSourceになってるのを選べ（ジェネリック）
/// </summary>
public class SoundManager : MonoBehaviour
{
    [Header("開始時に呼ばれるべき処理"),  SerializeField] 
    UnityEvent<CriAtomSource> _onGameStart;
    [Header("ゲームオーバー時に呼ばれるべき処理"),  SerializeField] 
    UnityEvent<CriAtomSource> _onGameOver;
    [Header("クリア時に呼ばれるべき処理"),SerializeField] 
    UnityEvent<CriAtomSource> _onGameClear;
    public void GameStart(CriAtomSource source)
    {
        _onGameStart.Invoke(source);
    }

    /// <summary>
    /// この関数はボタンなどから呼ばない。直接音源を指定して渡せないからだ
    /// Eventを介して音源指定することでボタンなどからでも再生できる
    /// </summary>
    /// <param name="source"></param>
    public void AudioPlay(CriAtomSource source)
    {
        source.Play();
    }
}
