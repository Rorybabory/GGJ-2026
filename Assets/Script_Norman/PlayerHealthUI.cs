using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    private Slider healthSlider;
    
    void Start()
    {
        healthSlider = GetComponent<Slider>();
    }

    void Update()
    {
        healthSlider.value = playerHealth.currHealth/playerHealth.GetMaxHealth();
    }
}
