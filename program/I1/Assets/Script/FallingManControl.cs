using UnityEngine;


public class FallingManControl : MonoBehaviour
{
    private bool isWalking;
    private Animator animator;

    private Vector3 startTransform;
    // SQ 제어로 만들꺼임
    void Awake()
    {
        animator = GetComponent<Animator>();
        isWalking = true;
        startTransform = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
         switch (SQControl.instance.sequenceStep)
        {
            case 0:
                transform.position = startTransform;
                transform.position += transform.forward * 1f * Time.deltaTime;
                break;

            case 1:
                animator.SetTrigger("idle1");
                break;

            case 2:
                animator.SetTrigger("Die");
                break;
        }
    }
}
