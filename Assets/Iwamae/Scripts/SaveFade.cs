using UnityEngine;

/// <summary>
/// フェードするオブジェクトをシーンをまたいでも保持しておく
/// </summary>
public class SaveFade : MonoBehaviour
{
    public static bool _Instance = false;

    void Start()
    {
        if (!_Instance)
        {
            DontDestroyOnLoad(this);
            _Instance = true;
        }
        else
        {
            Destroy(this);
        }
    }
}
