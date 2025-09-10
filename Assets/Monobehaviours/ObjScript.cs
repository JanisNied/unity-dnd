using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjScript : MonoBehaviour
{

    public GameObject[] vehicles;
    [HideInInspector]
    public Canvas can;
    public AudioSource effects;
    public AudioClip[] audioCli;
    [HideInInspector]
    public bool rightPlace = false;
    public GameObject lastDragged = null;
    // substitute for 12 cases within a switch constructor because who the fuck does that without thinking of alternative solutions
    private List<KeyValuePair<string, Vector3>> _startPositions = new List<KeyValuePair<string, Vector3>>();
    private void Awake()
    {

        for (int i = 0; i< vehicles.Length; i++)
        {
            _startPositions.Add(new KeyValuePair<string, Vector3>(vehicles[i].tag, vehicles[i].GetComponent<RectTransform>().localPosition));
            Debug.Log(_startPositions);
        }
        Debug.Log(_startPositions.FirstOrDefault(x => x.Key == "garbage").Value);

    }
   public Vector3 GetStartPosition(string tag)
    {
        return _startPositions.FirstOrDefault(x => x.Key == tag).Value;
    }
}