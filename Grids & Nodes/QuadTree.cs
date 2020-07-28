using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script by Aaron Freedman
//edits by John Wanamaker
public class QuadTree 
{
    //ever heard of node tree? 
    //yeah it's that

    private const int MaxObjects = 32;
    private const int MaxLevels = 4;
    private readonly int _level;
    private readonly List<Transform> _objects; // fastest type to use is Vector3 but it makes accessing rigidbody vars difficult
    private const float ObjSize = 0.1f; // idealized object size as radius

    public QuadTree(int level, Rect bounds, bool isoMode)
    {
        _level = level;
        _objects = new List<Transform>();
        Bounds = bounds;
        Nodes = new QuadTree[4];
        SubZAxisForY = isoMode;
    }

    public Rect Bounds { get; set; }

    public QuadTree[] Nodes { get; set; }

    public bool SubZAxisForY { get; set; } // for implementations of QT in 3D space; substitutes y axis for z axis;

    public void Clear()
    {
        _objects.Clear();
        for (var i = 0; i < Nodes.Length; i++)
        {
            if (Nodes[i] == null) continue;
            Nodes[i].Clear();
            Nodes[i] = null;
        }
    }

    public void Split()
    {
        int subWidth = (int) Bounds.width / 2;
        int subHeight = (int) Bounds.height / 2;
        var x = (int) Bounds.x;
        var y = (int) Bounds.y;

        // nodes are ordered for Unity's coordinate system; from bottom-left clockwise
        Nodes[0] = new QuadTree(_level + 1, new Rect(new Vector2(x, y), new Vector2(subWidth, subHeight)), SubZAxisForY);
        Nodes[1] = new QuadTree(_level + 1, new Rect(new Vector2(x, y + subHeight), new Vector2(subWidth, subHeight)), SubZAxisForY);
        Nodes[2] = new QuadTree(_level + 1, new Rect(new Vector2(x + subWidth, y + subHeight), new Vector2(subWidth, subHeight)),
                                SubZAxisForY);
        Nodes[3] = new QuadTree(_level + 1, new Rect(new Vector2(x + subWidth, y), new Vector2(subWidth, subHeight)), SubZAxisForY);
    }

    public int GetIndex(Vector3 obj)
    {
        if (SubZAxisForY)
        {
            if (!Bounds.Contains(new Vector3(obj.x, obj.z, 0))) return -1;
        }
        else
        {
            if (!Bounds.Contains(obj)) return -1;
        }
        double horizontalMidpoint = Bounds.y + (Bounds.height / 2);
        double verticalMidpoint = Bounds.x + (Bounds.width / 2);

        //TODO: doesn't include size

        bool topSide = SubZAxisForY ? (obj.z > horizontalMidpoint) : (obj.y > horizontalMidpoint);
        bool leftSide = obj.x < verticalMidpoint;

        if (topSide)
        {
            return leftSide ? 1 : 2;
        }
        return leftSide ? 0 : 3;
    }

    public int GetIndex(Transform obj)
    {
        return GetIndex(obj.position);
    }

    public void Insert(Transform obj)
    {
        if (Nodes[0] != null)
        {
            int index = GetIndex(obj);

            if (index != -1)
            {
                Nodes[index].Insert(obj);

                return;
            }
        }

        _objects.Add(obj);

        if (_objects.Count > MaxObjects && _level < MaxLevels)
        {
            if (Nodes[0] == null)
            {
                Split();
            }

            var i = 0;
            while (i < _objects.Count)
            {
                int index = GetIndex(_objects[i]);
                if (index != -1)
                {
                    Nodes[index].Insert(_objects[i]);
                    _objects.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }
    }

    //public void Insert(Vector3 obj)
    //{
    //    if (_nodes[0] != null)
    //    {
    //        int index = GetIndex(obj);

    //        if (index != -1)
    //        {
    //            _nodes[index].Insert(obj);

    //            return;
    //        }
    //    }

    //    _objects.Add(obj);

    //    if (_objects.Count > _maxObjects && _level < _maxLevels)
    //    {
    //        if (_nodes[0] == null)
    //        {
    //            Split();
    //        }

    //        var i = 0;
    //        while (i < _objects.Count)
    //        {
    //            int index = GetIndex(_objects[i]);
    //            if (index != -1)
    //            {
    //                _nodes[index].Insert(_objects[i]);
    //                _objects.RemoveAt(i);
    //            }
    //            else
    //            {
    //                i++;
    //            }
    //        }
    //    }
    //}

    //public void Retrieve(Vector3 obj, List<Vector3> list)
    //{
    //    int index = GetIndex(obj);
    //    if (index != -1 && _nodes[0] != null)
    //    {
    //        _nodes[index].Retrieve(obj, list);
    //    }

    //    list.AddRange(_objects);
    //}

    public void Retrieve(Transform obj, List<Transform> list)
    {
        int index = GetIndex(obj);
        if (index != -1 && Nodes[0] != null)
        {
            Nodes[index].Retrieve(obj, list);
        }

        list.AddRange(_objects);
    }

    /// <summary>
    ///     Retrieves all objects in tree and subtrees and returns as a list
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public List<Transform> Retrieve(Transform obj)
    {
        int index = GetIndex(obj);
        List<Transform> list = new List<Transform>();
        if (index != -1 && Nodes[0] != null)
        {
            list.AddRange(Nodes[index].Retrieve(obj));
        }

        list.AddRange(_objects);
        return list;
    }

    public List<Transform> Search(Transform obj, float range, List<Transform> list)
    {
        //List<Transform> list = new List<Transform>();
        List<QuadTree> nodes = new List<QuadTree>();
        HashSet<Transform> set = new HashSet<Transform>();

        nodes.AddRange(Nodes);
        int index = GetIndex(obj);

        var queryRect = new Rect(obj.position.x - range / 2, obj.position.z - range / 2, range, range);

        if (index == -1 || Nodes[0] == null) return list;
        if (Bounds.Contains(queryRect.position) && Bounds.Contains(queryRect.position + Vector2.one * range))
        {
            // traverse subnodes
        }

        // if the search region completely contains a node, add all objects in subnodes
        if (queryRect.Contains(Bounds.position) && queryRect.Contains(Bounds.position + Bounds.size))
        {
            Retrieve(obj, list);
        }

        //foreach (QuadTree quadTree in nodes) {}

        //list.AddRange(_objects);


        return list;
    }

    public static bool Overlap(Rect area, QuadTree tree)
    {
        return (tree.Bounds.Overlaps(area));
    }
}