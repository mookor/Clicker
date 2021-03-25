using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapTarget : MonoBehaviour
{
    private Vector2 normalScale;
    private Vector2 tapScale;

    private void Awake() {
        normalScale = transform.localScale;
        tapScale = normalScale*1.1f;
    }
    
    private void OnMouseDown() {
        transform.localScale = tapScale;
        Clicker.Instance.Click();
    }

    private void OnMouseUp() {
        transform.localScale = normalScale;
        
    }
}
