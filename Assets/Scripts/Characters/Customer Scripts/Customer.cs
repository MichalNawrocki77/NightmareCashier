using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Scripts.Enums;

using UnityEngine;
using UnityEngine.AI;

using Random = UnityEngine.Random;

public class Customer : MonoBehaviour, IDialogueable
{
    Animator animator;

    [HideInInspector] public Dictionary<Product, int> products;

    [Tooltip("This sprite will be used in dialgue's instead of the in game one")]

    #region IDialogueable members

    [SerializeField] Sprite dialogueSprite; 
    Sprite IDialogueable.DialogueSprite
    {
        get => dialogueSprite;
        set => dialogueSprite = value;
    }

    [SerializeField] TextAsset inkStory;
    TextAsset IDialogueable.InkStory
    {
        get => inkStory;
        set => inkStory = value;
    
    }
    #endregion

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

        animator = GetComponent<Animator>();

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
        products = new Dictionary<Product, int>();
        for(int i=0; i<Random.Range(3,7); i++)
        {
            Product product = DayManager.Instance.productList[
                Random.Range(0, DayManager.Instance.products.Count)
                ];
            AddProductToProductsDictionary(product);
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

    void AddProductToProductsDictionary(Product product)
    {
        if(products.ContainsKey(product))
        {
            products[product] += 1;
        }
        else
        {
            products.Add(product, 1);
        }
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
        checkout.StartInteraction();
        agent.SetDestination(checkout.navMeshDestination.position);
        sm.ChangeState(goingToCheckoutState);
    }
    #region animations
    public void SetShowingFailureIndicator(bool isShowing)
    {
        animator.SetBool("IsFailurePlaying", isShowing);
    }

    #endregion
}
