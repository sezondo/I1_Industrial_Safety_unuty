using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARFoundation.VisualScripting;
 
public class Track : MonoBehaviour
{
    public ARTrackedImageManager manager;
    public List<GameObject> list1 = new List<GameObject>();
    private Dictionary<string, GameObject> dict1
                                    = new Dictionary<string, GameObject>();
 
    public List<AudioClip> list2 = new List<AudioClip>();
    Dictionary<string, AudioClip> dict2
                            = new Dictionary<string, AudioClip>();                        
 
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject o in list1)
        {
            dict1.Add(o.name, o);
        }
 
        foreach (AudioClip o in list2)
        {
            dict2.Add(o.name, o);
        }
 
    }

    void UpdateImage(ARTrackedImage t)
    {
        string name = t.referenceImage.name;
 
        GameObject o = dict1[name];
        o.transform.position = t.transform.position;
        o.transform.rotation = t.transform.rotation;
        o.SetActive(true);
    }
 
    void UpdateSound(ARTrackedImage t)
    {
        string name = t.referenceImage.name;
 
        AudioClip o = dict2[name];
        GetComponent<AudioSource>().PlayOneShot(o);
    }
 
 
    void OnChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach (var t in eventArgs.added)
        {
            // Handle added event
            UpdateImage(t);
            UpdateSound(t);
        }
 
        foreach (var t in eventArgs.updated)
        {
            // Handle updated event
            UpdateImage(t);
        }
 
        foreach (var removedImage in eventArgs.removed)
        {
            // Handle removed event
        }
    }
 
 
 
    void OnEnable() => manager.trackablesChanged.AddListener(OnChanged);
 
    void OnDisable() => manager.trackablesChanged.RemoveListener(OnChanged);
 
 
 
 
    // Update is called once per frame
    void Update()
    {
        
    }
}