using UnityEngine;

[CreateAssetMenu(fileName = "HandUIResource", menuName = "Scriptable Objects/HandUIResource")]
public class HandUIResource : ScriptableObject
{
    public GameObject handPrefab;
    public AnimationClip idleAnimation;
    public AnimationClip LclickAnimation;
    public AnimationClip RclickAnimation;
}
