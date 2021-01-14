using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightViewControl : MonoBehaviour {

    [Header("Light Variables")]
    public float minLight;
    public float maxLight;

    private const float _minLight = 1;
    private const float _maxLight = 5;

    [Header("Angle Variables")]
    public float minAngle;
    public float maxAngle;

    [SerializeField]
    private float currentLight;


    [Header("References")]
    public Texture2D viewImage;

    [SerializeField]
    private Camera cameraRef;
    [SerializeField]
    private Light spotLight;

    // Update is called once per frame
    void Update() {
        float H, S, V;

        Color.RGBToHSV(cameraRef.backgroundColor, out H, out S, out V);

        cameraRef.backgroundColor = Color.HSVToRGB(H, S, GetCurrentBrightness());

        if (currentLight > maxLight) {
            currentLight = maxLight;
        }
        else if (currentLight < minLight) {
            currentLight = minLight;
        }

        spotLight.intensity = GetLightIntensity();
        spotLight.spotAngle = GetLightAngle();
    }

    float GetLightIntensity() {
        return Mathf.Clamp(_maxLight * (currentLight / maxLight), _minLight, _maxLight);
    }

    float GetLightAngle() {
        return Mathf.Clamp(maxAngle * (currentLight / maxLight), minAngle, maxAngle);
    }

    float GetCurrentBrightness() {
        return 0.2f + currentLight / maxLight;
    }
}
