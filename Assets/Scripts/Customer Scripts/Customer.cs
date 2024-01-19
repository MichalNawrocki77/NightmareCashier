using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Scripts.Enums;

using UnityEngine;
using UnityEngine.AI;

using Random = UnityEngine.Random;

public class Customer : MonoBehaviour
{
    [HideInInspector] public List<Product> products;

    #region Navigation
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public int minSecondsToGetNextProduct;
    [HideInInspector] public int maxSecondsToGetNextProduct;

    //In the future There will be another way of determining the checkout to use
    [HideInInspector] public Checkout checkout;

    public event Action<Collider2D> queueManagerEntered;
    #endregion

    #region StateMachine

    public StateMachine sm;


    public CollectingProductsState collectingProductsState;
    public GoingToQueueState goingToQueueState;
    public AtQueueState atQueueState;
    public GoingToCheckoutState goingToCheckoutState;
    public GoingHomeState goingHomeState;

    #endregion

    private void Awake()
    {
        sm = new StateMachine();

        collectingProductsState = new CollectingProductsState(this,sm);
        goingToQueueState = new GoingToQueueState(this, sm);
        atQueueState = new AtQueueState(this, sm);
        goingToCheckoutState = new GoingToCheckoutState(this, sm);
        goingHomeState = new GoingHomeState(this, sm);
        
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        InitializeItems();
        //Usually you call this method in awake, but since the 1st state's enter()'s InitializeShelves() Relies on the products List not being null, I need to call InitializeShelves() AFTER calling InitializeItems()
        sm.Initialize(collectingProductsState);
    }

    // Update is called once per frame
    void Update()
    {
        sm.currentState.LogicUpdate();
    }

    void InitializeItems()
    {
        products = new List<Product>();
        for(int i=0; i<Random.Range(3,7); i++)
        {
            products.Add(DayManager.Instance.productList[
                Random.Range(0,DayManager.Instance.products.Count)]
                );
        }
    }
    public bool IsDestinationReached()
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("QueueManager"))
        {
            queueManagerEntered?.Invoke(collision);
        }
    }
    //I coudln't figure out a better way to get acces to Destroy() in GoingHomeState xD
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    public void AssignCustomerToCheckout(Checkout checkout)
    {
        checkout.CustomerCurrent = this;
        this.checkout = checkout;
        agent.SetDestination(checkout.navMeshDestination.position);
        sm.ChangeState(goingToCheckoutState);
    }

}
