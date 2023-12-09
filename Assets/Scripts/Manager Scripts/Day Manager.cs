using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Assets.Scripts.Enums;

using UnityEngine;

public class DayManager : Singleton<DayManager>
{
    [Tooltip("index 0 - chance of age verifictaion occuring\n" +
             "index 1 - chance of weigh again occuring\n" +
             "index 2 - chance of check scanned product occuring\n" +
             "index 3 - chance of TO BE ADDED occuring\n" +
             "index 4 - chance of TO BE ADDED occuring\n" +
             "DO NOT CHANGE THE ORDER!!!")]
    public List<byte> chancesOfInteractionOccuring;

    public List<GameObject> products;

    private void Start()
    {
        for (int i = 0; i < chancesOfInteractionOccuring.Count; i++)
        {
            if (chancesOfInteractionOccuring[i] < 0)
            {
                chancesOfInteractionOccuring[i] = 0;
            } else if (chancesOfInteractionOccuring[i] > 100)
            {
                chancesOfInteractionOccuring[i] = 100;
            }
        }
    }
}
