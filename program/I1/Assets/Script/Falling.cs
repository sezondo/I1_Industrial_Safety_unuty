using UnityEngine;

public class Falling : MonoBehaviour
{
    public float speed;
    public float DestroyTime;
    GameObject man;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        man = GameObject.Find("FallingMan");
    }

    // Update is called once per frame
    void Update()
    {
        if (SQControl.instance.sequenceStep == 0)
        {
            
        }
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        Destroy(gameObject, DestroyTime);
    }
}
