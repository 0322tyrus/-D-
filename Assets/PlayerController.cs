using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public enum DIRECTION_TYPE　//列挙
    {
        STOP,
        RIGHT,
        LEFT,
    }

    //最初の設定
    DIRECTION_TYPE direction = DIRECTION_TYPE.STOP;
    [SerializeField] LayerMask blockLayer; //外から設定できるようにするもの

    float speed;
    Animator animator;

    Rigidbody2D rigidbody2D;
    float jumpPower= 250;
    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal"); //方向キーの取得
        animator.SetFloat("speed", Mathf.Abs(x));//入力時に-3にならないようMathf.Absで絶対値をとる


        if (x == 0)
        {
            //止まっている
            direction = DIRECTION_TYPE.STOP;
        }
        else if (x > 0)
        {
            //右へ
            direction = DIRECTION_TYPE.RIGHT;
        }
        else if (x < 0)
        {
            //左へ
            direction = DIRECTION_TYPE.LEFT;
        }
        //jumpさせる

        if (Ground())
        {
            if (Input.GetKeyDown("space"))
            {

                Jump();

            }
            else
            {
                animator.SetBool("IsJumping", false);
            }
        }

        if (isDead)
        {
            return;
        }

        switch (direction)
        {
            case DIRECTION_TYPE.STOP:
                speed = 0;
                break;

            case DIRECTION_TYPE.RIGHT:
                speed = 3;
                transform.localScale = new Vector3(1, 1, 1);
                break;

            case DIRECTION_TYPE.LEFT:
                speed = -3;
                transform.localScale = new Vector3(-1, 1, 1);
                break;

        }
        rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
    }

    bool Ground()
    {
        //始点と終点を作成
        Vector3 leftStartPoint = transform.position - transform.right * 0.3f;
        Vector3 rightStartPoint = transform.position + transform.right * 0.3f;
        Vector3 endPoint = transform.position - transform.up * 0.1f;

        //確認用　Debug.DrawLine(leftStartPoint, endPoint);
        //確認用　Debug.DrawLine(rightStartPoint, endPoint);//始点と終点を反映

        return Physics2D.Linecast(leftStartPoint, endPoint, blockLayer)
            || Physics2D.Linecast(rightStartPoint, endPoint, blockLayer);
    }
    void Jump()
    {
        //上方向に力を加える
        rigidbody2D.AddForce(Vector2.up * jumpPower);
        //audioSource.PlayOneShot(jumpSE);
        animator.SetBool("IsJumping", true);

    }



    /*
    UIでタイマーとスコアとHPの設定
    タイマーは０３：００
    スコア：　pt
    HP　100/100　

    敵キャラの行動
    歩く、一定の範囲で動きにランダム性を持たせたい
    見つける（視界）範囲の設定

    プレイヤー
    しゃがむアニメーションの設定（バーチカル）

    マップ
    タップでスタミナを10減らして障害物を置く

     */
}
