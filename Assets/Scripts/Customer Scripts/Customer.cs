using System.Collections;
using System.Collections.Generic;

using Assets.Scripts.Enums;

using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    public List<ProductType> products;

    #region Navigation

    NavMeshAgent agent;
    Queue<Transform> productShelvesQueue;
    [SerializeField] int minSecondsToGetNextProduct;
    [SerializeField] int maxSecondsToGetNextProduct;
    bool isProductsCollected;
    Coroutine currentNavigationCoroutine;

    //In the future There will be another way of determining the checkout to use
    [SerializeField] Transform mockCheckoutTransform;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        InitializeItems();
        InitializeShelves();

        SetNextDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDestinationReached())
        {
            if(currentNavigationCoroutine is null)
            {
                currentNavigationCoroutine = StartCoroutine(WaitForNextDestination());
            }
            
        }
    }

    void InitializeItems()
    {
        products = new List<ProductType>();
        for(int i=0; i<Random.Range(3,7); i++)
        {
            products.Add(DayManager.Instance.productTypesList[
                Random.Range(0,DayManager.Instance.products.Count)]
                );
        }
    }
    void InitializeShelves()
    {
        productShelvesQueue = new Queue<Transform>();
        foreach (ProductType item in products)
        {
            Debug.Log(item);
            if (productShelvesQueue.Contains(DayManager.Instance.productShelves[(int)item]))
            {
                continue;
            }
            productShelvesQueue.Enqueue(DayManager.Instance.productShelves[(int)item]);
        }
    }
    void SetNextDestination()
    {
        //Replace this code with an actual state machine that will keep track of whether the customer is in a state of collecting items, waiting in queue for checkout, next to a checkout, going home
        if(productShelvesQueue.Count == 0)
        {
            agent.SetDestination(mockCheckoutTransform.position);
        }
        agent.SetDestination(productShelvesQueue.Dequeue().position);
    }
    
    IEnumerator WaitForNextDestination()
    {
        Debug.Log("Zaczynam czekaæ!!!");
        yield return new WaitForSeconds(Random.Range(
            DayManager.Instance.minCustomerWait,DayManager.Instance.maxCustomerWait));
        Debug.Log("Skonczono czekac!!!");
        SetNextDestination();
        currentNavigationCoroutine = null;
    }
    bool IsDestinationReached()
    {
        if(agent.remainingDistance < agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                return true;
            }
        }
        return false;
    }
}
