using System.Collections.Generic;
using UnityEngine;

public class VehicleShuffler : MonoBehaviour
{
    [Header("Randomized Spots")]
    public List<GameObject> _randomSpotPlaces;
    public List<GameObject> _randomSpotVehicles;
    [Header("Existing Vehicles")]
    public List<GameObject> _vehicles;
    public List<GameObject> _places;
    public ObjScript _objScript;

    void Awake()
    {
        ShufflePositions();
        _objScript.FillOutStartPos();
    }
    public void ShufflePositions()
    {
        foreach (GameObject _place in _places)
        {
            GameObject chosenSpot = _randomSpotPlaces[Random.Range(0, _randomSpotPlaces.Count)];
            if (chosenSpot != null)
            {
                _randomSpotPlaces.Remove(chosenSpot);
                Vector3 pos = chosenSpot.GetComponent<RectTransform>().localPosition;
                _place.transform.localPosition = pos;
                float scale = Mathf.Round(Random.Range(0.6f, 0.9f) * 100f) / 100f;
                _place.transform.localScale = new Vector3(scale, scale, 1);
                _place.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                _place.SetActive(true);
            }
        }
        foreach (GameObject _vehicle in _vehicles)
        {
            GameObject chosenSpot = _randomSpotVehicles[Random.Range(0, _randomSpotVehicles.Count)];
            if (chosenSpot != null)
            {
                _randomSpotVehicles.Remove(chosenSpot);
                Vector3 pos = chosenSpot.GetComponent<RectTransform>().localPosition;
                _vehicle.transform.localPosition = pos;
                _vehicle.SetActive(true);
            }
        }
        Debug.Log("shuffled!");
    }
}
