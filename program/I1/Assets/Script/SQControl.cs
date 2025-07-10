using UnityEngine;

public class SQControl : MonoBehaviour
{
    public int sequenceStep;
    public int sequence_1;
    public int sequence_2;
    public int sequence_3;
    public float currentTime;
    public static SQControl instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // 싱글톤 변수 instance가 비어있는가?
        if (instance == null)
        {
            // instance가 비어있다면(null) 그곳에 자기 자신을 할당
            instance = this;
        }
        else
        {
            // instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우

            // 씬에 두개 이상의 GameManager 오브젝트가 존재한다는 의미.
            // 싱글톤 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.LogWarning("씬에 두개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }

        sequenceStep = 0;
        sequence_1 = 2;
        sequence_2 = 4;
        sequence_3 = 9;
    }


    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        Timeflies();
    }

    void Timeflies()
    {
        if (currentTime < 2)
        {
            sequenceStep = 0; //남자 걸어감 / 돌 대기
        }
        else if (currentTime > 2 && currentTime < 6)
        {
            sequenceStep = 1; //남자 서있음 / 돌 남자에게 옴
        }
        else if (currentTime > 6 && currentTime < 11)
        {
            sequenceStep = 2; //남자 죽음 / 돌 충돌후 다른대로감
        }
        else if (currentTime > 9)
        {
            sequenceStep = 0; // 리셋
            currentTime = 0;
        }
    }
}
