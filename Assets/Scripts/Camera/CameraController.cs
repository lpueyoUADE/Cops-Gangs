using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Far FOV")]
    [SerializeField] float farFOV;
    [SerializeField] Vector3 farRotation;
    [SerializeField] Vector3 farOffset;

    [Header("Near FOV")]
    [SerializeField] float nearFOV;
    [SerializeField] Vector3 nearRotation;
    [SerializeField] Vector3 nearOffset;

    [Header("Speeds")]
    [SerializeField] float speedFOV;
    [SerializeField] float speedAngle;
    [SerializeField] float speedOffset;

    float targetFOV;
    Vector3 targetRotation;
    Vector3 targetOffset;


    private void Update()
    {
        targetFOV = farFOV;
        targetRotation = farRotation;
        targetOffset = farOffset;

        if (Input.GetMouseButton(1))
        {
            targetFOV = nearFOV;
            targetRotation = nearRotation;
            targetOffset = nearOffset;
        } 
    }

    void FixedUpdate()
    {
        if (target == null)
            return;

        float fixedDeltaTime = Time.fixedDeltaTime;
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, targetFOV, speedFOV * fixedDeltaTime);
        this.transform.eulerAngles = Vector3.MoveTowards(this.transform.eulerAngles, targetRotation, speedAngle * fixedDeltaTime);
        this.transform.position = Vector3.MoveTowards(this.transform.position, target.position + targetOffset, speedOffset * fixedDeltaTime);
    }
}