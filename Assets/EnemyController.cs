using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
	//��Ԃ̔ԍ�
	private int StateNumber = 0;

	//�o�ߎ���(�b)
	private float TimeCounter = 0.0f;

	//�A�j���[�^�[�R���|�[�l���g
	private Animator myAnimator;

	//�X�v���C�g�R���|�[�l���g
	private SpriteRenderer mySpriteRenderer;

	//Rigidbody�R���|�[�l���g
	private Rigidbody2D myRigidbody2D;

	//�Z���T�[
	public GameObject GroundSensor;
	public GameObject RightSensor;
	public GameObject LeftSensor;
	public GameObject RightTopSensor;
	public GameObject LeftTopSensor;
	public GameObject RightBottomSensor;
	public GameObject LeftBottomSensor;

	//�ړ���
	private float velocity = 2f;
	//��W�����v�p�ړ���
	private float highjumpvelocity = 5f;

	//�W�����v��
	private float jumppower = 5f;
	//��W�����v��
	private float highjumppower = 7f;

    void Start()
    {
		//�A�j���[�^�R���|�[�l���g���擾
		this.myAnimator = GetComponent<Animator>();

		//SpriteRenderer�R���|�[�l���g���擾
		this.mySpriteRenderer = GetComponent<SpriteRenderer>();

		//Rigidbody2D�R���|�[�l���g���擾
		this.myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
 		//�X�e�[�g�}�V�� ���X�e�[�g�Ƃ�[���]�̂���
		switch( StateNumber) {
			//�A�C�h�����O
			case  0 :
				{   //�^�C�}�[
					if (TimeCounter > 0.5f)
					{
						//��̌����itrue = �E, false = ���j
						if (this.mySpriteRenderer.flipX)
						{
							//�ǁH
							if (RightSensor.GetComponent<Sensor_Bandit>().State())
							{

								if (RightSensor.GetComponent<Sensor_Bandit>().State() != RightTopSensor.GetComponent<Sensor_Bandit>().State())
								{
									//�A�j���[�V�����J�ځi�W�����v�j
									myAnimator.SetInteger("AnimState", 3);

									StateNumber = 2;
								}
								else
								{

									//���֔��]
									this.mySpriteRenderer.flipX = false;

									//�N���A�[
									TimeCounter = 0f;
								}
							}
							else
							{
								//�A�j���[�V�����J�ځi����j
								myAnimator.SetInteger("AnimState", 2);

								//��Ԃ̑J�ځi����j
								StateNumber = 1;
							}
						}
						else
						{
							//�ǁH
							if (LeftSensor.GetComponent<Sensor_Bandit>().State())
							{

								if (LeftSensor.GetComponent<Sensor_Bandit>().State() != LeftTopSensor.GetComponent<Sensor_Bandit>().State())
								{
									//�A�j���[�V�����J�ځi�W�����v�j
									myAnimator.SetInteger("AnimState", 3);

									StateNumber = 2;
								}
								else
								{
									//�E�֔��]
									this.mySpriteRenderer.flipX = true;

									//�N���A�[
									TimeCounter = 0f;
								}
							}
							else
							{
								//�A�j���[�V�����J�ځi����j
								myAnimator.SetInteger("AnimState", 2);

								//��Ԃ̑J�ځi����j
								StateNumber = 1;
							}
						}

					}
				}	break;
			    
			//����
			case  1 :	{	//��̌����itrue = �E, false = ���j
							if( this.mySpriteRenderer.flipX) {
								//�ړ�
								myRigidbody2D.velocity = new Vector2( velocity, myRigidbody2D.velocity.y);

								//�ǁH
								if( RightSensor.GetComponent<Sensor_Bandit>().State()) {
									//��~
									myRigidbody2D.velocity = new Vector2( 0f, myRigidbody2D.velocity.y);

									//�A�j���[�V�����J�ځi��~�j
									myAnimator.SetInteger( "AnimState", 0);	
							
									//�N���A�[
									TimeCounter = 0f;
							
							         //��Ԃ̑J�ځi�A�C�h�����O�j
							         StateNumber = 0;
								}
								else if(LeftTopSensor.GetComponent<Sensor_Bandit>().State()!= (RightTopSensor.GetComponent<Sensor_Bandit>().State() && RightSensor.GetComponent<Sensor_Bandit>().State() && RightBottomSensor.GetComponent<Sensor_Bandit>().State()))
                                {
							        //�A�j���[�V�����J�ځi�W�����v�j
							       myAnimator.SetInteger("AnimState", 3);
							        //�N���A�[
							       TimeCounter = 0f;
							       StateNumber = 3;
		        				}
							} else {
								//�ړ�
								myRigidbody2D.velocity = new Vector2( -velocity, myRigidbody2D.velocity.y);

								//�ǁH
								if( LeftSensor.GetComponent<Sensor_Bandit>().State()) {
									//��~
									myRigidbody2D.velocity = new Vector2( 0f, myRigidbody2D.velocity.y);

									//�A�j���[�V�����J�ځi��~�j
									myAnimator.SetInteger( "AnimState", 0);										
							
									//�N���A�[
									TimeCounter = 0f;

									//��Ԃ̑J�ځi�A�C�h�����O�j
									StateNumber = 0;
								}
						        else if (RightTopSensor.GetComponent<Sensor_Bandit>().State() != (LeftTopSensor.GetComponent<Sensor_Bandit>().State() && LeftSensor.GetComponent<Sensor_Bandit>().State() && LeftBottomSensor.GetComponent<Sensor_Bandit>().State()))
						        {
							        //�A�j���[�V�����J�ځi�W�����v�j
							        myAnimator.SetInteger("AnimState", 3);
							        //�N���A�[
						        	TimeCounter = 0f;
						         	StateNumber = 3;
						        }
					        }
			}	break;

			�@�@//�W�����v(case0����Z���T�[��E�������ڐG����StateNumber���Q�ɕύX���ČĂяo���Acase�Q�ŃW�����v�ɂȂ�悤�ɂ���A�������ȋC������j
			
				//��W�����v�icase�P�ŁARightSensor�ARightTopSensor�ARightBottomSensor�̂R�����Փ˂��Ă��Ȃ���ԁ{LeftBottomSensor���Փ˂��Ă��鎞��case�R���Ăяo����W�����v������j
			case 2:
				{
					if (this.mySpriteRenderer.flipX)
					{
						
						//�W�����v
						myRigidbody2D.velocity = new Vector2(velocity, jumppower);

						//�A�j���[�V�����J�ځi�W�����v�j
						myAnimator.SetInteger("AnimState", 3);

						//�N���A�[
						TimeCounter = 0f;

						//��Ԃ̑J�ځi�A�C�h�����O�j
						StateNumber = 0;

						Debug.Log("�W�����v");
						
					}
					else
					{
					

						//�W�����v
						myRigidbody2D.velocity = new Vector2(-velocity, jumppower);

						//�A�j���[�V�����J�ځi�W�����v�j
						myAnimator.SetInteger("AnimState", 3);

						//�N���A�[
						TimeCounter = 0f;

						//��Ԃ̑J�ځi�A�C�h�����O�j
						StateNumber = 0;

						Debug.Log("�W�����v");
						
					}
				}break;

				//��W�����v
			case 3:
				{
					if (this.mySpriteRenderer.flipX)
					{
						
						//��W�����v
						myRigidbody2D.velocity = new Vector2(highjumpvelocity, highjumppower);

						//�A�j���[�V�����J�ځi�W�����v�j
						myAnimator.SetInteger("AnimState", 3);

						//�N���A�[
						TimeCounter = 0f;

						//��Ԃ̑J�ځi�A�C�h�����O�j
						StateNumber = 0;

						Debug.Log("�W�����v");
						//}
					}
					else
					{

						//�W�����v
						myRigidbody2D.velocity = new Vector2(-highjumpvelocity, highjumppower);

						//�A�j���[�V�����J�ځi�W�����v�j
						myAnimator.SetInteger("AnimState", 3);

						//�N���A�[
						TimeCounter = 0f;

						//��Ԃ̑J�ځi�A�C�h�����O�j
						StateNumber = 0;

						Debug.Log("�W�����v");
					
					}
				}
				break;

			default: break;
		}
		

		//�^�C�}�[
		TimeCounter += Time.deltaTime;
	}

    
}