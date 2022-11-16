using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

/// <summary>
/// シーン遷移
/// </summary>
public class SceneChanger : MonoBehaviour
{
    [Header("フェードキャンバスのプレハブ入れる場所")]
    [SerializeField] GameObject fade;
    GameObject _fadeCanvas;

    void FindFadeCanvas()
    {
        _fadeCanvas = GameObject.FindGameObjectWithTag("Fade");
        _fadeCanvas.GetComponent<FadeController>().FadeIn();
    }

    public async void ChangeScene(string name)
    {
        _fadeCanvas.GetComponent<FadeController>().FadeOut();
        //200ミリ秒(0.2秒)待つ
        await Task.Delay(200);
        SceneManager.LoadScene(name);
        _fadeCanvas.GetComponent<FadeController>().FadeIn();
    }

    void Awake()
    {
        //0.2秒後にFindFadeCanvasメソッドを実行
        if (!FadeController._FadeInstance)
        {
            Instantiate(fade);
        }
        Invoke("FindFadeCanvas", 0.2f);
    }
}
