using UnityEngine;
using UnityEngine.UI;

public class DepartureMinions : MonoBehaviour
{
    private int _diceCount;
    private int _getDiceCount;
    [SerializeField] private GameObject _perfab;
    [SerializeField] private GameObject _map;
    [SerializeField] private Marker _homeMarker;
    [SerializeField] private GameObject _areaSpawn;
    [SerializeField] private GameObject _MessagePanel;
    [SerializeField] private Transform _pathChunksParent;
    [SerializeField] private Image [] Dices;
    [SerializeField] private Sprite [] _sprite;

    public void Departure()
    {
        if (ResourceHolder.Instance.GetResource(ResourceHolder.ResourceType.Minions) >= 2)
        {
            _MessagePanel.SetActive(true);
            return;
        }

        for (int i = Dices.Length - 1; i >= 0; i--)
        {
            if(Dices[i].sprite == _sprite[1])
            {
                Dices[i].sprite = _sprite[2];
            }
        }
        
        _getDiceCount = _diceCount;
        if (_getDiceCount <= 0) return;
        var entityObject = Instantiate(_perfab, _homeMarker.gameObject.transform.position + (Vector3)Random.insideUnitCircle * 100f, Quaternion.identity, _areaSpawn.transform);
        var entity = entityObject.GetComponent<MapEntity>();
        entity.DiceCount = _getDiceCount;
        entity.CurrentMarker = _homeMarker;
        SelectionMinions.Instance.PullEntity.Add(entityObject);
        SelectionMinions.Instance.SetHighlighted(entityObject);
        entityObject.AddComponent<Button>().onClick.AddListener(
            delegate 
            { 
                SelectionMinions.Instance.SetHighlighted(entityObject); 
            });
        entity._pathChunksParent = _pathChunksParent;
        UIManager.Instance.EnablePanel(_map);
        ResourceHolder.Instance.AddResource(ResourceHolder.ResourceType.Minions, 1);
        ResourceHolder.Instance.AddResource(ResourceHolder.ResourceType.PowerDise, _diceCount);
        _diceCount = 0;

    }
    public void GetDicePower (bool get)
    {
        if(get && _diceCount < 4)
        {
            _diceCount += 1;
            foreach (var t in Dices)
            {
                if (t.sprite != _sprite[0]) continue;
                t.sprite = _sprite[1];
                break;
            }
        }
        else if(!get)
        {
            if(_diceCount > 0) _diceCount -= 1;
            for (int i = Dices.Length - 1; i >= 0; i--)
            {
                if (Dices[i].sprite != _sprite[1]) continue;
                Dices[i].sprite = _sprite[0];
                break;
            }
        }
    }
}
