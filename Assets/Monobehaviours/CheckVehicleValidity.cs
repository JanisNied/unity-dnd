using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckVehicleValidity : MonoBehaviour, IDropHandler
{
    private float _endRotation, _entityRotation, _rotDifference;
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
                // rot difference
                _endRotation = GetComponent<RectTransform>().transform.eulerAngles.z;
                _entityRotation = eventData.pointerDrag.GetComponent<RectTransform>().transform.eulerAngles.z;
                _rotDifference = Mathf.Abs(_endRotation - _entityRotation);
                Debug.Log("Rotation Difference: " + _rotDifference);

                // scale difference
                _endScale = GetComponent<RectTransform>().localScale;
                _entityScale = eventData.pointerDrag.GetComponent<RectTransform>().localScale;
                _xDifference = Mathf.Abs(_endScale.x - _entityScale.x);
                _yDifference = Mathf.Abs(_endScale.y - _entityScale.y);

                Debug.Log($"Scale Differences: ({_xDifference}, {_yDifference})");
                if (_rotDifference <= 5 || (_rotDifference >= 355 &&  _rotDifference <= 360) && (_xDifference <= 0.05 && _yDifference <= 0.05))
                {
                    objectScript.rightPlace = true;
                    Debug.Log("yay");
                    CopyTransform(GetComponent<RectTransform>(), eventData.pointerDrag.GetComponent<RectTransform>());
                } else
                {
                    eventData.pointerDrag.GetComponent<RectTransform>().transform.localPosition = objectScript.GetStartPosition(eventData.pointerDrag.tag);
                }
            } else
            {
                Debug.Log("tags do not match!");
                objectScript.rightPlace = false;
                objectScript.effects.PlayOneShot(objectScript.audioCli[1]);

                // read start position from keyvaluepair
                eventData.pointerDrag.GetComponent<RectTransform>().transform.localPosition = objectScript.GetStartPosition(eventData.pointerDrag.tag);
            }
        }
      
        //throw new System.NotImplementedException();
    }
    private void CopyTransform(RectTransform source, RectTransform target)
    {
        target.localPosition = source.localPosition;
        target.localScale = source.localScale;
        target.localRotation = source.localRotation;
    }
}
