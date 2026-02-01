using UnityEngine;

public class Hover : MonoBehaviour
{
    [SerializeField] float magnitude = 5.0f;
    [SerializeField] float speed = 5.0f;
    float sinTimer = 0.0f;
    Vector3 basePosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        basePosition = transform.localPosition;
        sinTimer = Random.Range(0.0f, Mathf.PI * 2);
    }

    // Update is called once per frame
    void Update()
    {
        sinTimer += Time.deltaTime * speed;
        transform.position = basePosition + new Vector3(0.0f, Mathf.Sin(sinTimer)*magnitude, 0.0f);
    }
}
