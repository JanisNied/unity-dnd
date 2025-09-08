using UnityEngine;

public class ObjScript : MonoBehaviour
{
    public GameObject[] vehicles;
    [HideInInspector]
    public Vector2[] originalCoords;
    public Canvas _canvas;
    public AudioSource effect;
    public AudioClip[] effectSounds;
    [HideInInspector]
    public bool rightPlace = false;
    public GameObject lastDragged = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
