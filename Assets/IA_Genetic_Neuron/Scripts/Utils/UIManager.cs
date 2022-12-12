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
        SetGenerationUI();
        SetChickenCountUI();
        SetWinnersCountUI();
    }

    // Affichage de la génération actuelle
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
