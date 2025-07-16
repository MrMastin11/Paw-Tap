using UnityEngine;
using UnityEngine.UI;

public class ButtonScaler : MonoBehaviour
{
    private Vector3 originalScale;
    public float scaleFactor = 1.2f; 
    public float animationSpeed = 20f; 
    private void Start()
    {
        originalScale = transform.localScale;    
    }

    public void OnPointerDown()
    {
        transform.localScale = originalScale * scaleFactor;
    }
    public void OnPointerUp()
    {
        transform.localScale = originalScale; 
    }
}
