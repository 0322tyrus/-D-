using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public enum DIRECTION_TYPE�@//��
    {
        STOP,
        RIGHT,
        LEFT,
    }

    //�ŏ��̐ݒ�
    DIRECTION_TYPE direction = DIRECTION_TYPE.STOP;
    [SerializeField] LayerMask blockLayer; //�O����ݒ�ł���悤�ɂ������

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
        float x = Input.GetAxis("Horizontal"); //�����L�[�̎擾
        animator.SetFloat("speed", Mathf.Abs(x));//���͎���-3�ɂȂ�Ȃ��悤Mathf.Abs�Ő�Βl���Ƃ�


        if (x == 0)
        {
            //�~�܂��Ă���
            direction = DIRECTION_TYPE.STOP;
        }
        else if (x > 0)
        {
            //�E��
            direction = DIRECTION_TYPE.RIGHT;
        }
        else if (x < 0)
        {
            //����
            direction = DIRECTION_TYPE.LEFT;
        }
        //jump������

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
        //�n�_�ƏI�_���쐬
        Vector3 leftStartPoint = transform.position - transform.right * 0.3f;
        Vector3 rightStartPoint = transform.position + transform.right * 0.3f;
        Vector3 endPoint = transform.position - transform.up * 0.1f;

        //�m�F�p�@Debug.DrawLine(leftStartPoint, endPoint);
        //�m�F�p�@Debug.DrawLine(rightStartPoint, endPoint);//�n�_�ƏI�_�𔽉f

        return Physics2D.Linecast(leftStartPoint, endPoint, blockLayer)
            || Physics2D.Linecast(rightStartPoint, endPoint, blockLayer);
    }
    void Jump()
    {
        //������ɗ͂�������
        rigidbody2D.AddForce(Vector2.up * jumpPower);
        //audioSource.PlayOneShot(jumpSE);
        animator.SetBool("IsJumping", true);

    }



    /*
    UI�Ń^�C�}�[�ƃX�R�A��HP�̐ݒ�
    �^�C�}�[�͂O�R�F�O�O
    �X�R�A�F�@pt
    HP�@100/100�@

    �G�L�����̍s��
    �����A���͈̔͂œ����Ƀ����_����������������
    ������i���E�j�͈͂̐ݒ�

    �v���C���[
    ���Ⴊ�ރA�j���[�V�����̐ݒ�i�o�[�`�J���j

    �}�b�v
    �^�b�v�ŃX�^�~�i��10���炵�ď�Q����u��

     */
}
