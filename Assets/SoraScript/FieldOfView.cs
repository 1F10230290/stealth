using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class FieldOfView : MonoBehaviour
{
    [Header("視界の設定")]
    public float viewRadius = 10f;  // 視界の距離
    [Range(0, 360)]
    public float viewAngle = 90f;   // 視界の角度（扇形）

    [Header("レイヤー設定")]
    public LayerMask targetMask;    // プレイヤーのレイヤー
    public LayerMask obstacleMask;  // 壁などの障害物レイヤー

    [Header("描画設定")]
    public float meshResolution = 1f; // 1度あたり何本のレイを飛ばすか
    public int edgeResolveIterations = 4; // 壁の端を滑らかにするための計算回数
    public float edgeDstThreshold = 0.5f; // 壁の端と見なす距離の閾値

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>(); // 検知したプレイヤーリスト

    private MeshFilter viewMeshFilter; // 視界のメッシュを描画するためのMeshFilter
    private Mesh viewMesh; // 視界のメッシュ
     private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        // MeshFilterコンポーネントを取得してメッシュを初期化
        viewMeshFilter = GetComponent<MeshFilter>();
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;

        StartCoroutine("FindTargetsWithDelay", 0.2f); // 0.2秒ごとに検知処理を行う
    }

    void LateUpdate()
    {
        DrawFieldOfView(); // 毎フレーム視界を描画
    }

    // --- 1. 検知ロジック ---
    IEnumerator FindTargetsWithDelay(float delay)
    {
        // 一定時間ごとにターゲット検知を行うコルーチン
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        // 毎回検知する前にリストをクリア
        visibleTargets.Clear();

        // 視界の距離内にいるターゲット候補を取得
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        // 各ターゲットが視界の角度内に入っているか、障害物がないかをチェック
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform; // 敵からターゲットへの方向ベクトルを計算
            Vector3 dirToTarget = (target.position - transform.position).normalized; // ターゲットが視界の角度内に入っているかを確認

            // 視界の角度内に入っているか
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position); // ターゲットまでの距離を計算

                // 間に障害物がないかRaycastで確認
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                    Debug.Log("プレイヤー発見！: " + target.name);
                    // ここで「ゲームオーバー」や「追跡モード」などの処理を呼ぶ
                    gameManager.GameOver();
                }
            }
        }
    }

    // --- 2. 視界のメッシュ描画 ---
    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();

        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);
            viewPoints.Add(newViewCast.point);
        }

        // メッシュの頂点と三角形を作成
        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero; // 視界の起点（敵の位置）
        for (int i = 0; i < vertexCount - 1; i++)
        {
            // ローカル座標に変換
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }
}