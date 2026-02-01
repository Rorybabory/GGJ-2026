using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioSystem;

public class PlayAudio : MonoBehaviour
{
    public void PlayOneShot(string name)
    {
        AudioManager.instance.PlayAudio(name);
    }
}
