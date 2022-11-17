using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

/// <summary>
/// ÉVÅ[ÉìëJà⁄
/// </summary>
public class SceneChanger : MonoBehaviour
{
    [SerializeField] GameObject fade;
    GameObject fadeCanvas;

    void Awake()
    {
        if (!FadeController._FadeInstance)
        {
            Instantiate(fade);
        }
        Invoke("findFadeObject", 0.2f);
    }

    void findFadeObject()
    {
        fadeCanvas = GameObject.FindGameObjectWithTag("Fade");
        fadeCanvas.GetComponent<FadeController>().fadeIn();
    }

    public async void ChangeScene(string name)
    {
        fadeCanvas.GetComponent<FadeController>().fadeOut();
        await Task.Delay(200);
        SceneManager.LoadScene(name);
        fadeCanvas.GetComponent<FadeController>().fadeIn();
    }
}
