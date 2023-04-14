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
        // 바닥까지의 거리
        public float groundDistance = 0.52f;
        
        
        // 점프가해지는 힘
        public float jumpForce = 4.0f;
        [SerializeField]
        bool isGround = false;
        [SerializeField]
        bool isJump = true;
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

            CheckGround();
            if (isGround)
            {
                
                Jump();
                
                //if (this.transform.position.y < height)
                //{
                //    
                //}
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

        

        void Flip()
        {
            // 첫번째 방법 flip을 사용
            //if (moveInput > 0)
            //{
            //    this.GetComponent<SpriteRenderer>().flipX = false;
            //}
            //else if (moveInput < 0)
            //{
            //    this.GetComponent<SpriteRenderer>().flipX = true;
            //}
            // 두번째 방법 사이즈를 교체
            if (moveInput > 0)
            {
                transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
            }
            if (moveInput < 0)
            {
                transform.localScale = new Vector3((-1) * scaleX, transform.localScale.y, transform.localScale.z);
            }
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
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
                animator.SetBool("isJump", true);
                isJump = true;
                isGround = false;
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

        void CheckGround()
        {
            if (isJump && rigid.velocity.y < 0)
            {
                //RaycastHit2D raycastHit = Physics2D.Raycast(rigid.position, Vector3.down, groundDistance, LayerMask.GetMask("Ground"));
                //if(raycastHit.collider != null)
                //{
                //    //Debug.Log(raycastHit.collider.name);
                //    isGround = true;
                //    isJump = false;
                //}

                RaycastHit2D raycastHit2D = Physics2D.BoxCast(rigid.position, Vector2.one, 0, Vector2.down, groundDistance, LayerMask.GetMask("Ground"));
                if (raycastHit2D.collider != null)
                {
                    isGround = true;
                    isJump = false;
                }

                //isGround = Physics2D.OverlapCircle(TrGround.position, TrGround.GetComponent<CircleCollider2D>().radius, GroundLayer);
                //if (isGround)
                //{
                //    isJump = false;
                //    isGround = true;
                //}
            }
            
        }

        private void OnDrawGizmos()
        {
            if (rigid == null)
            {
                return;
            }

            Gizmos.color = Color.green;

            Gizmos.DrawLine(rigid.position, (rigid.position - new Vector2(0, groundDistance)));

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(rigid.position,  new Vector2(1, 0.5f + groundDistance));
        }

        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    if (collision.gameObject.CompareTag("Item"))
        //    {
        //        collision.gameObject.GetComponent<Item>().Open();
        //    }
        //}
        //private void OnC
        //{
        //    
        //}
    }
}
