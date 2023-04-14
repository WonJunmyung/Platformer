using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silly
{
    public class Enemy : MonoBehaviour
    {
        Rigidbody2D rigid;
        Animator animator;
        SpriteRenderer spriteRenderer;
        public float initDelay = 1.0f;
        public float delay = 2.0f;
        int dir = 0;
        public float detectPlayerDistance = 2.0f;
        public LayerMask GroundLayer;
        public LayerMask PlayerLayer;
        [SerializeField]
        GameObject player = null;

        bool detectDir = false;
        public float attackDistance = 1.0f;



        // Start is called before the first frame update
        void Start()
        {
            rigid = this.GetComponent<Rigidbody2D>();
            animator = this.GetComponent<Animator>();
            spriteRenderer = this.GetComponent<SpriteRenderer>();
            InvokeRepeating("AutoDir", initDelay, delay);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            AutoMove();
            CheckPlayer();
        }



        void AutoDir()
        {
            if (player == null)
            {
                dir = Random.Range(-1, 2);
            }
            else
            {
                float distance = player.transform.position.x - this.transform.position.x;
                if (distance < 0)
                {
                    dir = -1;
                }
                else if (distance > 0)
                {
                    dir = 1;
                }
                if (Mathf.Abs(distance) < attackDistance)
                {
                    dir = 0;
                }
            }


        }

        void AutoMove()
        {
            if (player == null)
            {
                rigid.MovePosition(rigid.position + (new Vector2(dir, 0) * Time.deltaTime));

                Vector2 checkGround = new Vector2(rigid.position.x + dir, rigid.position.y);


                RaycastHit2D raycastHit2D = Physics2D.Raycast(checkGround, Vector2.down, 1, GroundLayer);

                if (raycastHit2D.collider == null)
                {
                    dir *= -1;
                }

                if (dir == -1)
                {
                    spriteRenderer.flipX = false;
                }
                else if (dir == 1)
                {
                    spriteRenderer.flipX = true;
                }
                animator.SetInteger("Speed", dir);
            }
            else
            {
                rigid.MovePosition(rigid.position + (new Vector2(dir, 0) * Time.deltaTime));

                Vector2 checkGround = new Vector2(rigid.position.x + dir, rigid.position.y);

                RaycastHit2D raycastHit2D = Physics2D.Raycast(checkGround, Vector2.down, 1, GroundLayer);

                if (raycastHit2D.collider == null)
                {
                    dir *= 0;
                }

                if (dir == -1)
                {
                    spriteRenderer.flipX = false;
                }
                else if (dir == 1)
                {
                    spriteRenderer.flipX = true;
                }

            }
        }

        void CheckPlayer()
        {
            Collider2D collider2D = Physics2D.OverlapCircle(rigid.position, detectPlayerDistance, PlayerLayer.value);

            if (collider2D != null)
            {
                player = collider2D.gameObject;
                //CancelInvoke("AutoDir");
                detectDir = false;
                Debug.Log(player);
            }
            else
            {
                player = null;
            }

        }

        private void OnDrawGizmos()
        {
            if (rigid == null)
            {
                return;
            }

            Gizmos.color = Color.green;

            Vector2 checkGround = new Vector2(rigid.position.x + dir, rigid.position.y);
            Gizmos.DrawLine(rigid.position + new Vector2(dir, 0), rigid.position + new Vector2(dir, -1));

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(rigid.position, 2.0f);

        }
    }
}
