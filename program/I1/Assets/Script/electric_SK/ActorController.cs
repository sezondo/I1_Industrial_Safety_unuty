using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public Transform startPoint;     // ��� ��ġ
    public Transform targetPoint;    // ���� ��ġ

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
            // ��� ��ġ�� �ʱ�ȭ
            transform.position = startPoint.position;
            transform.rotation = startPoint.rotation;

            // �ȱ� - ���� �ִϸ��̼�
            animator.Play("Walking");

            // �̵�
            yield return StartCoroutine(MoveToTarget(targetPoint.position, 2.5f));

            // ��ü �ִϸ��̼� ���� ���� ������ ���
            yield return new WaitForSeconds(GetFullLoopDuration());

            // ���� ������
            yield return new WaitForSeconds(3f);
        }
    }

    float GetFullLoopDuration()
    {
        // �ȱ� + �۾� + ���� �ִϸ��̼� ���� �ջ�
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