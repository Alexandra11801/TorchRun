using TorchRun.Player;
using UnityEngine;

namespace TorchRun.Torch
{
    public class CollectibleLight : MonoBehaviour
    {
        [SerializeField] private AudioSource audio;
        [SerializeField] private int brightnessIncreasePoints;
        [SerializeField] private GameObject light;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerController>(out var player))
            {
                audio.Play();
                player.Torch.ChangeBrightness(brightnessIncreasePoints);
                Destroy(light);
            }
        }
    }
}