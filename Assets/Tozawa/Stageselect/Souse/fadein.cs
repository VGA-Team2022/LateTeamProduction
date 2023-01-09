using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Fadein : MonoBehaviour
{
    public float SaveX;
    public float SaveY;
    public float r;
    public float g;
    public float b;
    public bool scenetrg = true;
    Image imagecolor;
    public float MaxOutTime;

    private float FadeOutTime = 0f;

    public string NextScene;

    private float alphacolor = 0.0f;

    void Start()
    {
        imagecolor = this.GetComponent<Image>();
    }

    void Update()
    {

        FadeOutTime += Time.deltaTime;
        if (FadeOutTime < MaxOutTime)
        {
            alphacolor = FadeOutTime ;
            imagecolor.color = new Color(r, g, b, alphacolor);
            imagecolor = this.GetComponent<Image>();
        }
        else if (FadeOutTime > MaxOutTime)
        {
            if (scenetrg == true)
            {
                //GManager.instance.posX = SaveX;
                //PlayerPrefs.SetFloat("posX", GManager.instance.posX);
                //GManager.instance.posY = SaveY;
                //PlayerPrefs.SetFloat("posY", GManager.instance.posY);
                PlayerPrefs.Save();
                SceneManager.LoadScene(NextScene);
            }
        }


    }
}
