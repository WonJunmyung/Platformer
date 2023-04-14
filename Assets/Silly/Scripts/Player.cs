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
        // �ٴڱ����� �Ÿ�
        public float groundDistance = 0.52f;
        
        
        // ������������ ��
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
            // �ΰ����� ���� �ε巯�� �������� �����ϰ� ������
            //moveInput = Input.GetAxis("Horizontal");
            // ���� �Է��� ��� �ݿ��ϰ� ������
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

        

        void Flip()
        {
            // ù��° ��� flip�� ���
            //if (moveInput > 0)
            //{
            //    this.GetComponent<SpriteRenderer>().flipX = false;
            //}
            //else if (moveInput < 0)
            //{
            //    this.GetComponent<SpriteRenderer>().flipX = true;
            //}
            // �ι�° ��� ����� ��ü
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
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
                animator.SetBool("isJump", true);
                isJump = true;
                isGround = false;
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
