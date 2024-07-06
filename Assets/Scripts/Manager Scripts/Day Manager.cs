using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;

using Assets.Scripts.Enums;

using Unity.VisualScripting;

using UnityEngine;

using Random = UnityEngine.Random;

public class DayManager : Singleton<DayManager>
{
    [Tooltip("index 0 - chance of incorrect weight occuring\n" +
             "index 1 - chance of incorrect Quantity occuring\n"+
             "DO NOT CHANGE THE ORDER!!!")]
    public List<byte> chancesOfInteractionFailuresOccuring;

    [Tooltip("Make sure that the index of product's shelf matches the enum to int cast value of product's type (the way I take the transform value is by casting product's enum type to int as an index of this List)")]
    public List<Transform> productShelves;

    public List<GameObject> products;
    [HideInInspector] public List<Product> productList;

    [Tooltip("Minimum time in seconds for customers to spawn")]
    public int minCustomerSpawnInterval;
    [Tooltip("Maximum time in seconds for customers to spawn")]
    public int maxCustomerSpawnInterval;

    [Tooltip("Minimum time in seconds for customers to go to next product shelf")]
    public int minCustomerShelfWait;
    [Tooltip("Maximum time in seconds for customers to go to next product shelf")]
    public int maxCustomerShelfWait;

    public SelfServiceCheckoutQueue selfServiceQueue;
    [Tooltip("Make sure to not add self service queue to this list, since that has it's own field.")]
    public List<CheckoutQueue> Queues;

    public bool spawnCustomers;

    [SerializeField] List<GameObject> customerPrefabs;
    [SerializeField] Transform customerSpawnPoint;
    public Transform customerExitPoint;




    [SerializeField]
    GameObject StrikeUI;

    public int strikes = 0;

    public void AddStrike()
    {
        strikes += 1;
    }

    private void FixedUpdate()
    {
        StrikeUI.GetComponent<TextMeshProUGUI>().text = $"Strike: {strikes}";
    }

    private void Awake()
    {
        productList = new List<Product>();
        foreach(GameObject productObj in products)
        {
            productList.Add(productObj.GetComponent<Product>());
        }
    }
    private void Start()
    {
        FixChancesOfInteractionOccuring();

        StartCoroutine(CustomerSpawningCoroutine());
    }
    /// <summary>
    /// This method makes sure that the values of ChancesOfInteractionOccuring list stays within 0-100 range (in case somebody inputs wrong values)
    /// </summary>
    private void FixChancesOfInteractionOccuring()
    {
        for (int i = 0; i < chancesOfInteractionFailuresOccuring.Count; i++)
        {
            if (chancesOfInteractionFailuresOccuring[i] < 0)
            {
                chancesOfInteractionFailuresOccuring[i] = 0;
            }
            else if (chancesOfInteractionFailuresOccuring[i] > 100)
            {
                chancesOfInteractionFailuresOccuring[i] = 100;
            }
        }
    }

    IEnumerator CustomerSpawningCoroutine()
    {
        int id = 0;
        while (spawnCustomers)
        {
            GameObject temp = Instantiate(customerPrefabs[0], customerSpawnPoint);
            temp.transform.localPosition = Vector3.zero;
            temp.name = "Customer"+id;
            id++;
            yield return new WaitForSeconds(Random.Range(minCustomerSpawnInterval, maxCustomerSpawnInterval));
        }
    }

}
