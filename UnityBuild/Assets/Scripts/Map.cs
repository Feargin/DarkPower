using UnityEngine.UI;
using UnityEngine;

public sealed class Map : MonoBehaviour
{
    [SerializeField] private Transform _mapContent;
    [SerializeField] private ScrollRect _mapScroll;
    private Vector3 _basePosition;

    private void Start()
    {
        _basePosition = _mapContent.position;
    }

    public void SetViewportToBasePosition()
    {
        _mapContent.position = _basePosition;
        _mapScroll.velocity = Vector2.zero;
    }
}
