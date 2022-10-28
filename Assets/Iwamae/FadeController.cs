using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public static bool isFadeInstance = false;

    public bool isFadeIn = false;//フェードインするフラグ
    public bool isFadeOut = false;//フェードアウトするフラグ

    public float alpha = 0.0f;//透過率、これを変化させる
    public float fadeSpeed = 0.2f;//フェードに掛かる時間

    void Start()
    {
        if (!isFadeInstance)//起動時
        {
            DontDestroyOnLoad(this);
            isFadeInstance = true;
        }
        else//起動時以外は重複しないようにする
        {
            Destroy(this);
        }
    }

    void Update()
    {
        if (isFadeIn)
        {
            alpha -= Time.deltaTime / fadeSpeed;
            if (alpha <= 0.0f)//透明になったら、フェードインを終了
            {
                isFadeIn = false;
                alpha = 0.0f;
            }
            this.GetComponentInChildren<Image>().color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
        else if (isFadeOut)
        {
            alpha += Time.deltaTime / fadeSpeed;
            if (alpha >= 1.0f)//真っ黒になったら、フェードアウトを終了
            {
                isFadeOut = false;
                alpha = 1.0f;
            }
            this.GetComponentInChildren<Image>().color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
    }

    public void fadeIn()
    {
        isFadeIn = true;
        isFadeOut = false;
    }

    public void fadeOut()
    {
        isFadeOut = true;
        isFadeIn = false;
    }
}
