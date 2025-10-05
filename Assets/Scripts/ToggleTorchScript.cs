using UnityEngine;

public class ToggleTorchScript : MonoBehaviour
{
    [Header("Torch Settings")]
    public Light torchLight;
    public KeyCode toggleKey = KeyCode.F;
    public bool isOn = true;

    [Header("Flicker Settings")]
    public float minFlickerInterval = 3f;
    public float maxFlickerInterval = 7f;
    public float flickerDuration = 0.2f;
    public float flickerIntensityMin = 0.6f;
    public float flickerIntensityMax = 1.0f;

    [Header("Audio")]
    public AudioSource toggleSound;

    private float nextFlickerTime = 0f;
    private float defaultIntensity;
    private bool isFlickering = false;

    void Start()
    {
        if (torchLight != null)
        {
            torchLight.enabled = isOn;
            defaultIntensity = torchLight.intensity;
        }

        SetNextFlickerTime();
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleTorch();
        }

        if (isOn && Time.time >= nextFlickerTime && !isFlickering)
        {
            StartCoroutine(Flicker());
        }
    }

    void ToggleTorch()
    {
        isOn = !isOn;

        if (torchLight != null)
            torchLight.enabled = isOn;

        if (toggleSound != null)
            toggleSound.Play();
    }

    System.Collections.IEnumerator Flicker()
    {
        isFlickering = true;
        torchLight.intensity = Random.Range(flickerIntensityMin, flickerIntensityMax / 1.5f);
        yield return new WaitForSeconds(flickerDuration);
        torchLight.intensity = defaultIntensity;
        isFlickering = false;
        SetNextFlickerTime();
    }

    void SetNextFlickerTime()
    {
        nextFlickerTime = Time.time + Random.Range(minFlickerInterval, maxFlickerInterval);
    }
}
