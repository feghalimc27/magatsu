using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalPadInput : MonoBehaviour {
    public static bool dpadLeft, dpadRight, dpadUp, dpadDown;
    private float _lastX = 1, _lastY = 1;

    // Update is called once per frame
    void Update() {
        float x = Input.GetAxis("dpadHorizontalDebug");
        float y = Input.GetAxis("dpadVerticalDebug");

        // Debug.Log("(" + x + ", " + y + ")");

        if (_lastX != x) {
            dpadLeft = (x == -1);
            dpadRight = (x == 1);
        }
        else {
            dpadLeft = false;
            dpadRight = false;
        }

        if (_lastY != y) {
            dpadUp = (y == 1);
            dpadDown = (y == -1);
        }
        else {
            dpadUp = false;
            dpadDown = false;
        }

        _lastX = x;
        _lastY = y;
    }
}
