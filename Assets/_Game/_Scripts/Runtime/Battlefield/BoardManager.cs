using UnityEngine;

namespace Runtime.Battlefield
{
    public class BoardManager : MonoBehaviour
    {
        private const int WIDTH = 5;
        private const int HEIGHT = 5;
        private Cell[,] _grid;

        internal void Initialize()
        {
            _grid = new Cell[WIDTH, HEIGHT];
            for (int i = 0; i < this.transform.childCount; i++)
            {
                var currentCell = this.transform.GetChild(i).GetComponent<Cell>();
                int x = i % WIDTH;
                int y = i / WIDTH;
                currentCell.SetCoordinates(x,y);
                _grid[x, y] = currentCell;
                currentCell.SetNoTargetCell();
            }
        }
    }
}
