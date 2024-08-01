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
             "index 1 - chance of incorrect Quantity occuring\n" +
             "DO NOT CHANGE THE ORDER!!!")]
    public List<byte> chancesOfInteractionFailuresOccuring;


    public List<GameObject> products;
    [HideInInspector] public List<Product> productList;

    #region Customers AI

    [Tooltip("Make sure that the index of product's shelf matches the enum to int cast value of product's type (the way I take the transform value is by casting product's enum type to int as an index of this List)")]
    public List<ProductShelf> productShelves;

    [Tooltip("Minimum time in seconds for customers to go to next product shelf")]
    public int minCustomerShelfWait;

    [Tooltip("Maximum time in seconds for customers to go to next product shelf")]
    public int maxCustomerShelfWait;

    [Tooltip("Make sure to not add self service queue to this list, since that has it's own field.")]
    public List<CheckoutQueue> Queues;

    public SelfServiceCheckoutQueue selfServiceQueue;
    public Transform customerExitPoint;

    #endregion

    #region Spawning Customers

    public bool spawnCustomers;

    [Tooltip("Minimum time in seconds for customers to spawn")]
    public int minCustomerSpawnInterval;

    [Tooltip("Maximum time in seconds for customers to spawn")]
    public int maxCustomerSpawnInterval;

    [SerializeField] List<GameObject> customerPrefabs;
    [SerializeField] Transform customerSpawnPoint;

    #endregion

    #region DayTime

    //In the Future you can use a coroutine to measure time, and whenever you want to stop it you can suspend that coroutine. For now I'll just use a bool but in the future try thinking about a coroutine
    public bool isDayTimeRunning;

    

    [field: SerializeField] public int FullDayTime { get; private set; }
    [field: SerializeField] public int DayTimeLeft { get; private set; }

    public Action OnSecondPassed; 

    #endregion

    #region Strikes

    public int strikes = 0;

    #endregion



    private void Awake()
    {
        strikes = 0;
        UIManager.Instance.UpdateStrikeUI(strikes);

        DayTimeLeft = FullDayTime;
        UIManager.Instance.UpdateClockUI(DayTimeLeft);

        Debug.Log("Why the fuck do you use two lists??? every object in productList has a reference to it's GameObject");
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
        StartCoroutine(UpdateDayTime());
    }


    private IEnumerator UpdateDayTime()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1);

            //if isDayTimeRunning == false
            if (!isDayTimeRunning)
            {
                continue;
            }

            DayTimeLeft--;
            UIManager.Instance.UpdateClockUI(DayTimeLeft);
            OnSecondPassed?.Invoke();

            if (DayTimeLeft <= 0)
            {
                EndDay();
            }
        }
    }

    

    private void EndDay()
    {
        spawnCustomers = false;
        isDayTimeRunning = false;

        UIManager.Instance.EndDay();
        
    }

    #region strikes methods

    public void AddStrike()
    {
        strikes += 1;
        UIManager.Instance.UpdateStrikeUI(strikes);
    }

    

    #endregion

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