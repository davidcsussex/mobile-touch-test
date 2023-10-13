using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{

    public int sampleWindow = 64;

    public AudioSource source;
    public Vector3 minScale = new Vector3(1,1,1);
    public Vector3 maxScale = new Vector3(3, 3, 3);

    public float loudness = 100;
    public float threshold = 0.1f;

    private AudioClip microphoneClip;

    // Start is called before the first frame update
    void Start()
    {
        MicrophoneToAudioClip();
    }

    // Update is called once per frame
    void Update()
    {

        float loudness = GetLoudnessFromAudioClip(source.timeSamples, source.clip);

        if (loudness < threshold)
            loudness = 0;

        print(GetLoudnessFromMicrophone());
        
    }

    public float GetLoudnessFromAudioClip( int clipPosition, AudioClip clip )
    {
        int startPosition = clipPosition - sampleWindow;
        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPosition);

        if ( startPosition < 0 )
        {
            return 0;
        }
        float totalLoudness = 0;
        

        for (int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }
        return totalLoudness/sampleWindow;


    }

    public void MicrophoneToAudioClip()
    {
        string micName = Microphone.devices[0];
        microphoneClip = Microphone.Start(micName, true, 20, AudioSettings.outputSampleRate);

    }

    public float GetLoudnessFromMicrophone()
    {
        return GetLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[0]), microphoneClip);
    }
}
