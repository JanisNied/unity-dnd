using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float maxZoom = 300f, minZoom = 150f, panSpeed = 6f, zoomSpeed = 50f;
    Vector3 bottomLeft, topRight;
    float[] cameraBounds = new float[4]; //maxX, maxY, minX, minY
    float x, y;
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        topRight = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, -transform.position.z));
        bottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, -transform.position.z));
        cameraBounds[0] = topRight.x; // MAX X
        cameraBounds[1] = topRight.y; // MAX Y
        cameraBounds[2] = bottomLeft.x; // MIN X
        cameraBounds[3] = bottomLeft.y; // MIN Y
    }
    private void Update()
    {
        x = Input.GetAxis("Mouse X") * panSpeed;
        y = Input.GetAxis("Mouse Y") * panSpeed;
        transform.Translate(x, y, 0);

        if (Input.GetAxis("Mouse ScrollWheel") > 0f && cam.orthographicSize > minZoom) {
            cam.orthographicSize = cam.orthographicSize - zoomSpeed;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && cam.orthographicSize < maxZoom)
        {
            cam.orthographicSize = cam.orthographicSize + zoomSpeed;
        }
        topRight = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, -transform.position.z));
        bottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, -transform.position.z));
        if (topRight.x > cameraBounds[0])
        {
            transform.position = new Vector3(transform.position.x - (topRight.x - cameraBounds[0]), transform.position.y, transform.position.z);
        }
        if (topRight.y > cameraBounds[1])
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (topRight.y - cameraBounds[1]), transform.position.z);
        }
        if (bottomLeft.x < cameraBounds[2])
        {
            transform.position = new Vector3(transform.position.x + (cameraBounds[2] - bottomLeft.x), transform.position.y, transform.position.z);
        }
        if (bottomLeft.y < cameraBounds[3])
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (cameraBounds[3] - bottomLeft.y), transform.position.z);
        }
    }
}
