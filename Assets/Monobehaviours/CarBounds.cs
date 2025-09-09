using UnityEngine;

public class CarBounds : MonoBehaviour
{
    [HideInInspector]
    public Vector3 screenPoint, offset;
    private float minX, maxX, minY, maxY;
    public float padding = 0.02f;
    private void Awake()
    {
        Vector3 lowerLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 upperRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        float newWidth = (upperRight.x - lowerLeft.x) * padding;
        float newHeight = (upperRight.y - lowerLeft.y) * padding;

        minX = lowerLeft.x + newWidth;
        maxX = lowerLeft.x - newWidth;
        minY = lowerLeft.y + newHeight;
        minY = lowerLeft.y - newHeight;
    }

    public Vector2 GetClampedPosition (Vector3 pos)
    {
        return new Vector2(
            Mathf.Clamp(pos.x, minX, maxX), Mathf.Clamp(pos.y, minY, maxY)
            );
    }
}
