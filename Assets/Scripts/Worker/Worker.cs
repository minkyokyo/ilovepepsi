using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum WorkerState
{
    Idle,
    TakingOrder,
    Serving
}

public class Worker : Human
{
    public Vector3 spawnPosition;
    public Vector3 StandPosition;
    public Vector3 FarmPosition;

    public Queue<TaskType> tasks;
    public WorkerState state;
    public WorkerManager workerManager;

    public int orderNumber;

    public bool CanServe=true;
    private void Start()
    {
        base.onHumanArrived.AddListener(Arrived);
    }
    public void Initiallize(Vector3 spawnPosition,Vector3 FarmPosition, WorkerManager workerManager)
    {
        gameObject.transform.position = spawnPosition;
        this.FarmPosition = FarmPosition;
        this.workerManager = workerManager;
        state = WorkerState.Idle;
        CanServe = true;
    }
    public IEnumerator TakeOrder()
    {
        state = WorkerState.TakingOrder;
        // 여기서 주문 안 받은 손님을 확인하고 손님을 받습니다.
        while (true)
        {
            if (Stand.Instance.customers.Count == 0)
            {
                break;
            }
            var customer = Stand.Instance.customers.Dequeue();
            StandPosition = Stand.Instance.sellerPos[customer.orderNumber].position;
           yield return StartCoroutine(base.Move(gameObject.transform.position, StandPosition));
            for (int i = 0; i < customer.orderAmount; i++)
            {
                //TaskBoard에 넣습니다.
                workerManager.orderBoard.Enqueue(customer.order);
                Debug.Log("Amount :"+customer.order.orderAmount);
                Debug.Log("Number :"+customer.order.orderNumber);
                Debug.Log("Type :" +customer.order.orderType);
            }
        }
        state = WorkerState.Idle;
    }

    public IEnumerator Serve()
    {
        state = WorkerState.Serving;
        if (CanServe)
        {
            Order order=workerManager.orderBoard.Dequeue();
            orderNumber = order.orderNumber;
            yield return StartCoroutine(base.Move(gameObject.transform.position, FarmPosition));
            // 여기서 밀 챙겨오고,
            yield return StartCoroutine(base.Move(gameObject.transform.position, Stand.Instance.sellerPos[orderNumber].position));
            // 전달
            DeliverProduct(1,orderNumber);
        }
        state = WorkerState.Idle;
    }
    
    public void DeliverProduct(int deliverAmount, int orderNubmer)
    {
        Stand.Instance.customerList[orderNumber].ReceiveProduct(deliverAmount);
        Debug.Log("Deliver");
    }
    public override void Arrived()
    {
        return;
    }
}
