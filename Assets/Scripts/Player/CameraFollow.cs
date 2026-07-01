using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    [Header("Configurações")]
    [SerializeField] private float smoothTime = 0.25f;

    private Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 desiredPosition = target.position + offset;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            smoothTime
        );
    }
}