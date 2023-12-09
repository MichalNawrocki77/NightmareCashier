using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : MonoBehaviour
{
    internal Customer customer;
    internal Player player;
    internal void InjectDependencies(Customer customer, Player player)
    {
        this.customer = customer;
        this.player = player;
    }
}
