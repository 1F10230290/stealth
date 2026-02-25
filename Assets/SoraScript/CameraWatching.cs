using UnityEngine;

public class CameraWatching : MonoBehaviour
{
    [Header("カメラの移動先（空のオブジェクト）リスト")]
    public Transform[] cameraPositions;

    [Header("切り替え時間（秒）")]
    public float switchInterval = 5.0f;

    [Header("動きの設定")]
    public float moveSpeed = 1.0f;
    public float rotateSpeed = 0.5f;

    private Transform mainCamTransform;
    private int currentIndex = 0;
    private float timer;

    void Start()
    {
        if (Camera.main != null)
        {
            mainCamTransform = Camera.main.transform;
        }
        else
        {
            Debug.LogError("Main Cameraが見つかりません");
        }

        timer = switchInterval;
        MoveCamera();
    }

    void Update()
    {
        if (cameraPositions.Length == 0 || mainCamTransform == null) return;

        // ドリー＆パン（移動と回転）
        mainCamTransform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        mainCamTransform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            currentIndex = (currentIndex + 1) % cameraPositions.Length;
            MoveCamera();
            timer = switchInterval;
        }
    }

    void MoveCamera()
    {
        if (cameraPositions[currentIndex] == null) return;
        mainCamTransform.position = cameraPositions[currentIndex].position;
        mainCamTransform.rotation = cameraPositions[currentIndex].rotation;
    }

    // カメラの範囲（画角）をシーンビューに描画する機能
    void OnDrawGizmos()
    {
        if (cameraPositions == null) return;

        // メインカメラの設定を参考にする（なければ一般的な値）
        float fov = (Camera.main != null) ? Camera.main.fieldOfView : 60f;
        
        // 描画する視線の長さ（実際のFarClipPlaneだと長すぎるので、見やすい20mくらいにしています）
        float viewDistance = 20f; 

        Gizmos.color = Color.yellow; // 黄色の枠線で表示

        foreach (Transform pos in cameraPositions)
        {
            if (pos == null) continue;

            // そのオブジェクトの位置と回転に合わせてGizmosの座標系を変更
            Gizmos.matrix = Matrix4x4.TRS(pos.position, pos.rotation, Vector3.one);

            // カメラの視錐台（ピラミッド型の視野）を描画
            // 引数: 中心, FOV, 最大距離, 最小距離, アスペクト比
            Gizmos.DrawFrustum(Vector3.zero, fov, viewDistance, 0.3f, 1.77f);

            // カメラの向きを示す線も引いておく
            Gizmos.color = Color.red;
            Gizmos.DrawLine(Vector3.zero, Vector3.forward * viewDistance);
            Gizmos.color = Color.yellow; // 色を戻す
        }
    }
}