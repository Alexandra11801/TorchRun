using System.Collections;
using TMPro;
using TorchRun.Player;
using UnityEngine;

namespace TorchRun.UI
{
    public class StartCountdown : MonoBehaviour
    {
        [SerializeField] private PlayerController player;
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private string[] labelValues;
        [SerializeField] private float labelShowTime;
        [SerializeField] private float beginningDelay;
        [SerializeField] private AudioSource audio;

        public IEnumerator InitCountdown()
        {
            yield return new WaitForSeconds(beginningDelay);
            foreach (var labelValue in labelValues)
            {
                label.text = labelValue;
                audio.Play();
                yield return new WaitForSeconds(labelShowTime);
            }
            label.text = "";
            player.Run();
        }
    }
}