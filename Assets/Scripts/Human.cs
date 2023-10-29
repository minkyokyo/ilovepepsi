using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
머리 위에 아이콘 뜹니다.
움직입니다.
목적지에 도착하면 이벤트가 발생합니다.
 */
public enum HumanMoveState
{
    arrived,
    moving
}
public abstract class Human : MonoBehaviour
{
    public Transform headIconAnchor;
    public float speed;
    public HumanMoveState humanMoveState;

    public UnityEvent onHumanArrived;
    public abstract void Arrived();
    public virtual IEnumerator Move(Vector3 start, Vector3 end)
    {
        humanMoveState = HumanMoveState.moving;
        while (true)
        {
            if(gameObject.transform.position == end)
            {
                humanMoveState = HumanMoveState.arrived;
                onHumanArrived.Invoke();
                break;
            }
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, end, speed * Time.deltaTime);
            yield return null;
        }
    }
}
