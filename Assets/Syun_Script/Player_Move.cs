using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public Transform Camera;
    public float PlayerSpeed;
    public float RotationSpeed;

    Vector3 speed = Vector3.zero;
    Vector3 rot = Vector3.zero;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotation();

        // カメラをプレイヤーの位置に追従させる
        Camera.transform.position = transform.position;
    }

    void Move()
    {
        speed = Vector3.zero;
        rot = Vector3.zero;
        
        if(Input.GetKey(KeyCode.W))
        {
            rot.y = 0;
            MoveSet();
        }
        if(Input.GetKey(KeyCode.S))
        {
            rot.y = 180;
            MoveSet();
        }
        if(Input.GetKey(KeyCode.D))
        {
            rot.y = 90;
            MoveSet();
        }
        if(Input.GetKey(KeyCode.A))
        {
            rot.y = -90;
            MoveSet();
        }

        transform.Translate(speed* Time.deltaTime);
        
    }

    void MoveSet()
    {
        speed.z = PlayerSpeed;
        transform.eulerAngles = new Vector3(0, Camera.transform.eulerAngles.y + rot.y, 0);
    }

    void Rotation()
    {
        float rotStep = RotationSpeed * Time.deltaTime;

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            Camera.transform.Rotate(0f, -rotStep, 0f);
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            Camera.transform.Rotate(0f, rotStep, 0f);
        }

    }
}
