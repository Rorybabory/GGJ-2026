using System;
using UnityEngine;

public class PlayerSignature : MonoBehaviour
{
    public static PlayerSignature Instance { get; private set; }
    public Transform playerTransform;

    public MaskHolder PlayerMaskHolder;

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            // Optional: use DontDestroyOnLoad() if the player persists across scenes
            // DontDestroyOnLoad(this.gameObject); 
        }
        
        playerTransform = this.transform;
    }
}
