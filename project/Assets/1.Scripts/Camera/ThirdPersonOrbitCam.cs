using UnityEngine;
/// <summary>
/// ī�޶�κ��� ��ġ ������ ����, �Ǻ� ������ ����
/// ��ġ ������ ���ʹ� �浹 ó����
/// �Ǻ� ������ ���ʹ� �ü� �̵���
/// �浹 üũ: ���� �浹 üũ ��� (ĳ���ͷκ��� ī�޶�, ī�޶�κ��� ĳ����)
/// ��� �ݵ��� ���� ���
/// FOV ���� ���
/// </summary>
[RequireComponent(typeof(Camera))]
public class ThirdPersonOrbitCam : MonoBehaviour
{
    public Transform player;
    public Vector3 pivotOffset = new Vector3(0.0f, 1.0f, 0.0f);
    public Vector3 camOffset = new Vector3(0.4f, 0.5f, -2.0f);

    public float smooth = 10f;  // ī�޶� ���� �ӵ�
    public float horizontalAimingSpeed = 6.0f;  // ���� ȸ�� �ӵ�
    public float verticalAimingSpeed = 6.0f;    // ���� ȸ�� �ӵ�
    public float maxVerticalAngle = 30.0f;  // ī�޶��� ���� �ִ� ����
    public float minVerticalAngle = -60.0f; // ī�޶��� ���� �ּ� ����
    public float recoilAngleBounce = 5.0f;  // ��� �ݵ� �ٿ ��
    private float angleH = 0.0f;    // ���콺 �̵��� ���� ī�޶� ���� �̵� ��ġ
    private float angleV = 0.0f;    // ���콺 �̵��� ���� ī�޶� ���� �̵� ��ġ
    private Transform cameraTransform;  // Ʈ������ ĳ��
    private Camera myCamera;
    private Vector3 relCameraPos;   // �÷��̾�κ��� ī�޶������ ����
    private float relCameraPosMag;  // �÷��̾�κ��� ī�޶������ �Ÿ�
    private Vector3 smoothPivotOffset;
    private Vector3 smoothCamOffset;
    private Vector3 targetPivotOffset;
    private Vector3 targetCamOffset;
    private float defaultFOV;   // �⺻ �þ� ��
    private float targetFOV;    // Ÿ�� �þ� ��
    private float targetMaxVerticleAngle;   // ī�޶� ���� �ִ� ����
    private float recoilAngle = 0f; // ��� �ݵ� ����

    public float GetH
    {
        get
        {
            return angleH;
        }
    }

    private void Awake()
    {
        // ĳ��
        cameraTransform = transform;
        myCamera = cameraTransform.GetComponent<Camera>();
        // ī�޶� �⺻ ������ ����
        cameraTransform.position = player.position + Quaternion.identity * pivotOffset + Quaternion.identity * camOffset;
        cameraTransform.rotation = Quaternion.identity;

        // ī�޶�� �÷��̾�� ��� ����, �浹üũ�� ���
        relCameraPos = cameraTransform.position - player.position;
        relCameraPosMag = relCameraPos.magnitude - 0.5f;

        // �⺻ ����
        smoothPivotOffset = pivotOffset;
        smoothCamOffset = camOffset;
        defaultFOV = myCamera.fieldOfView;
        angleH = player.eulerAngles.y;

        ResetTargetOffsets();
        ResetFOV();
        ResetMaxVerticalAngle();
    }

    public void ResetTargetOffsets()
    {
        targetPivotOffset = pivotOffset;
        targetCamOffset = camOffset;
    }

    public void ResetFOV()
    {
        this.targetFOV = defaultFOV;
    }

    public void ResetMaxVerticalAngle()
    {
        targetMaxVerticleAngle = maxVerticalAngle;
    }

    public void BounceVertical(float degree)
    {
        recoilAngle = degree;
    }

