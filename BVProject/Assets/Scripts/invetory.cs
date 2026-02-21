using UnityEngine;

public class FlashlightToggle : MonoBehaviour
{
    public Light flashlight;   // Drag your Spotlight here
    private bool isOn = true;

    void Start()
    {
        if (flashlight != null)
            flashlight.enabled = isOn;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFlashlight();
        }
    }

    void ToggleFlashlight()
    {
        isOn = !isOn;
        flashlight.enabled = isOn;
    }
}