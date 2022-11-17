using UnityEngine;

/// <summary>
/// ポーズ機能の管理
/// </summary>
public class PauseManager : MonoBehaviour
{
    bool _isPause = false;
    public delegate void Pause(bool isPause);
    Pause _onPauseResume = default;

    public Pause OnPauseResume
    {
        get { return _onPauseResume; }
        set { _onPauseResume = value; }
    }

    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            PauseResume();
        }
    }

    void PauseResume()
    {
        _isPause = !_isPause;
        _onPauseResume(_isPause);
    }
}
