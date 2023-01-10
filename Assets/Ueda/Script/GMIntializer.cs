using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsGame;

public class GMIntializer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance == null)
        {
            GameManager.Instance = new GameManager();
        }
        GameManager.Instance.GameStart();
        Destroy(gameObject);
    }
}
