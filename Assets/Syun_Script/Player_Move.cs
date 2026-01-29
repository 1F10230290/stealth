using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(0f, 0f, speed);
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(0f, 0f, -speed);
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(speed, 0f, 0f);
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(-speed, 0f, 0f);
        }
    }
}
