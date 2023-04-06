using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silly
{
    public class Player : MonoBehaviour
    {
        // 미세하게 속도가 올라감 구현
        public float maxSpeed = 2.0f;
        // 일정 속도 유지
        public float moveSpeed = 2.0f;
        
        
        // 점프가해지는 힘
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
            // 민감도에 따른 부드러운 움직임을 적용하고 싶을때
            //moveInput = Input.GetAxis("Horizontal");
            // 실제 입력을 즉시 반영하고 싶을때
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
            // 첫번째 방법
            rigid.velocity = new Vector2(moveInput * moveSpeed, rigid.velocity.y);

            // 두번째 방법
            // Force : 질량을 사용해서 연속적인 힘을 가하는 경우
            // Impulse : 질량을 사용해서 순간적인 힘을 가하는 경우
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
            // 첫번째 방법 flip을 사용
            if (moveInput > 0)
            {
                this.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (moveInput < 0)
            {
                this.GetComponent<SpriteRenderer>().flipX = true;
            }
            // 두번째 방법 사이즈를 교체
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
            // 첫번째 방법 normalized 값을 이용함
            if (rigid.velocity.normalized.x == 0f)
            {
                animator.SetBool("isWalk", false);
            }
            else
            {
                animator.SetBool("isWalk", true);
            }
            // 두번째 방법 절대값을 이용함
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
            // 첫번째 방법
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //if (IsGround())
                {
                    rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
                    animator.SetBool("isJump", true);
                }

            }
            // 두번째 방법
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
