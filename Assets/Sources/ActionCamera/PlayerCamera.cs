using Newtonsoft.Json.Linq;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private GameObject owner;

    private Vector3 dynamicOffsetCamera = Vector3.zero;

    private Camera mainCamera;

    private const float angleCameraX = 60;
    private const float angleCameraY = -45;
    private const float heightCamera = 8.0f;

    private const int initialOffsetCameraX = 3;
    private const int initialOffsetCameraZ = 3;

    private int difDistanceX = 3;
    private int difDistanceZ = 3;

    private const float upperBoundView = 1;
    private const float lowerBoundView = 10;

    private const float speed = 10.0f;

    private readonly Vector3 forwardVector = new Vector3(-1.0f, 0.0f, 1.0f);

    private int floorLayer = 6;

    void Start()
    {
		mainCamera = Camera.main;
		SetInitialPositionCamera();
    }

    void FixedUpdate()
    {
        if (owner == null)
        {
            return;
        }
        CameraLag();
    }

    private void CameraLag()
    {
        var aimPosition = GetCursorPosition();
        var startPosition = owner.transform.position;
        var difDistanceCameraY = mainCamera.transform.position.y - owner.transform.position.y;
        var distances = aimPosition - startPosition;

        aimPosition.y = startPosition.y;

        int angle = Mathf.Abs((int)Vector3.SignedAngle(forwardVector, distances, Vector3.up));
        float avgValue = (Mathf.Abs(distances.x) + Mathf.Abs(distances.z)) / 2;

        var coef = MapValue(angle, 0, 180, upperBoundView, lowerBoundView);

        dynamicOffsetCamera.x = Mathf.Clamp(distances.x / difDistanceX, -coef, coef);
        dynamicOffsetCamera.z = Mathf.Clamp(distances.z / difDistanceZ, -coef, coef);
        dynamicOffsetCamera.y = Mathf.Sqrt(avgValue);
        
        var startCameraPosition = mainCamera.transform.position;

        var endCameraPosition = new Vector3(owner.transform.position.x + initialOffsetCameraX,
            mainCamera.transform.position.y + (heightCamera - difDistanceCameraY),
            owner.transform.position.z - initialOffsetCameraZ) + dynamicOffsetCamera;
        
        mainCamera.transform.position = Vector3.Slerp(startCameraPosition, endCameraPosition, speed * Time.fixedDeltaTime);
    }

    private void SetInitialPositionCamera()
    {
        mainCamera.transform.position = new Vector3(0.0f, heightCamera, 0.0f);
        mainCamera.transform.rotation = Quaternion.Euler(angleCameraX, angleCameraY, 0);
    }

    public void SetOwner(GameObject owner)
    {
        this.owner = owner;
    }

    public Vector3 GetCursorPosition()
    {
        Ray rayFromCursor = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        int layerMask = 1 << floorLayer;
        Physics.Raycast(rayFromCursor, out raycastHit, int.MaxValue, layerMask);
        return new Vector3(raycastHit.point.x, owner == null ? 0.0f : owner.transform.position.y, raycastHit.point.z);
    }

    private float MapValue(float value, float leftMin, float leftMax, float rightMin, float rightMax)
    {
        //Figure out how 'wide' each range is
        var leftSpan = leftMax - leftMin;
        var rightSpan = rightMax - rightMin;

        //Convert the left range into a 0-1 range (float)
        var valueScaled = (float)(value - leftMin) / (float)(leftSpan);

        //Convert the 0-1 range into a value in the right range.
        return rightMin + (valueScaled * rightSpan);
    }
}
