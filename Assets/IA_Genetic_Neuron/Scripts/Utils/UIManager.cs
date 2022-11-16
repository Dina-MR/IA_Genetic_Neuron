using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] ChickenManager chickenManager;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI currentGenerationText;
    [SerializeField] TextMeshProUGUI currentChickenText;

    private void Start()
    {
        SetGenerationUI();
        SetChickenCountUI();
    }

    // Affichage de la génération actuelle
    private void SetGenerationUI()
    {
        currentGenerationText.text = chickenManager.generationNumber.ToString();
    }

    // Affichage du nombre de poulets restants
    private void SetChickenCountUI()
    {
        currentChickenText.text = chickenManager.currentChickenAmount.ToString();
    }
}
