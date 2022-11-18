using UnityEngine;

/// <summary>
/// ポーズ時にポーズしているかを確認するUIの表示
/// </summary>
public class PauseUI : MonoBehaviour
{
    PauseManager _pm = default;

    void Awake()
    {
        _pm = GameObject.FindObjectOfType<PauseManager>();
    }

    void OnEnable()
    {
        _pm.OnPauseResume += ShowMessage;
    }

    void OnDisable()
    {
        _pm.OnPauseResume -= ShowMessage;
    }

    //ポーズボタンが押されたらポーズ、
    //もう一度押したら再開のログを表示する
    void ShowMessage(bool isPause)
    {
        if (isPause)
        {
            Debug.Log("ポーズ中");
        }
        else
        {
            Debug.Log("再開");
        }
    }
}
