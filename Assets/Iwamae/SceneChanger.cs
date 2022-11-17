using UnityEngine;

/// <summary>
/// シーン遷移
/// </summary>
public class SceneChanger : MonoBehaviour
{


    GameObject _fadeCanvas;

    void Awake()
    {
        _fadeCanvas = GameObject.FindGameObjectWithTag("Fade");
    }

    //ボタンを押したときに実行する処理(仮処理)
    public void ChangeScene(string name)
    {
        var fadeIn = _fadeCanvas.GetComponent<FadeController>().Fade(true);
        StartCoroutine(fadeIn);
    }
}
