using Unity.VisualScripting;
using UnityEngine;

public class CarTransfigurationModule : MonoBehaviour
{
    public ObjScript objectScript;

    public float speed = 1f;

    // 60fps = 0.0016ms
    void Update()
    {
        if (objectScript.lastDragged != null)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                objectScript.lastDragged.GetComponent<RectTransform>().transform.Rotate(0, 0, Time.deltaTime * 10f * speed);
            }
            if (Input.GetKey(KeyCode.X))
            {
                objectScript.lastDragged.GetComponent<RectTransform>().transform.Rotate(0, 0, Time.deltaTime * -10f * speed);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                objectScript.lastDragged.GetComponent<RectTransform>().transform.localScale = Scale(objectScript.lastDragged.GetComponent<RectTransform>(), 0.001f, true);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                objectScript.lastDragged.GetComponent<RectTransform>().transform.localScale = Scale(objectScript.lastDragged.GetComponent<RectTransform>(), -0.001f, true);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                objectScript.lastDragged.GetComponent<RectTransform>().transform.localScale = Scale(objectScript.lastDragged.GetComponent<RectTransform>(), 0.001f, false);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                objectScript.lastDragged.GetComponent<RectTransform>().transform.localScale = Scale(objectScript.lastDragged.GetComponent<RectTransform>(), -0.001f, false);
            }
        }
    }
    public Vector3 Scale(RectTransform car, float offset, bool isX)
    {
        
        float tempScale = isX ? car.transform.localScale.x : car.transform.localScale.y;
        tempScale += tempScale + offset < 1.2f && tempScale + offset > 0.3f ? offset : 0;
        return new Vector3(isX ? tempScale : car.transform.localScale.x, isX ? car.transform.localScale.y : tempScale, 1);
    } 
}

