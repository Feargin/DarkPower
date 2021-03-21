using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(Marker))]
    public class DrowMarkers : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderGismo(Marker marker, GizmoType gizmo)
        {
            Gizmos.color = new Color32(50, 40, 60, 90);
            Gizmos.DrawCube(marker.transform.position, new Vector3(2.5f, 2.5f, 0));
        }
    }
}