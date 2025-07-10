using UnityEngine;


public class FallingManControl : MonoBehaviour
{
    private Animator animator;

    private Vector3 startTransform;
    private int SQsave;
    // SQ 제어로 만들꺼임
    void Awake()
    {
        animator = GetComponent<Animator>();
        startTransform = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (SQControl.instance.sequenceStep)
        {
            case 0:
                transform.Translate(-transform.right * 0.025f * Time.deltaTime);
                break;

            case 1:

                break;

            case 2:

                break;

        }

        if (SQsave != SQControl.instance.sequenceStep)
        {
            SQsave = SQControl.instance.sequenceStep;
            Switching();
        }


    }

    void Switching()
    {
        switch (SQControl.instance.sequenceStep)
        {
            case 0:
                transform.position = startTransform;
                animator.SetTrigger("Run");
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
