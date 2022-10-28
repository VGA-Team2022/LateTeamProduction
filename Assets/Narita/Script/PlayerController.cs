using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    /// <summary>動く速度（子供）</summary>
    [SerializeField,Header("子供状態の動くスピード")]
    float childMoveSpeed = 10f;
    /// <summary>動く速度（大人）</summary>
    [SerializeField, Header("大人状態の動くスピード")]
    float adultMoveSpeed = 15f;
    /// <summary>時計</summary>
    float timer = 0f;
    /// <summary>枕を返すまでにかかる時間</summary>//atai
    [SerializeField,Header("枕を返すまでにかかる時間")]
    float timerlimit = 0.5f;
    Vector2 moveVelocity;
    /// <summary>止まる直前の速度方向</summary>
    Vector2 lastMoveVelocity;
    /// <summary>レベル</summary>
    int level = 1;
    public VariableJoystick joyStick;
    Animator anim;
    Rigidbody2D rb;
    /// <summary>枕を返す標的を保持する</summary>
    Returnpillow pillowEnemy;
    UIManager ui;
    /// <summary>枕返し圏内</summary>
    bool pillow = false;
    /// <summary>大人か子供か</summary>
    [SerializeField,Header("プレイヤーが大人か子供か")]
    bool adultState = false;

    /// <summary>枕返し圏内</summary>
    public bool Pillow { get => pillow; set => pillow = value; }
    public int Level { get => level; set => level = value; }
    public float Timerlimit { get => timerlimit; set => timerlimit = value; }
    public Returnpillow PillowEnemy { get => pillowEnemy; set => pillowEnemy = value; }

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ui = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");
        float h = joyStick.Horizontal;
        float v = joyStick.Vertical;

        if (!adultState)
        {
            ChildMode(h, v);
        }
        else
        {
            AdultMode(h, v);
        }

        rb.velocity = moveVelocity;
        lastMoveVelocity = moveVelocity;

        if (Input.GetButton("Jump"))//右長押し
        {
            if (pillowEnemy)//枕返し圏内にいたら
            {
                timer += Time.deltaTime;
                ui.ChargeSlider(timer);
            }
        }
        else if(Input.GetButtonUp("Jump"))
        {
            timer = 0;
            ui.ChargeSlider(timer);
        }
    }

    void LateUpdate()
    {
        if (anim)
        {
            {//動いている間
                anim.SetFloat("floatの名前", moveVelocity.x);
                anim.SetFloat("floatの名前", moveVelocity.y);
                //上の場合、＋Y
                //下の場合、ーY
                //右上、右下、右の場合、＋X
                //左上、左下、左の場合、ーX
            }
            {//止まっている間
                anim.SetFloat("", lastMoveVelocity.x);
                anim.SetFloat("", lastMoveVelocity.y);
                //上の条件に加えて、プレイヤーが動いていないこと。
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//寝ている敵の情報を取る
    {
        if (collision.gameObject.CompareTag("ReturnPillow"))
        {
            pillowEnemy = collision.GetComponent<Returnpillow>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        pillowEnemy = null;
        timer = 0;
        ui.ChargeSlider(timer);
    }

    private void ChildMode(float h, float v)
    {
        moveVelocity = new Vector2(h, v).normalized * childMoveSpeed;
    }

    private void AdultMode(float h, float v)
    {
        moveVelocity = new Vector2(h, v).normalized * adultMoveSpeed;
    }
}
