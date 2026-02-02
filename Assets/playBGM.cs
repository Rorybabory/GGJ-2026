using AudioSystem;
using UnityEngine;

public class playBGM : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.SwitchBGM(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
