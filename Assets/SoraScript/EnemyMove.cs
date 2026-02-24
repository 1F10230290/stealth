using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float rotateSpeed = 50f; // 回転速度
    public float rorateAngle = 45f; // 回転する角度
    public float stopTime = 10f; // 回転するまでの時間
    private float currentStopTime; // 現在の停止時間
    private float rotatedAngle = 0f; // 現在どれだけ回転したか
    private bool isRotating = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 回転ロジック
        if (!isRotating)
        {
            currentStopTime += Time.deltaTime;

            if (currentStopTime >= stopTime)
            {
                isRotating = true; // 回転開始
                currentStopTime = 0f; // 停止時間をリセット
                rotatedAngle = 0f; // 回転角度をリセット
            }
        }
        else
        {
            // 停止時間が経過したら回転を開始
            float rotateStep = rotateSpeed * Time.deltaTime; // 回転する量を計算

            // 回転を行う
            transform.Rotate(0, rotateStep, 0);
            rotatedAngle += Mathf.Abs(rotateStep);  // 回転した角度を累積

            // 回転時間が経過したら停止状態に戻る
            if (rotatedAngle >= rorateAngle)
            {
                isRotating = false; // 回転終了
                rotateSpeed *= -1; // 回転方向を反転
            }
        }
    }


}
