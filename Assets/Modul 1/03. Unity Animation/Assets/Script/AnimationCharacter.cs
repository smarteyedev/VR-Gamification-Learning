using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModulKetiga
{
    public class AnimationCharacter : MonoBehaviour
    {
        private Animator _animator;
        private Rigidbody _rb;
        public float moveSpeed = 2f;

        private bool isMoving = false;

        public Transform startTransform;
        public Transform targetTransform;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody>();

            if (startTransform != null && targetTransform != null)
            {
                transform.position = startTransform.position;
                isMoving = true;
            }
        }

        public void Initialize(Transform start, Transform target)
        {
            startTransform = start;
            targetTransform = target;

            isMoving = true;
        }

        private void Update()
        {
            if (isMoving && startTransform != null && targetTransform != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, targetTransform.position) < 0.01f)
                {
                    isMoving = false;
                    ChangeAnimation();
                }
            }
        }

        public void ChangeAnimation()
        {
            _animator.SetTrigger("Change");
        }
    }
}
