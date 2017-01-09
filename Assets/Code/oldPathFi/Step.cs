using System;

namespace HA.PathFinder {
    public class Step {

        public int x { get; private set; }
        public int y { get; private set; }
        public int g { get; set; }
        public int h { get; private set; }
        public int f { get; set; }
        public Step Parent { get; set; }

        public Step() {
        }

        public Step(int xC, int yC, int xT, int yT, int totalSteps, Step parentStep) {
            int dist = Distances.distanceManhattan(xC, yC, xT, yT);

            x = xC;
            y = yC;
            g = totalSteps;
            h = dist;
            f = totalSteps + dist;
            Parent = parentStep;
        }

        private static class Distances {
            public static int distanceManhattan(int xC, int yC, int xT, int yT) {
                int dx = Math.Abs(xT - xC);
                int dy = Math.Abs(yT - yC);
                return dx + dy;
            }
        }

        public bool HasParent() {
            if (Parent != null) {
                return true;
            }
            return false;
        }
    }
}
