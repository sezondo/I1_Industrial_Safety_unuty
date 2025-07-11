using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class Track : MonoBehaviour
{
    public ARTrackedImageManager manager;

    public List<GameObject> list1 = new List<GameObject>();
    private Dictionary<string, GameObject> dict1 = new Dictionary<string, GameObject>();

    public List<AudioClip> list2 = new List<AudioClip>();
    private Dictionary<string, AudioClip> dict2 = new Dictionary<string, AudioClip>();

    private bool isWaitingForClear = false;

    public Button stopTrackingButton;

    void Start()
    {
        // 리스트를 딕셔너리로 변환
        foreach (GameObject obj in list1)
        {
            dict1[obj.name] = obj;
        }

        foreach (AudioClip clip in list2)
        {
            dict2[clip.name] = clip;
        }

        // 버튼 클릭 시 트래킹 재개
        stopTrackingButton.onClick.AddListener(ClearAndEnableTracking);
    }

    void UpdateImage(ARTrackedImage trackedImage)
    {
        if (isWaitingForClear) return;

        if (trackedImage.trackingState != TrackingState.Tracking)
        return;

        string name = trackedImage.referenceImage.name;
        SQControl.instance.currentTime = 0f;


        if (dict1.ContainsKey(name))
        {
            GameObject prefab = dict1[name];
            prefab.transform.position = trackedImage.transform.position;
            prefab.transform.rotation = trackedImage.transform.rotation;

            if (!prefab.activeSelf)
                prefab.SetActive(true);


            prefab.GetComponentInChildren<SimpleTypewriter>()?.StartTyping();
            isWaitingForClear = true;
        }
    }

    void UpdateSound(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;

        if (dict2.ContainsKey(name))
        {
            AudioClip clip = dict2[name];
            AudioSource audio = GetComponent<AudioSource>();
            if (audio != null)
            {
                audio.PlayOneShot(clip);
            }
            else
            {
                Debug.LogWarning(" AudioSource가 없음");
            }

        }
    }

    void OnChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        if (isWaitingForClear) return;

        foreach (var added in eventArgs.added)
        {
            UpdateImage(added);
            UpdateSound(added);
        }

        foreach (var updated in eventArgs.updated)
        {
            UpdateImage(updated);
        }

        foreach (var removedImageKeyValuePair in eventArgs.removed) // 이 t는 KeyValuePair입니다!
        {
            // KeyValuePair에서 ARTrackedImage를 추출합니다.
            ARTrackedImage removedImage = removedImageKeyValuePair.Value;

            // 이제 removedImage는 ARTrackedImage이므로 referenceImage에 접근할 수 있습니다.
            string name = removedImage.referenceImage.name;

            if (dict1.ContainsKey(name))
            {
                dict1[name].SetActive(false);
            }

        }
    }


    void ClearAndEnableTracking()
    {
        foreach (var obj in dict1.Values)
        {
            obj.SetActive(false);
        }

        isWaitingForClear = false;
        Debug.Log("✅ 프리팹 비활성화됨, 트래킹 재개 준비 완료");
    }


    
    void OnEnable() => manager.trackablesChanged.AddListener(OnChanged);

    void OnDisable() => manager.trackablesChanged.RemoveListener(OnChanged);
}