using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    public Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = player.position + new Vector3(0, 0.3f, -2);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + new Vector3(0,2f,-5);
        //transform.LookAt(player);
        transform.rotation = Quaternion.Euler(15, 0, 0);
    }
}
