using TMPro;
using UnityEngine;

namespace TorchRun.UI
{
    public class TorchBrightnessView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;

        public void SetBrightnessValue(int brightness)
        {
            label.text = brightness.ToString();
        }
    }
}