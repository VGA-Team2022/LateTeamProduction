using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRun : MonoBehaviour
{
    [SerializeField] SoundManager soundManager = null;
    // Start is called before the first frame update
    void Start()
    {
        soundManager.GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
