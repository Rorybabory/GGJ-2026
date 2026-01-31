using UnityEngine;

public class PlayerRaycaster : MonoBehaviour
{
    private GameObject target;
    
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity))
        {
            target = hit.collider.gameObject;
        }
    }

    public GameObject GetTarget()
    {
        return target;
    }
}
