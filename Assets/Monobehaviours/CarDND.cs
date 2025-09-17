using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CarDND : MonoBehaviour, IPointerDownHandler, IBeginDragHandler,
    IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGro;
    private RectTransform rectTra;
    public ObjScript objectScr;
    public CarBounds screenBou;
    public bool dragging = false;
    [SerializeField] private float dragSpeed = 0.8f;  // values below 10 are sluggish

    void Start()
    {
        canvasGro = GetComponent<CanvasGroup>();
        rectTra = GetComponent<RectTransform>();
    }
    private void Update()
    {
        // ondrag substitute for smoother lerping towards target (mouse)

        // NOTE: not framerate independent!!! (i don't think it affects much in this context though, for a game like this, but i like the lerping, so i'll keep it as is)
        if (dragging)
        {
            Vector3 curSreenPoint =
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenBou.screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curSreenPoint) + screenBou.offset;
            rectTra.position = Vector3.Lerp(rectTra.position, screenBou.GetClampedPosition(curPosition), Time.deltaTime * dragSpeed);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("OnPointerDown");
            objectScr.effects.PlayOneShot(objectScr.audioCli[0]);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
        {
            dragging = true;
            objectScr.lastDragged = eventData.pointerDrag;
            canvasGro.blocksRaycasts = false;
            canvasGro.alpha = 0.6f;
            rectTra.SetAsLastSibling();
            Vector3 cursorWorldPos = Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenBou.screenPoint.z));
            rectTra.position = cursorWorldPos;

            screenBou.screenPoint = Camera.main.WorldToScreenPoint(rectTra.localPosition);

            screenBou.offset = rectTra.localPosition -
                Camera.main.ScreenToWorldPoint(
                    new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                screenBou.screenPoint.z));
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
        {
            //Vector3 curSreenPoint =
            //    new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenBou.screenPoint.z);
            //Vector3 curPosition = Camera.main.ScreenToWorldPoint(curSreenPoint) + screenBou.offset;
            //rectTra.position = Vector3.Lerp(rectTra.position, screenBou.GetClampedPosition(curPosition), Time.deltaTime * 0.2);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
            //objectScr.lastDragged = eventData.pointerDrag;
            canvasGro.blocksRaycasts = true;
            canvasGro.alpha = 1.0f;
            if (objectScr.rightPlace)
            {
                canvasGro.blocksRaycasts = false;
                objectScr.lastDragged = null;
            }

            objectScr.rightPlace = false;
        }
    }
}