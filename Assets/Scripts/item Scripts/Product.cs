using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;

using UnityEngine;
using UnityEngine.UI;

public class Product : MonoBehaviour
{
    Image img;
    [field: SerializeField] public ProductType type { get; private set; }
    [field: SerializeField] public float price { get; private set; }
    [field: SerializeField] public float weight { get; private set; }
    [field: SerializeField] public InteractionFailureType interactionType { get; private set; }
    [field: SerializeField] public Sprite sprite { get; private set; }

    void Start()
    {
        img = GetComponent<Image>();
        img.sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    bool RollForInteraction(float chance)
    {

        if (Random.Range(0, 101)
            >
            DayManager.Instance.chancesOfInteractionFailuresOccuring[(int)interactionType])
        {
            return false;
        }
        return true;
    }
}
