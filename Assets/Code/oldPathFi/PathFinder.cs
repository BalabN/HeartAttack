using System.Collections.Generic;
using System.Linq;

namespace HA.PathFinder {
    public class PathFinder {

        private const string OPEN = "open";
        private const string CLOSED = "closed";

        private MapMatrix MapMatrix;
        private List<Step> Closed = new List<Step>();
        private List<Step> Open = new List<Step>();
        private List<Pair<Step, string>> History = new List<Pair<Step, string>>();
        private int StepCount;

        private const int maxSearchDistance = 10;

        public PathFinder(MapMatrix mapMatrix) {
            MapMatrix = mapMatrix;
        }

        private void AddHistory(Pair<Step, string> step) {
            History.Add(step);
        }

        private void AddOpen(Step step) {
            AddHistory(new Pair<Step, string>(step, OPEN));
            Open.Add(step);
        }

        private bool InOpen(Step step) {
            if (Open.IndexOf(step) != -1) {
                return true;
            }
            return false;
        }

        private Step InOpen(Tile tile) {
            for (int i = 0; i < Open.Count; i++) {
                if (Open[i].x == tile.x && Open[i].y == tile.y) {
                    return Open[i];
                }
            }
            return null;
        }

        private bool RemoveOpen(Step step) {
            AddClosed(step);
            return Open.Remove(step);
        }

        private Step GetBestOpen() {
            int bestI = 0;
            for (int i = 0; i < Open.Count; i++) {
                if (Open.ElementAt(i).f < Open.ElementAt(bestI).f) {
                    bestI = i;
                }
            }
            return Open.ElementAt(bestI);
        }

        private void AddClosed(Step step) {
            AddHistory(new Pair<Step, string>(step, CLOSED));
            Closed.Add(step);
        }

        private bool InClosed(Step step) {
            if (Closed.IndexOf(step) != -1) {
                return true;
            }
            return false;
        }

        private Step InClosed(Tile tile) {
            for (int i = 0; i < Closed.Count; i++) {
                if (Closed[i].x == tile.x && Closed[i].y == tile.y) {
                    return Closed[i];
                }
            }
            return null;
        }

        public List<Step> FindPath(int xC, int yC, int xT, int yT) {
            Step current;
            List<Tile> neighbors;
            Step neighborRecord;
            int stepCost = 0;

            Reset();
            AddOpen(new Step(xC, yC, xT, yT, StepCount, null));

            while (Open.Count != 0) {
                StepCount++;
                current = GetBestOpen();

                if (current.x == xT && current.y == yT) {
                    return BuildPath(current, new List<Step>());
                }

                RemoveOpen(current);

                neighbors = MapMatrix.GetNeighbors(current.x, current.y);
                for (int i = 0; i < neighbors.Count; i++) {
                    Tile neighbor = neighbors.ElementAt(i);
                    StepCount++;

                    stepCost = current.g + MapMatrix.GetCost(neighbor);

                    neighborRecord = InClosed(neighbor);
                    if (neighborRecord != null && stepCost >= neighborRecord.g) {
                        continue;
                    }

                    neighborRecord = InOpen(neighbor);
                    if (neighborRecord != null || stepCost < neighborRecord.g) {
                        if (neighborRecord == null) {
                            AddOpen(new Step(neighbor.x, neighbor.y, xT, yT, stepCost, current));
                        } else {
                            neighborRecord.Parent = current;
                            neighborRecord.g = stepCost;
                            neighborRecord.f = stepCost + neighborRecord.h;
                        }
                    }
                }
            }
            return null;
        }

        private List<Step> BuildPath(Step step, List<Step> stack) {
            stack.Add(step);

            if (step.HasParent()) {
                return BuildPath(step.Parent, stack);
            } else {
                return stack;
            }
        }

        private void Reset() {
            Open = new List<Step>();
            Closed = new List<Step>();
        }
    }
}