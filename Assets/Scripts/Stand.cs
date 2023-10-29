using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stand : MonoBehaviour
{
    public CustomerGenerator customerGenerator;
    public WorkerGenerator workerGenerator;

    public int maxSlotCount;
    public int currentSlotCount;
    public int slotCount;
    public Transform[] customerPos;
    public Transform[] sellerPos;
    public static Stand Instance { get; private set; }
    public Queue<Customer> customers;
    public bool[] fullSlots;
    public UnityEvent onCustomerCome;

    public List<Customer> customerList;

    private void Awake()
    {
        Instance = this;
        customers = new Queue<Customer>();
        //customerList = new List<Customer>(slotCount);
       fullSlots = new bool[slotCount];

    }

    public void RegisterCustomer(Customer customer)
    {
        Debug.Log(customer.orderNumber);
        customerList[customer.orderNumber] = customer;
        customers.Enqueue(customer);
        onCustomerCome.Invoke();
    }

    public int GetEmptySlotIndex()
    {
        for(int i=0;i< slotCount; i++)
        {
            if(fullSlots[i] == false)
            {
                fullSlots[i] = true;
                return i;
            }
        }
        return -1;
    }
}
