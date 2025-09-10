using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckVehicleValidity : MonoBehaviour, IDropHandler
{
    private float _endRotation, _entityRotation, _validRotation;
    private Vector3 _endScale, _entityScale;
    private float _xDifference, _yDifference;
    public ObjScript objectScript;

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && Input.GetMouseButtonUp(0))
        {
            if (eventData.pointerDrag.tag.Equals(tag))
            {
                Debug.Log("tags match!");
            } else
            {
                Debug.Log("tags do not match!");
                objectScript.rightPlace = false;
                objectScript.effects.PlayOneShot(objectScript.audioCli[1]);

                // read start position from keyvaluepair
                eventData.pointerDrag.GetComponent<RectTransform>().transform.localPosition = objectScript.GetStartPosition(eventData.pointerDrag.tag);
                Debug.Log(objectScript.GetStartPosition(eventData.pointerDrag.tag));
            }
        }
        //throw new System.NotImplementedException();
    }
}
