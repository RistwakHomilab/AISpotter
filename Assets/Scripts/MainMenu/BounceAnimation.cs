using UnityEngine;

public class BounceAnimation : MonoBehaviour
{
    public float bounceHeight = 20f; // How high to bounce
    public float bounceSpeed = 2f;   // How fast to bounce

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        transform.localPosition = new Vector3(startPos.x, startPos.y + newY, startPos.z);
    }
}
