using UnityEngine;
using UnityEngine.EventSystems;

public class UIPointer : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource src;
    public AudioClip clip;
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        src.PlayOneShot(clip);
    }

    
}
