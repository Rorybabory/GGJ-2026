using UnityEngine;
using UnityEngine.Events;

public class InputDelegate : MonoBehaviour
{
    public UnityEvent mouseDown;
    public UnityEvent mouseUp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDown.Invoke();
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseUp.Invoke();
        }
    }
}
