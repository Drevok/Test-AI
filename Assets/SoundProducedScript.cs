using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundProducedScript : MonoBehaviour
{
    private float timeGrowing = 0;

    // Update is called once per frame
    void Update()
    {
        NoiseGrowing();
    }

    void NoiseGrowing()
    {
        timeGrowing += Time.deltaTime;
        
        Vector3 soundScale = new Vector3(timeGrowing * 0.5f, timeGrowing * 0.5f, timeGrowing *0.5f);

        gameObject.transform.localScale += soundScale;

        if (timeGrowing >= 0.5f)
        {
            DestroySound();
        }
    }

    void DestroySound()
    {
        Destroy(gameObject);
    }
}
