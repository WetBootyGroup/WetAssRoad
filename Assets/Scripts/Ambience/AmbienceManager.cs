using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AmbienceManager : MonoBehaviour
{
    public float minTimeSpawn = 1f;
    public float maxTimeSpawn = 15f;
    public float minPan = -0.5f;
    public float maxPan = 0.5f;
    public AudioArgument audioArgument;
    public List<AudioClip> clipList;

    private AudioSource _audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(AudioSpawner());
    }

    IEnumerator AudioSpawner()
    {
        const float defaultWaitTime = 0.5f;
        while (true)
        {
            float waitTime = Random.Range(minTimeSpawn, maxTimeSpawn);
            
            yield return new WaitForSeconds(waitTime);

            _audioSource.clip = clipList[Random.Range(0,clipList.Count)];
            _audioSource.volume = Random.Range(
                audioArgument.minRandomVolume, audioArgument.maxRandomPitch);
            _audioSource.pitch = Random.Range(
                audioArgument.minRandomPitch, audioArgument.maxRandomPitch);
            _audioSource.panStereo = Random.Range(minPan, maxPan);
            _audioSource.Play();
            
            yield return new WaitForSeconds(defaultWaitTime); // allow buffer

            while (_audioSource.isPlaying)
            {
                yield return new WaitForSeconds(defaultWaitTime);
            }
        }
        // ReSharper disable once IteratorNeverReturns
    }
}
