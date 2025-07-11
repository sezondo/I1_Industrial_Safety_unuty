using System.Collections;
using UnityEngine;

public class ShockEffectTrigger : MonoBehaviour
{
    public float delay = 1.0f;                 // �� �� �ڿ� ��������
    public GameObject effectPrefab;            // ��ƼŬ ������
    public Transform spawnPoint;               // ���� ��ġ (null�̸� �ڱ� ��ġ)

    void OnEnable()
    {
        StartCoroutine(DelayedEffect());
    }

    IEnumerator DelayedEffect()
    {
        yield return new WaitForSeconds(delay);

        if (effectPrefab != null)
        {
            Vector3 position = (spawnPoint != null) ? spawnPoint.position : transform.position;
            Quaternion rotation = (spawnPoint != null) ? spawnPoint.rotation : Quaternion.identity;

            GameObject instanceObj = Instantiate(effectPrefab, position, rotation);

            // ��� �ڽ� ��ƼŬ ��� (��Ȱ�� ����)
            var effects = instanceObj.GetComponentsInChildren<ParticleSystem>(true);
            foreach (var ps in effects)
            {
                ps.Play();
            }

            // 3�� �ڿ� ��Ȱ��ȭ
            yield return new WaitForSeconds(3f);
            instanceObj.SetActive(false);
        }
        else
        {
            Debug.LogWarning("effectPrefab is null");
        }
    }
}
