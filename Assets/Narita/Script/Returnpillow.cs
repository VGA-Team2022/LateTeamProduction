using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Returnpillow : MonoBehaviour
{
    /// <summary>枕返しを行ったかどうか</summary>
    bool returnPillow;
    Animator anim = null;
    Image image = null;
    [SerializeField]
    Slider slider = null;
    /// <summary>ランダムな数値</summary>
    float getupTime;
    /// <summary>時計</summary>
    float timer;
    public bool ReturnPillow { get => returnPillow; set => returnPillow = value; }
    // Start is called before the first frame update
    void Start()
    {
        if (!slider)
        {
            Debug.Log("sliderをセットしてください");
        }
        returnPillow = false;
        anim = GetComponent<Animator>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (returnPillow)
        {
            //↓当たり判定は後で変更予定
            GetComponent<BoxCollider2D>().enabled = false;
            //↓変える色は後で変更予定
            image.color = Color.black;
        }
    }
    private void LateUpdate()
    {
        anim.SetBool("boolの名前", returnPillow);
    }

    private void OnTriggerStay2D(Collider2D collision)//プレイヤーが当たり判定の中にとどまったら
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().Pillow = true;
            timer += Time.deltaTime;
            //数値の幅は後で変更予定
            getupTime = Random.Range(2, 5);
            if (getupTime <= timer && returnPillow)//制限時間を超えた　＋　枕を返されていなかったら
            {

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        timer -= timer;
    }
}
