using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "ScriptableObject/Shape")]
public class ShapeData : ScriptableObject
{
    public string ShapeName;
    public int ShapeId;
    public List<Vector2> BlockVectorList;
    public List<BlockData> BlockDataList;
}
