using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silly
{
    public class Item : MonoBehaviour
    {
        Animator animator;
        // Start is called before the first frame update
        void Start()
        {
            animator = this.GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void Open()
        {
            animator.SetTrigger("Open");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Open();
            }
        }
    }
}
