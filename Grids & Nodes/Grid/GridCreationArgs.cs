// GridCreationArgs.cs
// Last edited 7:43 PM 04/15/2015 by Aaron Freedman

using System;

namespace Assets.PrototypingKit.Patterns.Grid
{
    /// <summary>
    ///     Custom EventArgs class that contains information relevant to grid creation.
    ///     Currently the only relevant data is a reference to the grid.
    ///     http://msdn.microsoft.com/en-us/library/awbftdfh.aspx
    /// </summary>
    public class GridCreationArgs : EventArgs
    {
        public GridCreationArgs(Grid _grid, string message = "")
        {
            grid = _grid;
        }

        private readonly Grid grid;
        private string msg;

        public Grid NewGrid
        {
            get { return grid; }
        }

        public string Message
        {
            get { return msg; }
        }
    }
}