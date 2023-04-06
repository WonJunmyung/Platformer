using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silly
{
    public class Player : MonoBehaviour
    {
        // �̼��ϰ� �ӵ��� �ö� ����
        public float maxSpeed = 2.0f;
        // ���� �ӵ� ����
        public float moveSpeed = 2.0f;
        
        
        // ������������ ��
        public float jumpForce = 4.0f;
        bool isGround = false;
        public Transform GroundCheck;
        public LayerMask GroundLayer;

        Rigidbody2D rigid;

        float moveInput;
        float scaleX;

        Animator animator;

        // Start is called before the first frame update
        void Start()
        {
            rigid = GetComponent<Rigidbody2D>();
            rigid.freezeRotation = true;
            scaleX = transform.localScale.x;
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            // �ΰ����� ���� �ε巯�� �������� �����ϰ� ������
            //moveInput = Input.GetAxis("Horizontal");
            // ���� �Է��� ��� �ݿ��ϰ� ������
            moveInput = Input.GetAxisRaw("Horizontal");
            //Jump();

            if (IsGround())
            {
                Jump();
            }
            else
            {
                animator.SetBool("isJump", false);
            }
        }

        private void FixedUpdate()
        {
            move();
            CheckMove();
        }

        public void move()
        {
            Flip();
            // ù��° ���
            rigid.velocity = new Vector2(moveInput * moveSpeed, rigid.velocity.y);

            // �ι�° ���
            // Force : ������ ����ؼ� �������� ���� ���ϴ� ���
            // Impulse : ������ ����ؼ� �������� ���� ���ϴ� ���
            //rigid.AddForce(Vector2.right * moveInput, ForceMode2D.Impulse);

            //if (rigid.velocity.x > maxSpeed)
            //{
            //    rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
            //}
            //else if (rigid.velocity.x < -maxSpeed)
            //{
            //    rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);
            //}
        }

        public void move2()
        {
            
            
        }

        

        void Flip()
        {
            // ù��° ��� flip�� ���
            if (moveInput > 0)
            {
                this.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (moveInput < 0)
            {
                this.GetComponent<SpriteRenderer>().flipX = true;
            }
            // �ι�° ��� ����� ��ü
            //if (moveInput > 0)
            //{
            //    transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
            //}
            //if (moveInput < 0)
            //{
            //    transform.localScale = new Vector3((-1) * scaleX, transform.localScale.y, transform.localScale.z);
            //}
        }

        void CheckMove()
        {
            // ù��° ��� normalized ���� �̿���
            if (rigid.velocity.normalized.x == 0f)
            {
                animator.SetBool("isWalk", false);
            }
            else
            {
                animator.SetBool("isWalk", true);
            }
            // �ι�° ��� ���밪�� �̿���
            //if (Mathf.Abs(rigid.velocity.x) < 0.2f)
            //{
            //    animator.SetBool("isWalk", false);
            //}
            //else
            //{
            //    animator.SetBool("isWalk", true);
            //}
        }

        void Jump()
        {
            // ù��° ���
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //if (IsGround())
                {
                    rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
                    animator.SetBool("isJump", true);
                }

            }
            // �ι�° ���
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    if (IsGround())
            //    {
            //        rigid.AddForce(new Vector2(rigid.velocity.x, jumpForce), ForceMode2D.Impulse);
            //    }
            //}
        }

        bool IsGround()
        {
            isGround = Physics2D.OverlapCircle(GroundCheck.position, GroundCheck.GetComponent<CircleCollider2D>().radius, GroundLayer);
            return isGround;
        }
    }
}
