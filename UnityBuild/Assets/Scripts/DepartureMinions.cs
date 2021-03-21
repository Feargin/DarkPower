using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public sealed class DepartureMinions : MonoBehaviour
{
    private int _diceCount;
    private int _getDiceCount;
    [SerializeField] private GameObject _perfab;
    [SerializeField] private GameObject _map;
    [SerializeField] private Marker _homeMarker;
    [SerializeField][Range(0, 4)] private int _radiusSpawn;
    [SerializeField] private GameObject _areaSpawn;
    [SerializeField] private UnityEvent _MessagePanel;
    [SerializeField] private Transform _pathChunksParent;
    [FormerlySerializedAs("Dices")] [SerializeField] private Image [] dices;
    [SerializeField] private Sprite [] _sprite;
    

    public void Departure()
    {
        if (ResourceHolder.Instance.GetResource(ResourceHolder.ResourceType.Minions) >= 2)
        {
            _MessagePanel?.Invoke();
            return;
        }

        SetSpriteDice();
        
        _getDiceCount = _diceCount;
        if (_getDiceCount > 0)
        {
            var entityObject = Instantiate(_perfab,
                _homeMarker.gameObject.transform.position + (Vector3) Random.insideUnitCircle * _radiusSpawn,
                Quaternion.identity, _areaSpawn.transform);
            var entity = entityObject.GetComponent<Entity>();
            entity.DiceCount = _getDiceCount;
            entity.CurrentMarker = _homeMarker;
            SelectionMinions.Instance.PullEntity.Add(entityObject);
            //SelectionMinions.Instance.SetHighlighted(entityObject);
            /*entityObject.AddComponent<Button>().onClick.AddListener(
                delegate { SelectionMinions.Instance.SetHighlighted(entityObject); });*/
            //entity._pathChunksParent = _pathChunksParent;
            UIManager.Instance.EnablePanel(_map);
            ResourceHolder.Instance.AddResource(ResourceHolder.ResourceType.Minions, 1);
            ResourceHolder.Instance.AddResource(ResourceHolder.ResourceType.PowerDise, _diceCount);
            _diceCount = 0;
        }
    }

    private void SetSpriteDice()
    {
        for (int i = dices.Length - 1; i >= 0; i--)
        {
            if(dices[i].sprite == _sprite[1])
            {
                dices[i].sprite = _sprite[2];
            }
        }
    }
    public void GetDicePower (bool get)
    {
        if(get && _diceCount < 4)
        {
            _diceCount += 1;
            foreach (var t in dices)
            {
                if (t.sprite != _sprite[0]) continue;
                t.sprite = _sprite[1];
                break;
            }
        }
        else if(!get)
        {
            if(_diceCount > 0) _diceCount -= 1;
            for (int i = dices.Length - 1; i >= 0; i--)
            {
                if (dices[i].sprite != _sprite[1]) continue;
                dices[i].sprite = _sprite[0];
                break;
            }
        }
    }
}
