using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] ItemSO itemSource;
    SpriteRenderer rend;
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = itemSource.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
