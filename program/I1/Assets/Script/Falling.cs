using DG.Tweening;
using UnityEngine;

public class Falling : MonoBehaviour
{
    public float speed;
    public float DestroyTime;
    GameObject man;
    private int SQsave;
    private Vector3 startTransform;
    private Vector3 newPoint;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        man = GameObject.Find("FallingMan");
        startTransform = transform.position;
        newPoint = startTransform / 2;
    }

    // Update is called once per frame
    void Update()
    {

        switch (SQControl.instance.sequenceStep)
        {
            case 0:
                
                break;

            case 1:

                break;

            case 2:

                break;

        }

         if (SQsave != SQControl.instance.sequenceStep)
        {
            Switching();
        }
        SQsave = SQControl.instance.sequenceStep;
    }

    void Switching()
    {

        switch (SQControl.instance.sequenceStep)
        {
            case 0:
                transform.position = startTransform;
                
                break;

            case 1:
                Vector3 tagetPos = man.transform.position;
                transform.DOMove(tagetPos, SQControl.instance.sequence_2 - SQControl.instance.sequence_1).SetEase(Ease.InQuad);
                transform.DORotate(new Vector3(360, 0, 0), 1.5f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(SQControl.instance.sequence_2);
                break;

            case 2:
                transform.DOMove(newPoint, SQControl.instance.sequence_2 - SQControl.instance.sequence_1).SetEase(Ease.OutQuad);

                break;

        }
        
        
    }
}
