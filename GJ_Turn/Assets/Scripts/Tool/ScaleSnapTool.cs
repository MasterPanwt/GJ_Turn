using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.EditorTools;
using UnityEditor;

[EditorTool("Tilemap 吸附缩放")]
public class ScaleSnapTool : EditorTool
{
    public override void OnToolGUI(EditorWindow window)
    {
        Grid grid = Object.FindObjectOfType<Grid>();
        if (!grid) { Debug.LogWarning("场景中没有Grid"); }

        foreach(Transform t in Selection.transforms)
        {
            EditorGUI.BeginChangeCheck();
            Vector3 raw = Handles.ScaleHandle(
                t.localScale, t.position, t.rotation,
                HandleUtility.GetHandleSize(t.position));
            if(!EditorGUI.EndChangeCheck())continue;

            Undo.RecordObject(t,"吸附缩放");
            t.localScale = SnapToCells(t, grid, raw);
        }
    }

    static Vector3 SnapToCells(Transform t, Grid grid, Vector3 rawScale)
    {
        Vector3 cell = grid.cellSize;
        Vector3 baseSize = cell; //默认物体大小为一格

        if (t.TryGetComponent<SpriteRenderer>(out SpriteRenderer sr) && sr.sprite)
            baseSize = sr.sprite.bounds.size;

        baseSize.x = Mathf.Max(1e-4f, baseSize.x);
        baseSize.y = Mathf.Max(1e-4f, baseSize.y);
        cell.x = Mathf.Max(1e-4f, cell.x);
        cell.y = Mathf.Max(1e-4f, cell.y);

        float nx = Mathf.Max(1f, Mathf.Round(rawScale.x * baseSize.x / cell.x));
        float ny = Mathf.Max(1f, Mathf.Round(rawScale.y * baseSize.y / cell.y));

        return new(nx * cell.x / baseSize.x, ny * cell.y / baseSize.y, rawScale.z);
    }
}
