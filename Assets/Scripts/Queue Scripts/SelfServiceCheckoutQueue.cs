using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class SelfServiceCheckoutQueue : MonoBehaviour
{
    [HideInInspector] public List<Transform> queuePositions;
    [HideInInspector] public List<Customer> customersInQueue;

    [SerializeField] List<Checkout> selfServiceCheckouts;
    private void Awake()
    {
        customersInQueue = new List<Customer>();
    }
    void Start()
    {        
        queuePositions = transform.GetComponentsInChildren<Transform>().ToList();
        //GetComponentsInChildren() also fetches the component on the object which called this method, so in this case I end up with a list of QueueManager transform and then all the transforms of positions in queue, so i have to remove the QueueManager from the list
        queuePositions.RemoveAt(0);

        foreach (Checkout checkout in selfServiceCheckouts)
        {
            checkout.customerLeft += SendFirstCustomerToCheckout;
        }
    }
    void SendFirstCustomerToCheckout(Checkout sender)
    {
        if (customersInQueue.Count == 0) return;

        customersInQueue[0].AssignCustomerToCheckout(sender);
        customersInQueue.RemoveAt(0);
        UpdateCustomersPositionInQueue();
    }
    public Checkout GetAvailableSelfServiceCheckout()
    {
        for(int i = 0; i < selfServiceCheckouts.Count; i++)
        {
            if (selfServiceCheckouts[i].CustomerCurrent is null)
            {
                return selfServiceCheckouts[i];
            }
        }
        return null;
    }

    public void UpdateCustomersPositionInQueue()
    {
        foreach(Customer customer in customersInQueue)
        {
            //This will be called whenever a customer has left the queue to go to the checkout. Since Leaving the queue is calling customersInQueue.removeAt() (and this fucntion automatically moves all the latter customers indexes by -1 in the list) I just need to move them to the position of their new position that is stored in the queuePosition[customer's Index After RemoveAt() call]
            customer.agent.SetDestination(queuePositions[customersInQueue.IndexOf(customer)].position);
        }
    }
}
