using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour
{
    [SerializeField] private GameObject mainRefrence;
    [SerializeField] private GameObject objectToEnable1;
    [SerializeField] private GameObject objectToEnable2;

    public void EnableAnimation()
    {
    Animator animator = mainRefrence.GetComponent<Animator>();
        if (animator != null)
        {
            animator.enabled = true;
        }
    }
    public void EnableObjects()
    {
        objectToEnable1.SetActive(true);
        objectToEnable2.SetActive(true);
    }
}

