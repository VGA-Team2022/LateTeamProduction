using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] 
    string _loadSceneName = "sceneの名前";
    [SerializeField] 
    Image _fadePanel = null;
    [SerializeField,Header("シーン開始時のパネルの色が変わる時間")] 
    float _fastFadeTime = 1f;
    [SerializeField,Header("ボタンを押した後のパネルの色が変わる時間")]
    float _loadFadeTime = 1f;
    bool _loadStart = false;


    private void Start()
    {//黒から透明
        StartFade();
    }
    // Update is called once per frame
    void Update()
    {
        if (_loadStart)//trueだったら
        {
            LoadPlay(_loadSceneName);
        }
    }
    void StartFade()
    {
        _fadePanel.DOColor(Color.clear, _fastFadeTime);
    }
    public void LoadScene()
    {//リロード以外はこの関数を呼ぶ
        _loadStart = true;
    }

    public void LeloadScene()
    {
        LoadPlay(SceneManager.GetActiveScene().name);
    }
    void LoadPlay(string sceneName)
    {//この中で指定された名前のシーンに飛ぶ。
        if (_fadePanel)
        {
            _fadePanel.DOColor(Color.black, _loadFadeTime).OnComplete(() => SceneManager.LoadScene(sceneName));
            _loadStart = false;
            Debug.Log("遷移完了");
        }
        else
        {
            SceneManager.LoadScene(sceneName);
            _loadStart = false;
            Debug.Log("遷移完了");
        }
    }
}
