using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanControl : MonoBehaviour
{
    public Button customer, worker;
    public CustomerGenerator customerGenerator;
    public WorkerGenerator workerGenerator;

    private void Start()
    {
        customer.onClick.AddListener(AddCustomer);
        worker.onClick.AddListener(Addworker);
    }

    void AddCustomer()
    {
        customerGenerator.GenerateCustomer();
    }

    void Addworker()
    {
        workerGenerator.GenerateWorker();
    }
}
