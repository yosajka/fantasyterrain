using System;
using UnityEngine;

public class RainSpawner : MonoBehaviour
{   
    [SerializeField]
    private GameObject rain;

    // [SerializeField]
    // private AudioSource rainAudio;

    [SerializeField]
    private WindZone windZone;

    public static bool isRaining;
    
    [SerializeField]
    private int startRainHour;
    private TimeSpan startRainTime;

    [SerializeField]
    private int rainDuration;
    private TimeSpan endRainTime;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRaining == false)
        {
            startRainHour = UnityEngine.Random.Range(0, 20);
            startRainTime = TimeSpan.FromHours(startRainHour);
            endRainTime = startRainTime + TimeSpan.FromHours(rainDuration);
            isRaining = true;
        }

        if (TimeController.currentTime.TimeOfDay >= startRainTime
            &&  TimeController.currentTime.TimeOfDay < endRainTime + TimeSpan.FromHours(1))
        {
            //isRaining = true;
            rain.SetActive(true);
            //windZone.windMain = Mathf.Lerp(0.5f, 4f, 1f);

            //rainAudio.enabled = true;
            
            if (TimeController.currentTime.TimeOfDay >= endRainTime)
            {
                isRaining = false;
                rain.SetActive(false);
                //rainAudio.enabled = false;
                //windZone.windMain = Mathf.Lerp(4f, 0.5f, 1f);
            }
        }

        
    }
}
