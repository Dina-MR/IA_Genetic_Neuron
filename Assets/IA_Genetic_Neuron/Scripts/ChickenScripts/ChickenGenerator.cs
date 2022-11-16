using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenGenerator : MonoBehaviour
{
    [SerializeField] ChickenManager chickenManager;

    [Header("Chicken generation")]
    [SerializeField] private GameObject _chickenAsset; // Modèle de poulet
    [SerializeField] private int _initialChickenAmount; // Nombre de poulets initial, dans la 1ère génération

    [Header("Positions")]
    // Positions extrêmes sur l'axe x
    [SerializeField] private float _minimumX;
    [SerializeField] private float _maximumX;
    // Positions sur les axes y et z, pour le parcours sur le terrain
    [SerializeField] private float _positionY;
    [SerializeField] private float _positionZ; // Position de départ

    private void Start()
    {
        chickenManager.currentChickenAmount = _initialChickenAmount;
        chickenManager.chickenList = new List<GameObject>();
        GenerateFirstGeneration();
    }

    /*
     * Fonction générale
     */

    // Initialisation de la 1ère génération
    private void GenerateFirstGeneration()
    {
        for (int i = 0; i < _initialChickenAmount; i++)
            chickenManager.chickenList.Add(GenerateChicken(i));
    }

    /*
     * Fonctions utilitaires
     */

    // Création d'un poulet
    private GameObject GenerateChicken(int chickenId)
    {
        GameObject chicken = Instantiate(_chickenAsset, RandomizePosition(), Quaternion.identity);
        chicken.name = NameChicken(chickenId);
        return chicken;
    }

    // Position aléatoire d'un poulet
    private Vector3 RandomizePosition()
    {
        return new Vector3(Random.Range(_minimumX, _maximumX), _positionY, _positionZ);
    }

    // Nommage du poulet
    private string NameChicken(int chickenId)
    {
        return "Chicken n°" + chickenId;
    }
}
