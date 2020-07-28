// GridManager.cs
// Last edited 7:43 PM 04/15/2015 by Aaron Freedman

using System.Collections.Generic;
using UnityEngine;

namespace Assets.PrototypingKit.Patterns.Grid
{
    /// <summary>
    ///     GridManager centralizes control of Grid objects but is not responsible for how a Grid object is created.
    ///     The GridManager is especially helpful when using multiple Grid objects.
    ///     (A lot of this isn't implemented in this Unity version.)
    /// </summary>
    public abstract class GridManager<T> : Singleton<T> where T : Object
    {
        [HideInInspector] public List<Grid> grid;
        public bool alwaysDrawNodes;
        public bool alwaysDrawBounds;

        protected virtual void Start()
        {
            Grid[] grids = FindObjectsOfType<Grid>();
            foreach (Grid g in grids)
            {
                grid.Add(g);
            }
        }

    }
}