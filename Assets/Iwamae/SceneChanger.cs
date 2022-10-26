using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン遷移
/// </summary>
public class SceneChanger : MonoBehaviour
{
    [Tooltip("シーン遷移先の名前")]
    [SerializeField] string _sceneName;

    public void ChangeScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
}
