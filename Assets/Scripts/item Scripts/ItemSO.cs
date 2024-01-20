using System.Collections;
using System.Collections.Generic;

using Assets.Scripts.Enums;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



[CreateAssetMenu(fileName = "New Item")]
public class ItemSO : ScriptableObject
{
    [field: SerializeField] public InteractionFailureType interactionType { get; private set; }
    [field: SerializeField] public Sprite sprite { get; private set; }


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
