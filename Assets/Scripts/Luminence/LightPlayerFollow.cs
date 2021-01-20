using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPlayerFollow : MonoBehaviour {

    [Header("Positional Variables")]
    public Vector2 positionalOffset;

    [Header("Technical Variables")]
    public int framesToDelay;
    public GameObject followReference;
    
    // Player and Direction may not be needed
    private List<Vector3> positions;
    private bool direction;

    void Start() {
        // Use for getDirection to position light in direction of movement
        positions = new List<Vector3>();
    }

    // Update is called once per frame
    void Update() {
        UpdatePositionalOffset();
        DelayMovement();
    }

    void DelayMovement() {
        if (positions.Count < framesToDelay) {
            positions.Add(followReference.transform.position);
        }
        else {
            gameObject.transform.position = positions[0] + new Vector3(positionalOffset.x, positionalOffset.y, 0);
            positions.RemoveAt(0);
        }
    }


    // TODO: Change to rotate light up and down
    void UpdatePositionalOffset() {
        // Right stick to look around slightly
        //positionalOffset.x = Input.GetAxis("rHorizontal") / 2;
        //positionalOffset.y = -Input.GetAxis("rVertical") / 2;
    }
}
