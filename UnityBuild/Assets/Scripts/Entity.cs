using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Transactions;
using DG.Tweening;
using UnityEngine;
using UnityEngine.PlayerLoop;


internal sealed class Entity : MonoBehaviour
{
    [Header("Settings")]
    public Marker CurrentMarker;
    public float MoveWaving = 25f;
    [Space]
    [Header("References")]
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _pathChunkStep = 0.3f;
    //[SerializeField] private GameObject _pathChunkPrefab;
    //[HideInInspector] public Transform _pathChunksParent;
    public GameObject SelectImage;

    public int DiceCount;
    public int Candles = 0;

    public static System.Action<Entity, Marker> OnTargetArrived;

    private List<GameObject> _path = new List<GameObject>();
    private Rigidbody2D _rigidbody2D;
    private Marker _homeMarker;
    private Marker _moveTarget;
    private LineRenderer _lineRenderer;
    private Vector2 _direction;
    private bool _movingHome;
    

    private void Start()
    {
        _homeMarker = CurrentMarker;
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void OnEnable()
    {
        if(_moveTarget != null)
        {
            Moving();
        }
    }

    private void Kill()
    {
        SelectionMinions.Instance.PullEntity.Remove(gameObject);
        ResourceHolder.Instance.AddResource(ResourceHolder.ResourceType.Minions, -1);
        Destroy(gameObject);
    }

    //public void SelectedThis(bool select) => SelectImage.SetActive(select);

    public void MoveTo(Marker marker)
    {
        if (_moveTarget != null)
            return;
        _moveTarget = marker;
        //_direction = Vector3.Normalize(_moveTarget.transform.position - transform.position);
        
        Moving();
    }

    public void BackHome()
    {
        _movingHome = true;
        MoveTo(_homeMarker);
    }

    private void Moving()
    {
        _direction = new Vector2(_moveTarget.transform.position.x, _moveTarget.transform.position.y) - new Vector2(transform.position.x, transform.position.y);
        var distans = _direction.magnitude;
        Vector2[] pullWeapoints = new Vector2[(int) (distans / 1)];
        pullWeapoints = SetPathWeapoint(pullWeapoints);
        
        
        var seq = DOTween.Sequence();
        seq.Append(_rigidbody2D.DOPath(pullWeapoints, _moveSpeed, PathType.Linear, PathMode.TopDown2D, 10,
            Color.green));
        seq.Append(_rigidbody2D.DOMove(_direction * _pathChunkStep + Random.insideUnitCircle * MoveWaving, _moveSpeed, false))
        
        //_rigidbody2D.DOMove(_direction * _pathChunkStep + Random.insideUnitCircle * MoveWaving, _moveSpeed, false);
        //DrawPath();
        TargetArrived();
    }

    private Vector2 [] SetPathWeapoint(Vector2 [] pullWeapon)
    {
        for (int i = 0; i < pullWeapon.Length; i++)
        {
            var direction = new Vector2(_moveTarget.transform.position.x, _moveTarget.transform.position.y) - new Vector2(transform.position.x, transform.position.y);
            var distans = _direction.magnitude;
            if (i > 0) pullWeapon[i] = pullWeapon[i - 1];
            else pullWeapon[i] = transform.position;
            pullWeapon[i] += direction + Random.insideUnitCircle * MoveWaving;
        }

        return pullWeapon;
    }

    private void TargetArrived()
    {
        transform.position = _moveTarget.transform.position + (Vector3)Random.insideUnitCircle * 100f;

        if (_movingHome)
        {
            ResourceHolder.Instance.AddResource(ResourceHolder.ResourceType.Candles, Candles);
            Kill();
        }
        else
        {
            CurrentMarker = _moveTarget;
            OnTargetArrived?.Invoke(this, _moveTarget);
            _moveTarget = null;
        }
    }

    /*private void DrawPath()
    {
        Vector3 [] points = new Vector3[100];
        _lineRenderer.positionCount = points.Length;
        for (int i = 0; i < points.Length; i++)
        {
            float time = i * 0.8f;
            points[i] = transform.position * (_moveSpeed * time);
        }
        _lineRenderer.SetPositions(points);
    }*/

    public void OnMouseDown()
    {
        foreach (var i in SelectionMinions.Instance.PullEntity.Where(i => i.GetComponent<Entity>().SelectImage.activeSelf == true))
        {
            i.GetComponent<Entity>().SelectImage.SetActive(false);
        }

        SelectImage.SetActive(true);
    }
}
