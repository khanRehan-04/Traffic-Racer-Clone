using UnityEngine;
using DG.Tweening;

public class TrafficCar : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float laneOffset = 2f;
    public LayerMask trafficCarLayer;

    private bool isMovingAside = false;

    private void Start()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        transform.DOMoveZ(transform.position.z + 1000f, 1000f / moveSpeed).SetEase(Ease.Linear).OnComplete(() =>
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -1000f);
            MoveForward();
        });
    }

    public void MoveAside()
    {
        if (!isMovingAside)
        {
            isMovingAside = true;

            RaycastHit hit;
            if (!Physics.Raycast(transform.position, -transform.right, out hit, laneOffset, trafficCarLayer))
            {
                transform.DOMoveX(transform.position.x - laneOffset, 1f).SetEase(Ease.Linear).OnComplete(() => isMovingAside = false);
            }
            else if (!Physics.Raycast(transform.position, transform.right, out hit, laneOffset, trafficCarLayer))
            {
                transform.DOMoveX(transform.position.x + laneOffset, 1f).SetEase(Ease.Linear).OnComplete(() => isMovingAside = false);
            }
            else
            {
                isMovingAside = false;
                Debug.Log("Both lanes are blocked, cannot move aside.");
            }
        }
    }
}
