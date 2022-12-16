using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] ChickenManager chickenManager;
    [SerializeField] WinnersChecker winnersChecker;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI currentGenerationText;
    [SerializeField] TextMeshProUGUI currentChickenText;
    [SerializeField] TextMeshProUGUI winnersCountText;

    private void Start()
    {
        UpdateUI();
    }

    /// <summary>
    /// Updating UI. It's called at the start of the simulation and each time a new generation will start the race.
    /// </summary>
    public void UpdateUI()
    {
        SetGenerationUI();
        SetChickenCountUI();
        SetWinnersCountUI();
    }

    // Affichage de la g�n�ration actuelle
    public void SetGenerationUI()
    {
        currentGenerationText.text = chickenManager.generationNumber.ToString();
    }

    // Affichage du nombre de poulets restants
    public void SetChickenCountUI()
    {
        currentChickenText.text = chickenManager.currentChickenAmount.ToString();
    }

    public void SetWinnersCountUI()
    {
        winnersCountText.text = winnersChecker.winnersCount.ToString();
    }
}
