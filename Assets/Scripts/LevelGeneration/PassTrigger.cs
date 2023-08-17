using UnityEngine;

namespace TorchRun.LevelGeneration
{
    public class PassTrigger : MonoBehaviour
    {
        private HallGenerator hallGenerator;

        public HallGenerator HallGenerator
        {
            get => hallGenerator;
            set => hallGenerator = value;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                hallGenerator.AddNewSegment();
            }
        }
    }
}