    public void SetTargetOffset(Vector3 newPivotOffset, Vector3 newCamOffset)
    {
        targetPivotOffset = newPivotOffset;
        targetCamOffset = newCamOffset;
    }

    public void SetFOV(float customFOV)
    {
        this.targetFOV = customFOV;
    }

    bool ViewingPosCheck(Vector3 checkPos, float deltaPlayerHeight)
    {
        Vector3 target = player.position + (Vector3.up * deltaPlayerHeight);

        if (Physics.SphereCast(checkPos, 0.2f, target - checkPos, out RaycastHit hit, relCameraPosMag))
        {
            if (hit.transform != player && !hit.transform.GetComponent<Collider>().isTrigger)
            {
                return false;
            }
        }

        return true;
    }

    bool ReverseViewingPosCheck(Vector3 checkPos, float deltaPlayerHeight, float maxDistance)
    {
        Vector3 origin = player.position + (Vector3.up * deltaPlayerHeight);

        if (Physics.SphereCast(origin, 0.2f, checkPos - origin, out RaycastHit hit, maxDistance))
        {
            if (hit.transform != player && hit.transform != transform && !hit.transform.GetComponent<Collider>().isTrigger)
            {
                return false;
            }
        }

        return true;
    }

    bool DoubleViewingPosCheck(Vector3 checkPos, float offset)
    {
        float playerFocusHeight = player.GetComponent<CapsuleCollider>().height * 0.75f;

        return ViewingPosCheck(checkPos, playerFocusHeight) && ReverseViewingPosCheck(checkPos, playerFocusHeight, offset);
    }

    private void Update()
    {
        // ���콺 �̵� ��
        angleH += Mathf.Clamp(Input.GetAxis("Mouse X"), -1f, 1f) * horizontalAimingSpeed;
        angleV += Mathf.Clamp(Input.GetAxis("Mouse Y"), -1f, 1f) * verticalAimingSpeed;
        // ���� �̵� ����
        angleV = Mathf.Clamp(angleV, minVerticalAngle, targetMaxVerticleAngle);
        // ���� ī�޶� �ٿ
        angleV = Mathf.LerpAngle(angleV, angleV + recoilAngle, 10f * Time.deltaTime);

        // ī�޶� ȸ��
        Quaternion camYRotation = Quaternion.Euler(0.0f, angleH, 0.0f);
        Quaternion aimRotation = Quaternion.Euler(-angleV, angleH, 0.0f);
        cameraTransform.rotation = aimRotation;

        // set FOV
        myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, targetFOV, Time.deltaTime);

        Vector3 baseTempPosition = player.position + camYRotation * targetPivotOffset;
        Vector3 noCollisionOffset = targetCamOffset;    // ������ �� ī�޶��� ������ ��

        for (float zOffset = targetCamOffset.z; zOffset <= 0f; zOffset += 0.5f)
        {
            noCollisionOffset.z = zOffset;

            if (DoubleViewingPosCheck(baseTempPosition + aimRotation * noCollisionOffset, Mathf.Abs(zOffset)) || zOffset == 0f)
            {
                break;
            }
        }

        //Reposition Camera
        smoothPivotOffset = Vector3.Lerp(smoothPivotOffset, targetPivotOffset, smooth * Time.deltaTime);
        smoothCamOffset = Vector3.Lerp(smoothCamOffset, noCollisionOffset, smooth * Time.deltaTime);

        cameraTransform.position = player.position + camYRotation * smoothPivotOffset + aimRotation * smoothCamOffset;

        if (recoilAngle > 0.0f)
        {
            recoilAngle -= recoilAngleBounce * Time.deltaTime;
        }
        else if (recoilAngle < 0.0f)
        {
            recoilAngle += recoilAngleBounce * Time.deltaTime;
        }
    }

    public float GetCurrentPivotMagnitude(Vector3 finalPivotOffset)
    {
        return Mathf.Abs((finalPivotOffset - smoothPivotOffset).magnitude);
    }
}
