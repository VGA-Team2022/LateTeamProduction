using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン遷移
/// </summary>
public class SceneChanger : MonoBehaviour
{
    [Tooltip("遷移先のシーン名")]
    [SerializeField] string _sceneName = "test2";
    [Tooltip("フェードキャンバスのプレハブ")]
    [SerializeField] GameObject _fadeImage;
    [Tooltip("シーン遷移するまで待つ時間")]
    [SerializeField ]float _waitTime = 0.8f;

    public void ChangeScene()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        Instantiate(_fadeImage);
        yield return new WaitForSeconds(_waitTime);
        SceneManager.LoadScene(_sceneName);
    }
}
