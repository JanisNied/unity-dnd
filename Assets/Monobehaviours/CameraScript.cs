using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float maxZoom = 300f, minZoom = 150f, panSpeed = 6f;
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


    }
}
