// Grid.cs
// Last edited 4:50 PM 05/16/2015 by Aaron Freedman

using System;
using UnityEngine;

namespace Assets.PrototypingKit.Patterns.Grid
{
    /// <summary>
    ///     A <see cref="Grid" /> object is a 2D array of <see cref="GridNode" /> objects.
    ///     <para>
    ///         It provides the baseline implementation of the
    ///         square tile array of nodes that can be overridden by derived classes.
    ///         It will also set-up the connections for each node in the Grid. (Only cardinal connections are implemented in
    ///         this
    ///         version.)
    ///     </para>
    /// </summary>
    public abstract class Grid : MonoBehaviour
    {
        public event EventHandler<GridCreationArgs> RaiseGridCreationEvent;
        private Quaternion storedRotation;

        public enum Direction
        {
            up,
            down,
            left,
            right,
            upLeft,
            upRight,
            downLeft,
            downRight
        };

        public enum ConnectionType
        {
            none,
            cardinal,
            cardinalWithDiagonal
        };

        [SerializeField] private ConnectionType connectionType;
        private GridNode[,] gridNodes;

        public GridNode[,] GridNodes
        {
            get { return gridNodes; }
        }

        [SerializeField] protected GameObject gridNodePrefab;
        public int columns, rows;
        public bool drawGizmoGrid;

        protected virtual void Start()
        {
            gridNodes = new GridNode[columns, rows];
            storedRotation = transform.rotation;
            CreateGrid();
        }

        protected virtual void NotifyCreation()
        {
            OnRaiseGridCreationEvent(new GridCreationArgs(this));
        }

        protected virtual void OnRaiseGridCreationEvent(GridCreationArgs e)
        {
            EventHandler<GridCreationArgs> handler = RaiseGridCreationEvent;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void Activate() {}

        protected virtual void OnDrawGizmosSelected()
        {
            DrawBounds();
            IterateOverGrid(DrawWireCubesAtNodes);
        }

        protected virtual void OnDrawGizmos()
        {

        }

        protected void DrawWireCubesAtNodes(Vector2 pos)
        {
            Gizmos.color = Color.grey;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireSphere(new Vector3(pos.x, pos.y, 0), 0.05f);
        }

        protected void DrawBounds()
        {
            Gizmos.color = Color.cyan;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(new Vector3(columns / 2, rows / 2, 0), new Vector3(columns, rows, 0.1f));
        }

        #region Grid creation

        /// <summary>
        ///     Creates the grid with the specified <c>rows</c> and <c>columns</c> by instantiating the linked
        ///     <c>gridNodePrefab</c> at each position.
        ///     Right now the creation of the grid assumes that the nodes will be 1x1x1 in size.
        /// </summary>
        protected void CreateGrid()
        {
            transform.rotation = Quaternion.identity;
            IterateOverGrid(CreateNode);
            ConnectGrid();
            transform.rotation = storedRotation;
            NotifyCreation();
        }

        protected void CreateNode(int x, int y)
        {
            var g =
                (GameObject)
                    Instantiate(gridNodePrefab, new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z),
                                Quaternion.identity);
            g.transform.parent = transform;
            gridNodes[x, y] = g.GetComponent<GridNode>();
            gridNodes[x, y].grid = this;
            gridNodes[x, y].index = new Vector2(x, y);
        }

