using UnityEngine;

public class DebuggingScript : MonoBehaviour
{
    [Header("Both arrays must be of equal length")]
    public GameObject[] _vehicles;
    public GameObject[] _places;
    [Header("Must be the GO that spawns obstacles")]
    public GameObject _obstacleHolder;
    public bool _isObstacles = true;
   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
            FinalizeParameters("full");
        if (Input.GetKeyDown(KeyCode.F3))
            ToggleObstacles();
        if (Input.GetKeyDown(KeyCode.F4))
            FinalizeParameters("position");
    }
    private void ToggleObstacles()
    {
        _isObstacles = !_isObstacles;
        _obstacleHolder.SetActive(_isObstacles);
    }
    private void FinalizeParameters(string type)
    {
       if (_vehicles.Length == _places.Length)
            for (int i = 0; i < _vehicles.Length; i++)
            {
                if (_places[i] != null && _vehicles[i] != null)
                    CopyTransform(_places[i].GetComponent<RectTransform>(), _vehicles[i].GetComponent<RectTransform>(), type);
            }
    }
    private void CopyTransform(RectTransform source, RectTransform target, string type)
    {
       switch (type)
        {
            case "full":
                target.localPosition = source.localPosition;
                target.localScale = source.localScale;
                target.localRotation = source.localRotation;
                break;
            case "position":
                target.localPosition = source.localPosition;
                break;
        }
    }
}
