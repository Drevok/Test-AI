using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FSAIScripts
{
    public class CollisionDetector : MonoBehaviour
    {
        public List<GameObject> detectedGameObjects = new List<GameObject>();

        private void OnTriggerEnter(Collider other)
        {
            detectedGameObjects.Add(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if (detectedGameObjects.Contains(other.gameObject))
            detectedGameObjects.Remove(other.gameObject);
        }

        public GameObject firstWithTag(string withTag)
        {
            return detectedGameObjects.FirstOrDefault(o => o.CompareTag(withTag));
        }
    }
}