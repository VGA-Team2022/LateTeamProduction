using UnityEngine;

/// <summary>
/// ポーズ機能のテスト用
/// </summary>
public class Move : MonoBehaviour
{
    PauseManager _pauseManager = default;
    bool isStop = false;

    void Awake()
    {
        _pauseManager = GameObject.FindObjectOfType<PauseManager>();
    }

    void OnEnable()
    {
        _pauseManager.OnPauseResume += PauseResume;
    }

    void OnDisable()
    {
        _pauseManager.OnPauseResume -= PauseResume;
    }

    void PauseResume(bool isPause)
    {
        if (isPause)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    public void Pause()
    {
        isStop = true;
    }

    public void Resume()
    {
        isStop = false;
    }

    void Update()
    {
        if (isStop)
        {
            return;
        }
        transform.position = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow)) //上キー
        {
            transform.position = Vector2.up * 5;
        }
        if (Input.GetKey(KeyCode.DownArrow)) //下キー
        {
            transform.position = Vector2.down * 5;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) //左キー
        {
            transform.position = Vector2.left * 5;
        }
        if (Input.GetKey(KeyCode.RightArrow)) //右キー
        {
            transform.position = Vector2.right * 5;
        }
    }
}