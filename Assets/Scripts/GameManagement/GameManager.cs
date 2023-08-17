using System;
using System.Collections;
using TorchRun.LevelGeneration;
using TorchRun.Player;
using TorchRun.UI;
using UnityEditor;
using UnityEngine;

namespace TorchRun.GameManagement
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerController player;
        [SerializeField] private Vector3 playerStartPosition;
        [SerializeField] private GameObject startPanel;
        [SerializeField] private GameObject resultsPanel;
        [SerializeField] private ResultsView resultsView;
        [SerializeField] private HallGenerator hallGenerator;
        [SerializeField] private StartCountdown countdown;
        [SerializeField] private float endGameDelay;
        private DateTime startTime;
        private TimeSpan timePlayed;

        public void StartGame()
        {
            player.transform.position = playerStartPosition;
            player.Torch.ResetValues();
            hallGenerator.ClearLevel();
            hallGenerator.GenerateFirstSegments();
            startPanel.SetActive(false);
            startPanel = resultsPanel;
            StartCoroutine(nameof(WaitForGameStart));
        }

        private IEnumerator WaitForGameStart()
        {
            yield return countdown.StartCoroutine(nameof(countdown.InitCountdown));
            startTime = DateTime.Now;
            timePlayed = TimeSpan.Zero;
        }

        public void EndGame()
        {
            timePlayed += DateTime.Now.Subtract(startTime);
            var bestResultMilliseconds = PlayerPrefs.GetInt("Best Result", -1);
            TimeSpan bestResult;
            if (bestResultMilliseconds == -1 || timePlayed.TotalMilliseconds > bestResultMilliseconds)
            {
                bestResult = timePlayed;
                PlayerPrefs.SetInt("Best Result", (int)bestResult.TotalMilliseconds);
            }
            else
            {
                bestResult = TimeSpan.FromMilliseconds(bestResultMilliseconds);
            }
            resultsView.SetResults(timePlayed, bestResult);
            StartCoroutine(nameof(WaitForGameEnd));
        }

        private IEnumerator WaitForGameEnd()
        {
            yield return new WaitForSeconds(endGameDelay);
            resultsPanel.SetActive(true);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                timePlayed += DateTime.Now.Subtract(startTime);
            }
            else
            {
                startTime = DateTime.Now;
            }
        }

        public void Exit()
        {
#if !UNITY_EDITOR
            Application.Quit();
#else
            EditorApplication.isPlaying = false;
#endif
        }
    }
}