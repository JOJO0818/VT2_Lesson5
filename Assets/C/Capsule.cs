using UnityEngine;
using UnityEngine.Android;

public class Capsule : MonoBehaviour
{
    [SerializeField] private float _raycastDistance = 2f;

    [SerializeField] private LayerMask _layerMask;

    private float playerSpeed = 5f;


    void Start()
    {
        
    }

    void Update()
    {
        bool isHit = Physics.Raycast(
            origin:transform.position,      //光線の始点
            direction: Vector3.down,        //光線の方向
            hitInfo: out RaycastHit hit,    //衝突したオブジェクトの情報
            maxDistance: _raycastDistance,  //光線の長さ
            layerMask: _layerMask           //衝突レイヤー
            );

        Debug.DrawRay(transform.position,Vector3.down * _raycastDistance, Color.red);

        if(isHit)
        {
            Debug.Log($"==== 衝突情報 ====" +
                $"\nName ={ hit. transform.name}" +
                $"\nPoint ={hit.point}"+
                $"\nDistance = {hit.distance}"+
                $"\n============");

            Debug.DrawRay(hit.point,hit.normal,Color.yellow);
        }

        Vector3 velocity = transform.forward;

        Vector3 right = Vector3.Cross(velocity, hit.normal);

        //Vector3 slope = Vector3.Cross(right, hit.normal);

        Vector3 slope = Vector3.ProjectOnPlane(velocity, hit.normal);

        Debug.DrawRay(hit.point, slope.normalized, Color.green);

        transform.position += (slope.normalized * -1) * playerSpeed * Time.deltaTime;
    }


}
