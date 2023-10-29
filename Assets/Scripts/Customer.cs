using UnityEngine;
using UnityEngine.Events;

public enum CustomerState
{
    waiting,
    comming,
    leaving
}
public class Customer : Human
{
    public Vector3 enterPosition;
    public Vector3 exitPosition;
    public Vector3 storePosition;

    //public int slotNumber;
    public int orderAmount;
    public int orderType;
    public int orderNumber;
    public CustomerState customerState = CustomerState.comming;
    public Order order;
   

    public UnityEvent OnCustomerLeave;

    private void Start()
    {
        base.onHumanArrived.AddListener(Arrived);
    }
    public void Initiallize(int slotNumber,Vector3 enterPos,Vector3 exitPos)
    {
        enterPosition = enterPos; exitPosition = exitPos; storePosition = Stand.Instance.customerPos[slotNumber].position;
        orderNumber = slotNumber;
        gameObject.transform.position = enterPos;
        SetCustomerOrder();
        GoToStore();
    }
    private void SetCustomerOrder()
    {
        orderAmount = Random.Range(1, 4);
        orderType = Random.Range(1, 4);
        order = new Order(orderNumber,orderType,orderAmount);
    }
    public void GoToStore()
    {
        customerState = CustomerState.comming;
        StopAllCoroutines();
        StartCoroutine(base.Move(enterPosition, storePosition));
    }
    public void Order()
    {
       //ui에서  아이콘 뜹니다.
    }
    public void ReceiveProduct(int deliverdOrder)
    {
        orderAmount -= 1;
        if (orderAmount==0)
        {
            LeaveStore();
        }
    }
    public void LeaveStore()
    {
        customerState = CustomerState.leaving;
        OnCustomerLeave.Invoke();
        Stand.Instance.fullSlots[orderNumber] = false;
        StopAllCoroutines();
        StartCoroutine(base.Move(gameObject.transform.position, exitPosition));
    }
    public override void Arrived()
    {
        if(customerState == CustomerState.leaving)
        {
            Destroy(gameObject);
        }
        else if(customerState == CustomerState.comming)
        {
            customerState = CustomerState.waiting;
            Stand.Instance.RegisterCustomer(this);
        }
    }
}
