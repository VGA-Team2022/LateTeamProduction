using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ClickFade : MonoBehaviour
{
    //ステージセレクトから指定シーンへ飛ぶ
    [Header("フェードUIをセット"), SerializeField]
    GameObject _fadein;
    [Header("ステージ指定※ボタンそれぞれに"), SerializeField]
    string _stagename;
    [Header("UIのアニメーションをセット"), SerializeField]
    Animator _uianim;
    const int ONE_NUM = 1;
    public void HighlightCommand()
    {
        _uianim.SetInteger("Anumber", ONE_NUM);
    }
    public void NormalCommand()
    {
        _uianim.SetInteger("Anumber", 0);
    }
    public void ClickCommand()
    {
        _fadein.SetActive(true);
        Invoke(nameof(MoveScene), ONE_NUM); //マージするときはちゃんとコード見ろや澤田 問題の答えはここ
    }
    void MoveScene()
    {
        SceneManager.LoadScene(_stagename);
    }

}
