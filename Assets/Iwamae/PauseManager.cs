using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ポーズ機能
/// </summary>
public class PauseManager : MonoBehaviour
{
	[SerializeField]
	//　ポーズした時に表示するUIのプレハブ
	GameObject pauseUIPrefab;
	//　ポーズUIのインスタンス
	GameObject pauseUIInstance;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown("q"))
		{
			if (pauseUIInstance == null)
			{
				pauseUIInstance = GameObject.Instantiate(pauseUIPrefab) as GameObject;
				Time.timeScale = 0f;
			}
			else
			{
				Destroy(pauseUIInstance);
				Time.timeScale = 1f;
			}
		}
	}
}
