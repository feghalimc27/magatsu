using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightViewControl : MonoBehaviour {

    [Header("Light Variables")]
    public float minLight;
    public float maxLight;

    private const float _minLight = 0.4f;
    private const float _maxLight = 1.5f;

    [Header("Angle Variables")]
    public float minAngle = 1;
    public float maxAngle = 9;

    [SerializeField]
    private float currentLight;


    [Header("References")]
    public Texture2D viewImage;

    [SerializeField]
    private Camera cameraRef;
    [SerializeField]
    private Light2D spotLight;

    [Header("Debug")]
    public bool debug;

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
        spotLight.pointLightOuterRadius = GetLightAngle();

        if (debug) {
            DebugDepadIncrementLighting();
        }
    }

    void DebugDepadIncrementLighting() {
        if (DirectionalPadInput.dpadRight) {
            currentLight += 10;
        }
        else if (DirectionalPadInput.dpadLeft) {
            currentLight -= 10;
        }
    }

    float GetLightIntensity() {
        if (_maxLight * (currentLight / maxLight) < _minLight) {
            return  _minLight * (currentLight / maxLight) + _minLight;
        }

        return Mathf.Clamp(_maxLight * (currentLight / maxLight), _minLight, _maxLight);
    }

    float GetLightAngle() {
        if (maxAngle * (currentLight / maxLight) < minAngle) {
            return  (currentLight / maxLight) + minAngle;
        }
        return Mathf.Clamp(maxAngle * (currentLight / maxLight), minAngle, maxAngle);
    }

    float GetCurrentBrightness() {
        return 0.2f + currentLight / maxLight;
    }
}
