// GridNode.cs
// Last edited 7:43 PM 04/15/2015 by Aaron Freedman

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.PrototypingKit.Patterns.Grid
{
    /// <summary>
    ///     The grid node is each node in the Grid. It has a list of other GridNode nodes it is connected to.
    ///     The logic for determining a connection is implemented in the GridManager.
    /// </summary>
    public abstract class GridNode : MonoBehaviour
    {
        public GameObject contents;
        public Grid grid;
        public bool mouseOver;
        public bool mousePressed;
        public List<GridNode> connections;
        public IDictionary<Grid.Direction, GridNode> connectionByDirection;
        public Vector2 index;

        protected abstract void Start();

        //protected virtual void Update() {}

        public virtual bool HasConnectionWithNode(GridNode withThisNode)
        {
            return connections.Any(connectedNode => connectedNode == withThisNode);
        }

        public virtual void AddConnection<T>(Grid.Direction direction, T gridNode) where T : GridNode
        {
            if (connectionByDirection == null)
            {
                connectionByDirection = new Dictionary<Grid.Direction, GridNode>();
            }
            GridNode newNode = gridNode;
            connectionByDirection.Add(direction, newNode);
            connections.Add(gridNode);
        }
    }
}