using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CriWare;
public class TittleSound : MonoBehaviour
{
    /// <summary>
    /// ボタンでこれ読んでシーン上のDecide音源呼んで下さい」
    /// </summary>
    /// <param name="source"></param>
    public void AudioPlay(CriAtomSource source)
    {
        source.Play();
    }
}
