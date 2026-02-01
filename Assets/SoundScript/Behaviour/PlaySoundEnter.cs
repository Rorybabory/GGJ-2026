using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioSystem;

public class PlaySoundEnter : StateMachineBehaviour
{
    [SerializeField] private string soundName;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AudioManager.instance.PlayAudio(soundName);
    }
}
