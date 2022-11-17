using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン遷移時のフェード処理の管理
/// </summary>

public class FadeController : MonoBehaviour
{
    [SerializeField]float _time = 0;
    float _fadeSpeed = 0.2f;
    Image _image;

    void Start()
    {
       _image = GetComponentInChildren<Image>();
        StartCoroutine(Fade(false));
    }

    public IEnumerator Fade(bool isFadeOut)
    {
        while(true)
        {
            //徐々に明るくなる
            if (!isFadeOut)
            {
                _time -= Time.deltaTime;
                Color c = _image.color;
                c.a = _time / _fadeSpeed;
                _image.color = c;
                if(_time <= 0.0f)
                {
                    yield break;
                }
            }
            //徐々に暗くなる
            else
            {
                _time += Time.deltaTime;
                Color c = _image.color;
                c.a = _time / _fadeSpeed;
                _image.color = c;
                if (_time  >= 1.0f)
                {
                    //仮処理
                    SceneManager.LoadScene("test2");
                    yield break;
                }
                
            }
             yield return null;
        }
    }
}
