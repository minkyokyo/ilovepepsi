using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskType
{
    TakeOrder,
    MakeProduct,
    ServeProduct
}



public class WorkerManager : MonoBehaviour
{
    public Transform spawnTransform;
    public Transform storePosition;
    public Queue<Order> orderBoard;
    public List<Worker> workers;

    private void Awake()
    {
        orderBoard = new Queue<Order>();
    }
    private void Start()
    {
        Stand.Instance.onCustomerCome.AddListener(assignTakeOrderTask);       
    }

    private void Update()
    {
        if(orderBoard.Count==0)
        {
            return;
        }
        assginServeTask();
    }

    public void assignTakeOrderTask()
    {
        Worker worker = CallWorker();
        if (worker == null)
        {
            return;
        }
        StartCoroutine(worker.TakeOrder());
    }

    public void assginServeTask()
    {
        Worker worker = CallWorker();
        if (worker == null)
        {
            return;
        }
        StartCoroutine(worker.Serve());
    }

    public Worker CallWorker()
    {
        foreach(Worker worker in workers)
        {
            if(worker.state == WorkerState.Idle)
            {
                return worker;
            }
        }
        return null;
    }
}
