namespace HA.PathFinder {
    public class Tile {

        public int x { get; private set; }
        public int y { get; private set; }

        public Tile(int x, int y) {
            this.x = x;
            this.y = y;
        }
    }
}
