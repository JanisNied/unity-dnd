using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using DG.Tweening;
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
    private List<KeyValuePair<string, AudioClip>> _sounds = new List<KeyValuePair<string, AudioClip>>();
    [HideInInspector]
    public bool drag = false;
    public bool victoryScreen = false;
    public TMP_Text timeLabel;
    public TMP_Text victoryScreenTimeLabel;
    public TMP_Text destroyedLabel;
    public GameObject victoryScreenUI;
    public GameObject[] stars;
    public GameStats stats;
    private float points = 0;

    [HideInInspector]
    public int vehiclesRemaining = 0;
    [HideInInspector]
    public int vehiclesDestroyed = 0;
    private void Awake()
    {

     // empty

    }
    public void FillOutStartPos()
    {
        for (int i = 0; i < vehicles.Length; i++)
        {
            _sounds.Add(new KeyValuePair<string, AudioClip>(vehicles[i].tag, audioCli[i + 2]));
            _startPositions.Add(new KeyValuePair<string, Vector3>(vehicles[i].tag, vehicles[i].GetComponent<RectTransform>().localPosition));
            Debug.Log(_startPositions);
            vehiclesRemaining++;
        }
        Debug.Log(_startPositions.FirstOrDefault(x => x.Key == "garbage").Value);
        Debug.Log("populated kvp list");
    }
   public Vector3 GetStartPosition(string tag)
    {
        return _startPositions.FirstOrDefault(x => x.Key == tag).Value;
    }
    public AudioClip GetAudioClip(string tag)
    {
        return _sounds.FirstOrDefault(x => x.Key == tag).Value;
    }
    private void Update()
    {
        if (vehiclesRemaining == 0)
        {
            if (!victoryScreen)
            {
                victoryScreen = true;
                victoryScreenUI.SetActive(true);
                victoryScreenUI.transform.DOBlendableScaleBy(new Vector3(1, 1, 1), 0.4f);
                Debug.Log("win win win!");
                Debug.Log("time spent: "+timeLabel.text);
                victoryScreenTimeLabel.text = "Time spent: " + timeLabel.text;
                destroyedLabel.text = "Vehicles destroyed: " + vehiclesDestroyed;
                float time = Mathf.Floor(stats.totalElapsed);
                if (time < 600f)
                {
                    points += 30f;
                } else if (time < 900f)
                {
                    points += 20f;
                } else if (time < 1800f)
                {
                    points += 10f;
                }
                points -= vehiclesDestroyed * 2;
                if (points > 25f)
                {
                    foreach (GameObject star in stars)
                    {
                        star.SetActive(true);
                    }
                } else if (points > 15 && points <= 25)
                {
                    stars[0].SetActive(true);
                    stars[1].SetActive(true);
                }
                else if (points > 10 && points <= 15)
                {
                    stars[0].SetActive(true);
                }
            }
        }
    }
}