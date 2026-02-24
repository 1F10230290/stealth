using NUnit.Framework;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("移動の設定")]
    public float moveSpeed = 3f;    // 移動速度
    public float moveDistance = 5f; // 移動する距離

    [Header("回転の設定")]
    public float rotateSpeed = 50f; // 回転速度
    public float rorateAngle = 45f; // 回転する角度
    public float stopTime = 10f; // 回転するまでの時間

    private enum State { 
        Moving, // 移動中
        Waiting, // 待機中
        Looking, // 見まわし中
        TurningAround // 180度回転中
        }

    private State currentState = State.Waiting; // 現在の状態

    private Vector3 startPosition;    // 移動開始時の位置（距離計算用）
    private float currentStopTime; // 現在の停止時間
    private float rotatedAngle = 0f; // 現在どれだけ回転したか
    // 0: 右を向く, 1: 左を向く, 2: 正面に戻る
    private int lookPhase = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position; // 初期位置を保存
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.Moving:
                MoveUpdate();
                break;
            case State.Waiting:
                WaitUpdate();
                break;
            case State.Looking:
                LookUpdate();
                break;
            case State.TurningAround:
                TurnUpdate();
                break;
        }
    }

        //各状態の処理

        // 移動処理
        private void MoveUpdate()
        {
            // 前方(Z軸方向)へ移動
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            // 移動開始地点からの距離を計算し、設定距離(moveDistance)に達したか判定
            if (Vector3.Distance(startPosition, transform.position) >= moveDistance)
            {
                currentState = State.Waiting; // 待機状態へ移行
                currentStopTime = 0f;         // 待機タイマーをリセット
            }
        }

        // 待機処理
        private void WaitUpdate()
        {
            currentStopTime += Time.deltaTime; // 待機時間をカウント

            if (currentStopTime >= stopTime)
            {
                currentState = State.Looking; // 見まわし状態へ移行
                currentStopTime = 0f;         // タイマーをリセット
                rotatedAngle = 0f; // 見まわしの方向を初期化
                lookPhase = 0; // 見まわしのフェーズを初期化
            }
        }

        // 見回し処理
        private void LookUpdate()
        {
            float rotateStep = rotateSpeed * Time.deltaTime;

            if (lookPhase == 0)
            {
                // 設定角度(45度)まで向く
                transform.Rotate(0, rotateStep, 0);
                rotatedAngle += rotateStep;

                if (rotatedAngle >= rorateAngle)
                {
                    lookPhase = 1; // 正面に戻る動きに切り替え
                }
            }
            else if (lookPhase == 1)
            {
            // フェーズ1: 右端から左端(-rorateAngle)まで向く
            transform.Rotate(0, -rotateStep, 0);
            rotatedAngle -= rotateStep;

                if (rotatedAngle <= -rorateAngle)
                {
                    lookPhase = 2; // 正面に戻る処理に切り替え
                }
            }
            else if (lookPhase == 2)
            {
                // フェーズ2: 左端から正面(0度)に戻る
                transform.Rotate(0, rotateStep, 0);
                rotatedAngle += rotateStep;

                if (rotatedAngle >= 0f)
                {
                    // ピッタリ0度になるようにズレを補正
                    transform.Rotate(0, -rotatedAngle, 0);
                    rotatedAngle = 0f;

                    // 見回し完了、180度ターンへ移行
                    currentState = State.TurningAround;
                }
            }
        }

        // 180度反転処理
        private void TurnUpdate()
        {
            float rotateStep = rotateSpeed * Time.deltaTime;
            transform.Rotate(0, rotateStep, 0);
            rotatedAngle += rotateStep;

            if (rotatedAngle >= 180f)
            {
                // ターンが行き過ぎた分を綺麗に180度へ補正（ズレ防止）
                float overRotation = rotatedAngle - 180f;
                transform.Rotate(0, -overRotation, 0);

                // 次の移動のために開始位置を現在地に更新
                startPosition = transform.position;
                currentState = State.Moving; // 再び移動状態へ
            }
        }

    }