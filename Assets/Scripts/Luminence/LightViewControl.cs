using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightViewControl : MonoBehaviour {

    public float minLight;
    public float maxLight;

    private float _minLight = 1;
    private float _maxLight = 5;

    [SerializeField]
    private float currentLight;

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
    }

    float GetLightIntensity() {
        return Mathf.Clamp(_maxLight * (currentLight / maxLight), _minLight, _maxLight);
    }

    float GetCurrentBrightness() {
        return 0.2f + currentLight / maxLight;
    }
}
