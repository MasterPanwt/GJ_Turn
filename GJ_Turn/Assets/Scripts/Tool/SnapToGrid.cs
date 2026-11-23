using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.EditorTools;
using UnityEditor;

[EditorTool("Tilemap 吸附移动")]
public class SnapToGrid : EditorTool
{
    public override void OnToolGUI(EditorWindow window)
    {
        Grid grid = Object.FindObjectOfType<Grid>();
        if (!grid) { Debug.LogWarning("场景中没有Grid"); }

        foreach (Transform t in Selection.transforms)
        {
            if (!t) continue;

            EditorGUI.BeginChangeCheck();
            Vector3 dragged = Handles.PositionHandle(t.position, t.rotation);
            if (!EditorGUI.EndChangeCheck()) continue;

            Undo.RecordObject(t, "吸附移动");

            Vector3Int TargetCell = grid.WorldToCell(dragged);
            Vector3 world = grid.GetCellCenterWorld(TargetCell);
            t.position = new Vector3(world.x, world.y, t.position.z)
;        }
    }
}
