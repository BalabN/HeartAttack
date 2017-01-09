using System.Collections.Generic;

namespace HA.PathFinder {
    public class MapMatrix {

        private int[][] Data;

        public MapMatrix(int[][] data) {
            Data = data;
        }

        public bool OutOfBounds(int x, int y) {
            return x < 0 || x >= Data[0].Length || y < 0 || y >= Data.Length;
        }

        public int GetWithInTiles() {
            return Data[0].Length;
        }

        public int GetHeightInTiles() {
            return Data.Length;
        }

        public bool Blocked(int x, int y) {
            if (OutOfBounds(x, y)) {
                return true;
            }
            return Data[x][y] == 0;
        }

        public List<Tile> GetNeighbors(int x, int y) {
            List<Tile> neighbors = new List<Tile>();

            if (!Blocked(x, y - 1)) {
                neighbors.Add(new Tile(x, y - 1));
            }
            if (!Blocked(x + 1, y)) {
                neighbors.Add(new Tile(x + 1, y));
            }
            if (!Blocked(x, y + 1)) {
                neighbors.Add(new Tile(x, y + 1));
            }
            if (!Blocked(x - 1, y)) {
                neighbors.Add(new Tile(x - 1, y));
            }
            return neighbors;
        }

        public int GetCost(Tile tile) {
            return Data[tile.y - 1][tile.x - 1];
        }
    }
}
