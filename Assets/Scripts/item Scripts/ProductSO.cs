using System.Collections;
using System.Collections.Generic;

using Assets.Scripts.Enums;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



[CreateAssetMenu(fileName = "New Product")]
public class ProductSO : ScriptableObject
{
    [field: SerializeField] public ProductType type { get; private set; }
    [field: SerializeField] public float price { get; private set; }
    [field: SerializeField] public float weight { get; private set; }
    [field: SerializeField] public InteractionFailureType interactionType { get; private set; }
    [field: SerializeField] public Sprite sprite { get; private set; }
}
