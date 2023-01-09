using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Clickfade : MonoBehaviour
{
    //ステージセレクトから指定シーンへ飛ぶ
    [Header("フェードUIをセット"),SerializeField]
    GameObject _fadein;
    [Header("ステージ指定※ボタンそれぞれに"), SerializeField]
    string _stagename ;
    [Header("UIのアニメーションをセット"), SerializeField]
    Animator _uianim;
    const int ONE_NUM = 1;
    public void highlightCommand()
    {
        _uianim.SetInteger("Anumber", ONE_NUM);
    }
    public void normalCommand()
    {
        _uianim.SetInteger("Anumber", 0);
    }
    public void clickCommand()
    {
        _fadein.SetActive(true);
        Invoke("MoveScene", ONE_NUM);
    }
    void MoveScene()
    {
        SceneManager.LoadScene(_stagename);
    }
   
}
