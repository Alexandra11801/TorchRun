using System.Collections;
using TorchRun.GameManagement;
using TorchRun.Player;
using TorchRun.UI;
using UnityEngine;

namespace TorchRun.Torch
{
    public class Torch : MonoBehaviour
    {
        [SerializeField] private int initBrightness;
        [SerializeField] private int minBrightness;
        [SerializeField] private TorchBrightnessView brightnessView;
        [SerializeField] private Light dispersedLight;
        [SerializeField] private Light torchLight;
        [SerializeField] private float maxDispersedLightIntensity;
        [SerializeField] private float minTorchLightRange;
        [SerializeField] private float maxTorchLightRange;
        [SerializeField] private float brightnessToIntensityCoefficient;
        [SerializeField] private float brightnessToRangeCoefficient;
        [SerializeField] private PlayerController player;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private float waitBeforePlayerStop;
        private int brightness;

        public void ResetValues()
        {
            brightness = initBrightness;
            dispersedLight.intensity = CalculateDispersedLightIntensity(brightness);
            torchLight.range = CalculateTorchLightRange(brightness);
            brightnessView.SetBrightnessValue(brightness);
        }
        
        public void ChangeBrightness(int brightnessPoints)
        {
            var targetBrightness = brightness + brightnessPoints >= minBrightness
                ? brightness + brightnessPoints
                : minBrightness; 
            brightness = targetBrightness;
            brightnessView.SetBrightnessValue(brightness);

            var targetIntensity = CalculateDispersedLightIntensity(brightness);
            dispersedLight.intensity = targetIntensity <= maxDispersedLightIntensity ? targetIntensity : maxDispersedLightIntensity;

            var targetRange = CalculateTorchLightRange(brightness);
            torchLight.range = targetRange <= maxTorchLightRange ? targetRange : maxTorchLightRange;
            
            if (brightness == minBrightness)
            {
                StartCoroutine(nameof(WaitForPlayerStop));
            }
        }

        private float CalculateDispersedLightIntensity(float brightness)
        {
            return brightness * brightnessToIntensityCoefficient;
        }

        private float CalculateTorchLightRange(float brightness)
        {
            return minTorchLightRange + brightness * brightnessToRangeCoefficient;
        }

        private IEnumerator WaitForPlayerStop()
        {
            yield return new WaitForSeconds(waitBeforePlayerStop);
            player.Stop();
            gameManager.EndGame();
        }
    }
}