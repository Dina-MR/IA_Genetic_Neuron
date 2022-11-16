using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;

public class ChickenManager : MonoBehaviour
{
    [HideInInspector] public int generationNumber = 1;
    [HideInInspector] public int currentChickenAmount; // Nombre de poulets restants
    [HideInInspector] public List<GameObject> chickenList; // Liste de poulets

    private void Start()
    {
        
    }
}
