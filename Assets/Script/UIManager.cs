using UnityEngine;
using UnityEngine.UI; // For UI elements

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; // Singleton reference
    public Image companionHPBar; //Companion HP bar UI

   void Awake()
    {
        Instance = this; 
    }
    //Update companion HP bar
    public void UpdateCompanionHP(float currentHealth, float maxHealth)
    {
        if (companionHPBar != null)
        {
            companionHPBar.fillAmount = currentHealth / maxHealth;
        }
    }
}