        /// <summary>
        ///     Connects the grid based on the selected connection type.
        /// </summary>
        protected void ConnectGrid()
        {
            switch (connectionType)
            {
                case ConnectionType.cardinal:
                    for (var x = 0; x < columns; x++)
                    {
                        for (var y = 0; y < rows; y++)
                        {
                            GridNode currentNode = gridNodes[x, y];
                            ConnectGridNode(ref currentNode, x, y, x - 1, y, Direction.left);
                            ConnectGridNode(ref currentNode, x, y, x + 1, y, Direction.right);
                            ConnectGridNode(ref currentNode, x, y, x, y + 1, Direction.up);
                            ConnectGridNode(ref currentNode, x, y, x, y - 1, Direction.down);
                        }
                    }
                    break;

                case ConnectionType.cardinalWithDiagonal:
                    for (var x = 1; x < columns - 1; x++)
                    {
                        for (var y = 1; y < rows - 1; y++)
                        {
                            GridNode currentNode = gridNodes[x, y];
                            ConnectGridNode(ref currentNode, x, y, x - 1, y, Direction.left);
                            ConnectGridNode(ref currentNode, x, y, x + 1, y, Direction.right);
                            ConnectGridNode(ref currentNode, x, y, x, y + 1, Direction.up);
                            ConnectGridNode(ref currentNode, x, y, x, y - 1, Direction.down);
                            ConnectGridNode(ref currentNode, x, y, x - 1, y - 1, Direction.upLeft);
                            ConnectGridNode(ref currentNode, x, y, x + 1, y - 1, Direction.upRight);
                            ConnectGridNode(ref currentNode, x, y, x - 1, y + 1, Direction.downRight);
                            ConnectGridNode(ref currentNode, x, y, x + 1, y - 1, Direction.downLeft);
                        }
                    }
                    break;

                case ConnectionType.none:

                    break;

                default:

                    break;
            }
        }

        private GridNode ConnectGridNode<T>(ref T gridNode, int fromRow, int fromColumn, int targetRow, int targetColumn,
                                            Direction directionOfConnection) where T : GridNode
        {
            GridNode fromNode = gridNode;

            if (fromRow == targetRow && fromColumn == targetColumn)
            {
                return null;
            }

            if (targetRow > rows - 1 || targetColumn > rows - 1 || targetRow < 0 || targetColumn < 0)
            {
                return null;
            }

            if (gridNodes[targetRow, targetColumn] != null)
            {
                GridNode targetGridNode = gridNodes[targetRow, targetColumn];
                fromNode.AddConnection(directionOfConnection, targetGridNode);
                return targetGridNode;
            }
            return null;
        }

        #endregion

        protected void MoveGridInWorldSpace()
        {
            var yDelta = 0;
            var xDelta = 0;
            if (Input.GetButtonDown("Vertical"))
            {
                if (Input.GetAxis("Vertical") > 0)
                {
                    yDelta = 1;
                }
                else
                {
                    yDelta = -1;
                }
            }

            if (Input.GetButtonDown("Horizontal"))
            {
                if (Input.GetAxis("Horizontal") > 0)
                {
                    xDelta = 1;
                }
                else
                {
                    xDelta = -1;
                }
            }

            transform.position = new Vector3(transform.position.x + xDelta, transform.position.y + yDelta, 0);
        }

        #region Utiities & Queries

        public bool IsNodeOccupied(int x, int y)
        {
            if (gridNodes[x, y].contents != null)
            {
                return true;
            }
            return false;
        }

        public GridNode GetGridNodeAtWorldCoordinate(Vector3 pos)
        {
            foreach (GridNode g in gridNodes)
            {
                if (Math.Abs(g.transform.position.x - pos.x) < Mathf.Epsilon && Math.Abs(g.transform.position.y - pos.y) < Mathf.Epsilon)
                {
                    return g;
                }
            }
            return null;
        }

        protected void IterateOverGrid(Action<GridNode> methodToInvoke)
        {
            for (var x = 0; x < columns; x++)
            {
                for (var y = 0; y < rows; y++)
                {
                    methodToInvoke(gridNodes[x, y]);
                }
            }
        }

        protected void IterateOverGrid(Action<Vector2> methodToInvoke)
        {
            for (var x = 0; x < columns; x++)
            {
                for (var y = 0; y < rows; y++)
                {
                    methodToInvoke(new Vector2(x, y));
                }
            }
        }

        protected void IterateOverGrid(Action<int, int> methodToInvoke)
        {
            for (var x = 0; x < columns; x++)
            {
                for (var y = 0; y < rows; y++)
                {
                    methodToInvoke(x, y);
                }
            }
        }

        #endregion
    }
}