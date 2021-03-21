using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public sealed class SelectionMinions : MonoBehaviour
{
    public static SelectionMinions Instance;
    //public static System.Action<MapEntity, Image> ItWasHighlighted;
    public Marker TargetMarker;
    public List<GameObject> PullEntity;
    private Entity _entity;
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
    
    public void SetTarget () 
    {
        if (TargetMarker == null) return;
        foreach (var i in PullEntity.Where(i => i.GetComponent<Entity>()?.SelectImage.activeSelf == true))
        {
            i.GetComponent<Entity>().MoveTo(TargetMarker);
        }
    }
}
