using System.Collections;
using UnityEngine;

public class ShockEffectTrigger : MonoBehaviour
{
    public float delay = 1.0f;                 // 몇 초 뒤에 실행할지
    public GameObject effectPrefab;            // 파티클 프리팹
    public Transform spawnPoint;               // 생성 위치 (null이면 자기 위치)

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

            // 모든 자식 파티클 재생 (비활성 포함)
            var effects = instanceObj.GetComponentsInChildren<ParticleSystem>(true);
            foreach (var ps in effects)
            {
                ps.Play();
            }

            // 3초 뒤에 비활성화
            yield return new WaitForSeconds(3f);
            instanceObj.SetActive(false);
        }
        else
        {
            Debug.LogWarning("effectPrefab is null");
        }
    }
}
