using TorchRun.Player;
using TorchRun.Torch;
using UnityEngine;

namespace TorchRun.Torch
{
    public class Water : MonoBehaviour
    {
        [SerializeField] private AudioSource audio;
        [SerializeField] private int brightnessDecreasePoints;
        private bool enteredOnce;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerController>())
            {
                audio.Play();
            }
            else if (other.TryGetComponent<Torch>(out var torch))
            {
                torch.ChangeBrightness(-brightnessDecreasePoints);
            }
        }
    }
}