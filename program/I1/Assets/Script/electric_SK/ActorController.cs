using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public Transform startPoint;     // 출발 위치
    public Transform targetPoint;    // 도착 위치

    private Animator animator;
    private bool isPlaying = false;

    void OnEnable()
    {
        if (isPlaying) return;
        isPlaying = true;

        animator = GetComponent<Animator>();
        StartCoroutine(LoopSequence());
    }

    IEnumerator LoopSequence()
    {
        while (true)
        {
            // 출발 위치로 초기화
            transform.position = startPoint.position;
            transform.rotation = startPoint.rotation;

            // 걷기 - 시작 애니메이션
            animator.Play("Walking");

            // 이동
            yield return StartCoroutine(MoveToTarget(targetPoint.position, 2.5f));

            // 전체 애니메이션 루프 끝날 때까지 대기
            yield return new WaitForSeconds(GetFullLoopDuration());

            // 루프 딜레이
            yield return new WaitForSeconds(3f);
        }
    }

    float GetFullLoopDuration()
    {
        // 걷기 + 작업 + 감전 애니메이션 길이 합산
        return 2.0f + 7.0f + 9.0f;
    }


    IEnumerator MoveToTarget(Vector3 targetPos, float duration)
    {
        Vector3 startPos = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        transform.position = targetPos;
    }
}