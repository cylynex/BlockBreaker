using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    [SerializeField] float gameBoardUnits = 16f;
    [SerializeField] float mousePosition;
    
    private void Update() {
        mousePosition = Input.mousePosition.x / Screen.width * gameBoardUnits;
        Vector2 newPosition = new Vector2(Mathf.Clamp(mousePosition,1, gameBoardUnits - 1), transform.position.y);
        transform.position = newPosition;
    }

}