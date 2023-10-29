using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerGenerator : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform spawnTransform,destroyTransform,storeTransform;
    public Transform customerHolder;

    public Queue<Customer> GeneratedCustomers;
   // public Button button;
    private void Awake()
    {
        GeneratedCustomers = new Queue<Customer>();        
    }

    void Start()
    {
        GenerateCustomer();    
    }

    public void GenerateCustomer()
    {
        Customer customer =  Instantiate(customerPrefab,customerHolder).GetComponent<Customer>();
        int customerSlotNumber = Stand.Instance.GetEmptySlotIndex();
        customer.Initiallize(customerSlotNumber, spawnTransform.position, destroyTransform.position);
        customer.OnCustomerLeave.AddListener(GenerateCustomer);
       // button.onClick.AddListener(customer.LeaveStore);
        GeneratedCustomers.Enqueue(customer);
    }
}
