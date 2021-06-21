using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAction : MonoBehaviour
{
    private Camera _camera;
    private GameObject _playerPawn;
    private const string _playerPawnGameObjectName = "GameObjectSelf(Clone)";

    public Vector2 Margin;//相机与角色的相对范围
    public Vector2 Smoothing;//相机移动的平滑度
    public BoxCollider2D Bounds;//背景的边界

    private Vector3 _min;//边界最大值
    private Vector3 _max;//边界最小值

    public bool IsFollowing { get; set; }//用来判断是否跟随


    private void Awake()
    {
        _camera = GetComponent<Camera>();

        //_playerPawn = GameObject.FindGameObjectWithTag("Player");

    }

    // Start is called before the first frame update
    void Start()
    {


        _min = Bounds.bounds.min;//初始化边界最小值(边界左下角)
        _max = Bounds.bounds.max;//初始化边界最大值(边界右上角)
        IsFollowing = true;//默认为跟随

    }

    // Update is called once per frame
    void Update()
    {
        _playerPawn = GameObject.Find(_playerPawnGameObjectName);

        var x = _playerPawn.transform.position.x;
        var y = _playerPawn.transform.position.y;
        if (IsFollowing)
        {
            if (Mathf.Abs(x - _playerPawn.transform.position.x) > Margin.x)
            {//如果相机与角色的x轴距离超过了最大范围则将x平滑的移动到目标点的x
                x = Mathf.Lerp(x, _playerPawn.transform.position.x, Smoothing.x * Time.deltaTime);
            }
            if (Mathf.Abs(y - _playerPawn.transform.position.y) > Margin.y)
            {//如果相机与角色的y轴距离超过了最大范围则将x平滑的移动到目标点的ya
                y = Mathf.Lerp(y, _playerPawn.transform.position.y, Smoothing.y * Time.deltaTime);
            }
        }
        float orthographicSize = GetComponent<Camera>().orthographicSize;//orthographicSize代表相机(或者称为游戏视窗)竖直方向一半的范围大小,且不随屏幕分辨率变化(水平方向会变)
        var cameraHalfWidth = orthographicSize * ((float)Screen.width / Screen.height);//的到视窗水平方向一半的大小
        x = Mathf.Clamp(x, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);//限定x值
        y = Mathf.Clamp(y, _min.y + orthographicSize, _max.y - orthographicSize);//限定y值
        transform.position = new Vector3(x, y, transform.position.z);//改变相机的位置

    }


}
