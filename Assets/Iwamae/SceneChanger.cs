using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

/// <summary>
/// シーン遷移
/// </summary>
public class SceneChanger : MonoBehaviour
{
    [Tooltip("シーン遷移先の名前")]
    [SerializeField] string _sceneName;

    public GameObject fade;//インスペクタからPrefab化したCanvasを入れる
    public GameObject fadeCanvas;//操作するCanvas、タグで探す

    void Start()
    {
        if (!FadeController.isFadeInstance)//isFadeInstanceは後で用意する
        {
            Instantiate(fade);
        }
        Invoke("findFadeObject", 0.02f);//起動時用にCanvasの召喚をちょっと待つ
    }

    void findFadeObject()
    {
        fadeCanvas = GameObject.FindGameObjectWithTag("Fade");//Canvasをみつける
        fadeCanvas.GetComponent<FadeController>().fadeIn();//フェードインフラグを立てる
    }

    public async void ChangeScene(string name)
    {
        fadeCanvas.GetComponent<FadeController>().fadeOut();//フェードアウトフラグを立てる
        await Task.Delay(200);//暗転するまで待つ
        SceneManager.LoadScene(name);
    }
}
