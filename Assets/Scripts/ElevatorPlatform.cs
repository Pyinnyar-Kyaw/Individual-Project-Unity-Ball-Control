using UnityEngine;

public class ElevatorPlatform : MonoBehaviour
{

    private float startPos;
    private float endPos;
    private float direction = 1f;
    private bool isWaiting = false;
    private float waitTimer = 0f;
    [SerializeField] float speed = 0.005f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position.y;
        endPos = startPos + 5;
    }

    // Update is called once per frame
    void Update()
    {

        if (isWaiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= 2f)
            {
                direction *= -1f;
                isWaiting = false;
                waitTimer = 0f;
            }
            return;
        }

        transform.position += new Vector3(0, speed * direction, 0);

        if (transform.position.y > endPos || transform.position.y < startPos)
        {

            //direction = direction * -1f;

            float clampedY = Mathf.Clamp(transform.position.y, startPos, endPos);
            transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);

            //transform.position += new Vector3(0, speed * direction, 0);
            isWaiting = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }

}