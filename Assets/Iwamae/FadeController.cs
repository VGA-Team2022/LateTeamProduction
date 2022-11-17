using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// シーン遷移時のフェード処理の管理
/// </summary>

public class FadeController : MonoBehaviour
{
    public static bool _FadeInstance = false;
    public bool _isFadeIn = false;
    public bool _isFadeOut = false;
    public float _alpha = 0.0f;
    public float _fadeSpeed = 0.2f;

    void Awake()
    {
        if (!_FadeInstance)
        {
            DontDestroyOnLoad(this);
            _FadeInstance = true;
        }
        else
        {
            Destroy(this);
        }
    }

    void Update()
    {
        if (_isFadeIn)
        {
            _alpha -= Time.deltaTime / _fadeSpeed;
            if (_alpha <= 0.0f)
            {
                _isFadeIn = false;
                _alpha = 0.0f;
            }
            this.GetComponentInChildren<Image>().color = new Color(0.0f, 0.0f, 0.0f, _alpha);
        }
        else if (_isFadeOut)
        {
            _alpha += Time.deltaTime / _fadeSpeed;
            if (_alpha >= 1.0f)
            {
                _isFadeOut = false;
                _alpha = 1.0f;
            }
            this.GetComponentInChildren<Image>().color = new Color(0.0f, 0.0f, 0.0f, _alpha);
        }
    }

    public void fadeIn()
    {
        _isFadeIn = true;
        Debug.Log("フェードイン");
    }

    public void fadeOut()
    {
        _isFadeOut = true;
        Debug.Log("フェードアウト");
    }
}
