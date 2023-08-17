using System;
using TMPro;
using UnityEngine;

namespace TorchRun.UI
{
    public class ResultsView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentResultLabel;
        [SerializeField] private TextMeshProUGUI bestResultLabel;

        public void SetResults(TimeSpan currentResult)
        {
            currentResultLabel.text = "Your result: " + currentResult.ToString(@"hh\:mm\:ss\.fff");
            bestResultLabel.text = "";
        }
        
        public void SetResults(TimeSpan currentResult, TimeSpan bestResult)
        {
            currentResultLabel.text = "Your result: " + currentResult.ToString(@"hh\:mm\:ss\.fff");
            bestResultLabel.text = "Best result: " + bestResult.ToString(@"hh\:mm\:ss\.fff");
        }
    }
}