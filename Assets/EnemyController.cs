using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
	//状態の番号
	private int StateNumber = 0;

	//経過時間(秒)
	private float TimeCounter = 0.0f;

	//アニメーターコンポーネント
	private Animator myAnimator;

	//スプライトコンポーネント
	private SpriteRenderer mySpriteRenderer;

	//Rigidbodyコンポーネント
	private Rigidbody2D myRigidbody2D;

	//センサー
	public GameObject GroundSensor;
	public GameObject RightSensor;
	public GameObject LeftSensor;
	public GameObject RightTopSensor;
	public GameObject LeftTopSensor;
	public GameObject RightBottomSensor;
	public GameObject LeftBottomSensor;

	//移動量
	private float velocity = 2f;
	//大ジャンプ用移動量
	private float highjumpvelocity = 5f;

	//ジャンプ量
	private float jumppower = 5f;
	//大ジャンプ量
	private float highjumppower = 7f;

    void Start()
    {
		//アニメータコンポーネントを取得
		this.myAnimator = GetComponent<Animator>();

		//SpriteRendererコンポーネントを取得
		this.mySpriteRenderer = GetComponent<SpriteRenderer>();

		//Rigidbody2Dコンポーネントを取得
		this.myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
 		//ステートマシン ※ステートとは[状態]のこと
		switch( StateNumber) {
			//アイドリング
			case  0 :
				{   //タイマー
					if (TimeCounter > 0.5f)
					{
						//顔の向き（true = 右, false = 左）
						if (this.mySpriteRenderer.flipX)
						{
							//壁？
							if (RightSensor.GetComponent<Sensor_Bandit>().State())
							{

								if (RightSensor.GetComponent<Sensor_Bandit>().State() != RightTopSensor.GetComponent<Sensor_Bandit>().State())
								{
									//アニメーション遷移（ジャンプ）
									myAnimator.SetInteger("AnimState", 3);

									StateNumber = 2;
								}
								else
								{

									//左へ反転
									this.mySpriteRenderer.flipX = false;

									//クリアー
									TimeCounter = 0f;
								}
							}
							else
							{
								//アニメーション遷移（走る）
								myAnimator.SetInteger("AnimState", 2);

								//状態の遷移（走る）
								StateNumber = 1;
							}
						}
						else
						{
							//壁？
							if (LeftSensor.GetComponent<Sensor_Bandit>().State())
							{

								if (LeftSensor.GetComponent<Sensor_Bandit>().State() != LeftTopSensor.GetComponent<Sensor_Bandit>().State())
								{
									//アニメーション遷移（ジャンプ）
									myAnimator.SetInteger("AnimState", 3);

									StateNumber = 2;
								}
								else
								{
									//右へ反転
									this.mySpriteRenderer.flipX = true;

									//クリアー
									TimeCounter = 0f;
								}
							}
							else
							{
								//アニメーション遷移（走る）
								myAnimator.SetInteger("AnimState", 2);

								//状態の遷移（走る）
								StateNumber = 1;
							}
						}

					}
				}	break;
			    
			//走る
			case  1 :	{	//顔の向き（true = 右, false = 左）
							if( this.mySpriteRenderer.flipX) {
								//移動
								myRigidbody2D.velocity = new Vector2( velocity, myRigidbody2D.velocity.y);

								//壁？
								if( RightSensor.GetComponent<Sensor_Bandit>().State()) {
									//停止
									myRigidbody2D.velocity = new Vector2( 0f, myRigidbody2D.velocity.y);

									//アニメーション遷移（停止）
									myAnimator.SetInteger( "AnimState", 0);	
							
									//クリアー
									TimeCounter = 0f;
							
							         //状態の遷移（アイドリング）
							         StateNumber = 0;
								}
								else if(LeftTopSensor.GetComponent<Sensor_Bandit>().State()!= (RightTopSensor.GetComponent<Sensor_Bandit>().State() && RightSensor.GetComponent<Sensor_Bandit>().State() && RightBottomSensor.GetComponent<Sensor_Bandit>().State()))
                                {
							        //アニメーション遷移（ジャンプ）
							       myAnimator.SetInteger("AnimState", 3);
							        //クリアー
							       TimeCounter = 0f;
							       StateNumber = 3;
		        				}
							} else {
								//移動
								myRigidbody2D.velocity = new Vector2( -velocity, myRigidbody2D.velocity.y);

								//壁？
								if( LeftSensor.GetComponent<Sensor_Bandit>().State()) {
									//停止
									myRigidbody2D.velocity = new Vector2( 0f, myRigidbody2D.velocity.y);

									//アニメーション遷移（停止）
									myAnimator.SetInteger( "AnimState", 0);										
							
									//クリアー
									TimeCounter = 0f;

									//状態の遷移（アイドリング）
									StateNumber = 0;
								}
						        else if (RightTopSensor.GetComponent<Sensor_Bandit>().State() != (LeftTopSensor.GetComponent<Sensor_Bandit>().State() && LeftSensor.GetComponent<Sensor_Bandit>().State() && LeftBottomSensor.GetComponent<Sensor_Bandit>().State()))
						        {
							        //アニメーション遷移（ジャンプ）
							        myAnimator.SetInteger("AnimState", 3);
							        //クリアー
						        	TimeCounter = 0f;
						         	StateNumber = 3;
						        }
					        }
			}	break;

			　　//ジャンプ(case0からセンサー上・中央が接触時にStateNumberを２に変更して呼び出す、case２でジャンプになるようにする、が正解な気がする）
			
				//大ジャンプ（case１で、RightSensor、RightTopSensor、RightBottomSensorの３っが衝突していない状態＋LeftBottomSensorが衝突している時にcase３を呼び出し大ジャンプをする）
			case 2:
				{
					if (this.mySpriteRenderer.flipX)
					{
						
						//ジャンプ
						myRigidbody2D.velocity = new Vector2(velocity, jumppower);

						//アニメーション遷移（ジャンプ）
						myAnimator.SetInteger("AnimState", 3);

						//クリアー
						TimeCounter = 0f;

						//状態の遷移（アイドリング）
						StateNumber = 0;

						Debug.Log("ジャンプ");
						
					}
					else
					{
					

						//ジャンプ
						myRigidbody2D.velocity = new Vector2(-velocity, jumppower);

						//アニメーション遷移（ジャンプ）
						myAnimator.SetInteger("AnimState", 3);

						//クリアー
						TimeCounter = 0f;

						//状態の遷移（アイドリング）
						StateNumber = 0;

						Debug.Log("ジャンプ");
						
					}
				}break;

				//大ジャンプ
			case 3:
				{
					if (this.mySpriteRenderer.flipX)
					{
						
						//大ジャンプ
						myRigidbody2D.velocity = new Vector2(highjumpvelocity, highjumppower);

						//アニメーション遷移（ジャンプ）
						myAnimator.SetInteger("AnimState", 3);

						//クリアー
						TimeCounter = 0f;

						//状態の遷移（アイドリング）
						StateNumber = 0;

						Debug.Log("ジャンプ");
						//}
					}
					else
					{

						//ジャンプ
						myRigidbody2D.velocity = new Vector2(-highjumpvelocity, highjumppower);

						//アニメーション遷移（ジャンプ）
						myAnimator.SetInteger("AnimState", 3);

						//クリアー
						TimeCounter = 0f;

						//状態の遷移（アイドリング）
						StateNumber = 0;

						Debug.Log("ジャンプ");
					
					}
				}
				break;

			default: break;
		}
		

		//タイマー
		TimeCounter += Time.deltaTime;
	}

    
}