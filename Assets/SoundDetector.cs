using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundDetector : MonoBehaviour
{
    public List<GameObject> detectedSounds = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        detectedSounds.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (detectedSounds.Contains(other.gameObject))
        {
            detectedSounds.Remove(other.gameObject);
        }
    }

    public GameObject SoundWithTag(string soundTag)
    {
        return detectedSounds.FirstOrDefault(o => o.CompareTag(soundTag));
    }
}
