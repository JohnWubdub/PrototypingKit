// GridItem.cs
// Last edited 7:43 PM 04/15/2015 by Aaron Freedman

using UnityEngine;

//#pragma warning disable 0219 
#pragma warning disable 0414

namespace Assets.PrototypingKit.Patterns.Grid
{
    /// <summary>
    ///     Very basic abstract class for objects which go into a Grid
    /// </summary>
    public abstract class GridItem : MonoBehaviour
    {
        private Vector2 startPos;
        public GridNode gridNode;
        public bool blocksMovement;

        protected virtual void Start()
        {
            startPos = new Vector2(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y));
        }

        private void OnDrawGizmos()
        {
//        transform.position = new Vector2(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y));
        }

//    protected virtual void Update()
//    {
//    }

        public virtual void Activate() {}

        public virtual void SetGridNode(GridNode _gridNode)
        {
            gridNode = _gridNode;
        }
    }
}