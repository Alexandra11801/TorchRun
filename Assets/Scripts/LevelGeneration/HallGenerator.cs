using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TorchRun.LevelGeneration
{
    public class HallGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject[] freeSegments;
        [SerializeField] private GameObject[] waterSegments;
        [SerializeField] private float hallLength;
        [SerializeField] private Vector3 defaultSpawnPosition;
        [SerializeField] private int initialSpawnCount;
        private List<GameObject> addedHalls;

        private void Start()
        {
            addedHalls = new List<GameObject>();
        }

        public void GenerateFirstSegments()
        {
            for (var i = 0; i < initialSpawnCount; i++)
            {
                AddNewSegment();
            }
        }

        public void AddNewSegment()
        {
            var segmentsArray = addedHalls.Count % 2 == 0 ? freeSegments : waterSegments;
            var segment = segmentsArray[Random.Range(0, segmentsArray.Length)];
            var newSegment = Instantiate(segment, 
                defaultSpawnPosition + addedHalls.Count * hallLength * Vector3.forward, Quaternion.identity, transform);
            newSegment.GetComponentInChildren<PassTrigger>().HallGenerator = this;
            addedHalls.Add(newSegment);
        }

        public void ClearLevel()
        {
            var segmentsCount = addedHalls.Count;
            for (var i = 0; i < segmentsCount; i++)
            {
                var segment = addedHalls[0];
                addedHalls.Remove(segment);
                Destroy(segment);
            }
        }
    }
}