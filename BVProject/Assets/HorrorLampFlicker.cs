using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class HorrorLampFlicker : MonoBehaviour
{
    [Header("Light Reference")]
    public Light lampLight;

    [Header("Flicker Timing")]
    public float minInterval = 2f;
    public float maxInterval = 6f;
    public float minBlackoutTime = 0.1f;
    public float maxBlackoutTime = 0.6f;

    [Header("Emission Settings")]
    public Color emissionColor = Color.yellow;
    public float emissionMultiplier = 2f;

    [Header("Continuous Instability")]
    public bool continuousFlicker = true;
    public float baseVariationAmount = 0.15f;
    public float noiseSpeed = 3f;

    [Header("Aggression Level")]
    [Range(1, 5)]
    public int aggression = 3;

    private Material glowMaterial;
    private float originalIntensity;
    private float noiseOffset;
    private bool isInBlackout = false;

    void Start()
    {
        if (lampLight == null)
            lampLight = GetComponentInChildren<Light>();

        glowMaterial = GetComponent<Renderer>().material;
        glowMaterial.EnableKeyword("_EMISSION");

        originalIntensity = lampLight.intensity;
        noiseOffset = Random.Range(0f, 100f);

        SetEmission(originalIntensity * emissionMultiplier);

        StartCoroutine(FlickerLoop());
    }

    void Update()
    {
        // If blackout or light disabled → force emission OFF
        if (isInBlackout || !lampLight.enabled)
        {
            SetEmission(0f);
            return;
        }

        // Continuous subtle instability
        if (continuousFlicker)
        {
            float noise = Mathf.PerlinNoise(Time.time * noiseSpeed, noiseOffset);
            float variation = (noise - 0.5f) * 2f * baseVariationAmount;

            float dynamicIntensity = originalIntensity + variation;
            lampLight.intensity = dynamicIntensity;

            SetEmission(dynamicIntensity * emissionMultiplier);
        }
        else
        {
            SetEmission(lampLight.intensity * emissionMultiplier);
        }
    }

    IEnumerator FlickerLoop()
    {
        while (true)
        {
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);

            float blackout = Random.Range(minBlackoutTime, maxBlackoutTime);
            yield return StartCoroutine(AggressiveFlicker(blackout));
        }
    }

    IEnumerator AggressiveFlicker(float blackoutDuration)
    {
        int microFlickers = Random.Range(2 * aggression, 5 * aggression);

        // ⚡ Instability burst
        for (int i = 0; i < microFlickers; i++)
        {
            float randomIntensity = Random.Range(0.2f, 1.5f) * originalIntensity;
            lampLight.intensity = randomIntensity;
            SetEmission(randomIntensity * emissionMultiplier * 1.5f);

            yield return new WaitForSeconds(Random.Range(0.02f, 0.07f));
        }

        // ⚡ Over-bright surge
        float flashIntensity = originalIntensity * (2f + aggression);
        lampLight.intensity = flashIntensity;
        SetEmission(flashIntensity * emissionMultiplier * 2f);

        yield return new WaitForSeconds(0.05f);

        // ⚡ TRUE Blackout (no ghost glow)
        isInBlackout = true;
        lampLight.enabled = false;
        SetEmission(0f);

        yield return new WaitForSeconds(blackoutDuration);

        // ⚡ Restore
        lampLight.enabled = true;
        isInBlackout = false;

        lampLight.intensity = originalIntensity;
    }

    void SetEmission(float intensity)
    {
        if (glowMaterial != null)
        {
            glowMaterial.SetColor("_EmissionColor", emissionColor * intensity);
        }
    }
}