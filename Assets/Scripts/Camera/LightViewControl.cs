using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightViewControl : MonoBehaviour {

    public float minLight;
    public float maxLight;

    [SerializeField]
    private float currentLight;

    public Texture2D viewImage;

    private Camera camera;

    // Start is called before the first frame update
    void Start() {
        camera = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update() {
        float H, S, V;

        Color.RGBToHSV(camera.backgroundColor, out H, out S, out V);

        camera.backgroundColor = Color.HSVToRGB(H, S, GetCurrentBrightness());

        if (currentLight > maxLight) {
            currentLight = maxLight;
        }
        else if (currentLight < minLight) {
            currentLight = minLight;
        }
    }

    float GetCurrentBrightness() {
        return 0.2f + currentLight / maxLight;
    }
}
