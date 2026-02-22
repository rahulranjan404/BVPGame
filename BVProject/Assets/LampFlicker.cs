using UnityEngine;
using System.Collections;

public class LampFlicker : MonoBehaviour
{
    [Header("References")]
    public Light lampLight;
    public AudioSource audioSource;
    public AudioClip flickerSound;

    [Header("Mode")]
    public bool randomMode = true;

    [Header("Random Mode Settings")]
    public float minInterval = 1f;
    public float maxInterval = 5f;
    public float minOffTime = 0.05f;
    public float maxOffTime = 0.5f;

    [Header("Manual Mode Settings")]
    public float manualGap = 2f;
    public float manualOffTime = 0.2f;

    [Header("Optional Intensity Variation")]
    public bool intensityVariation = false;
    public float minIntensity = 0.8f;
    public float maxIntensity = 1.2f;

    [Header("Sound Variation")]
    public bool randomPitch = true;
    public float minPitch = 0.9f;
    public float maxPitch = 1.1f;

    private float originalIntensity;

    void Start()
    {
        if (lampLight == null)
            lampLight = GetComponent<Light>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        originalIntensity = lampLight.intensity;

        StartCoroutine(FlickerLoop());
    }

    IEnumerator FlickerLoop()
    {
        while (true)
        {
            if (randomMode)
            {
                float waitTime = Random.Range(minInterval, maxInterval);
                yield return new WaitForSeconds(waitTime);

                float offTime = Random.Range(minOffTime, maxOffTime);
                yield return StartCoroutine(TurnOffFor(offTime));
            }
            else
            {
                yield return new WaitForSeconds(manualGap);
                yield return StartCoroutine(TurnOffFor(manualOffTime));
            }
        }
    }

    IEnumerator TurnOffFor(float duration)
    {
        // Play flicker sound
        PlayFlickerSound();

        lampLight.enabled = false;
        yield return new WaitForSeconds(duration);
        lampLight.enabled = true;

        if (intensityVariation)
            lampLight.intensity = Random.Range(minIntensity, maxIntensity);
        else
            lampLight.intensity = originalIntensity;
    }

    void PlayFlickerSound()
    {
        if (flickerSound != null && audioSource != null)
        {
            if (randomPitch)
                audioSource.pitch = Random.Range(minPitch, maxPitch);
            else
                audioSource.pitch = 1f;

            audioSource.PlayOneShot(flickerSound);
        }
    }
}