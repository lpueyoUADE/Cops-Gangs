using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Far")]
    [SerializeField] float farSize;
    [SerializeField] Vector3 farOffset;

    [Header("Near")]
    [SerializeField] float nearSize;
    [SerializeField] Vector3 nearOffset;

    [Header("Speeds")]
    [SerializeField] float speedSize;
    [SerializeField] float speedOffset;

    float targetSize;
    Vector3 targetOffset;

    private void Update()
    {
        targetSize = nearSize;
        targetOffset = nearOffset;

        if (Input.GetMouseButton(1))
        {
            targetSize = farSize;
            targetOffset = farOffset;
        } 
    }

    void FixedUpdate()
    {
        if (target == null)
            return;

        float fixedDeltaTime = Time.fixedDeltaTime;
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetSize, speedSize * fixedDeltaTime);
        this.transform.position = Vector3.MoveTowards(this.transform.position, target.position + targetOffset, speedOffset * fixedDeltaTime);
    }
}