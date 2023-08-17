using UnityEngine;

namespace TorchRun.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float runningSpeed;
        [SerializeField] private float maxXOffset;
        [SerializeField] private Torch.Torch torch;
        private bool running;

        public Torch.Torch Torch => torch;
        public bool Running => running;
        
        private void Update()
        {
            if (running)
            {
                var targetPosition = transform.position + transform.forward * runningSpeed;
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
            }
        }

        public void Run()
        {
            animator.SetTrigger("Run");
            running = true;
        }

        public void Stop()
        {
            animator.SetTrigger("Stop");
            running = false;
        }

        public void MoveSideways(float xDelta)
        {
            var targetX = Mathf.Abs(transform.position.x + xDelta) <= maxXOffset
                ? transform.position.x + xDelta
                : maxXOffset * Mathf.Sign(transform.position.x + xDelta);
            var targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
            transform.position = targetPosition;
        }
    }
}