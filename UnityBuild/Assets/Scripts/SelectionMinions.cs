using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SelectionMinions : MonoBehaviour
{
    public static SelectionMinions Instance;
    //public static System.Action<MapEntity, Image> ItWasHighlighted;
    public Marker TargetMarker;
    public List<GameObject> PullEntity;
    private MapEntity _entity;
    private void Awake ()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this; 
        PullEntity = new List<GameObject>();
    }

    /*public void Selected(GameObject obj)
    {
        //ItWasHighlighted?.Invoke(obj.GetComponent<MapEntity>(), obj.GetComponent<Image>());
    }

    private void OnEnable()
    {
        SelectionMinions.ItWasHighlighted += SetHighlighted;
    }

    private void OnDisable()
    {
        SelectionMinions.ItWasHighlighted -= SetHighlighted;
    }


    private void SetHighlighted(MapEntity entity, Image image)
    {
        if(entity.Selected == false)
        {
            _entity = entity;
            entity.Selected = true;
            image.color = new Color(0.5f, 0.5f, 0.5f);
        }
        else
        {
            entity.Selected = false;
            image.color = new Color(1f, 1f, 1f);
        }
    }*/

    public void SetHighlighted(GameObject obj)
    {
        foreach(GameObject i in PullEntity)
        {
            i.GetComponent<MapEntity>().SelectedThis(false);
            if(i == obj)
            {
                i.GetComponent<MapEntity>().SelectedThis(true);
            }
        }
    }
    public void SetTarget ()
    {
        if (TargetMarker == null) return;
        foreach (var i in PullEntity.Where(i => i.GetComponent<MapEntity>().SelectImage.activeSelf == true))
        {
            i.GetComponent<MapEntity>().MoveTo(TargetMarker);
        }
    }
}
