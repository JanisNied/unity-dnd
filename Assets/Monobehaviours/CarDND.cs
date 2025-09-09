using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class CarDND : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
{
    private CanvasGroup _canvasGroup;
    private RectTransform _rectTransform;
    public ObjScript objectScript;
    public CarBounds carBounds;
    public void OnBeginDrag(PointerEventData eventData)
    {
            Debug.Log("yay left click! and dragging!");
            objectScript.lastDragged = null;
            _canvasGroup.alpha = 0.6f;
            _canvasGroup.blocksRaycasts = false;
            _rectTransform.SetAsLastSibling();
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, carBounds.screenPoint.z));
            _rectTransform.position = mousePos;
            carBounds.screenPoint = Camera.main.WorldToScreenPoint(_rectTransform.localPosition);
            carBounds.offset = _rectTransform.localPosition - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, carBounds.screenPoint.z));
    }

    public void OnDrag(PointerEventData eventData)
    {
           
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("yay left click!");
            objectScript.effect.PlayOneShot(objectScript.effectSounds[0]);
        }
    }

    private void Start() {
        Debug.Log("total garbage");
        _canvasGroup = GetComponent<CanvasGroup>();
        _rectTransform = GetComponent<RectTransform>();
    }
   
}