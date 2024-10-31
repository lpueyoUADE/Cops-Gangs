using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFS_Dijkstra: MonoBehaviour
{
    private void Start()
    {
        print("Using Dijkstra");
    }

    public static List<T> Run<T>(T start, Func<T, bool> verification, Func<T, List<T>> getConections, Func<T,T, float> getCost,int watchDog = 500)
    {
        PriorityQueue<T> pending = new PriorityQueue<T>();
        HashSet<T> visited = new HashSet<T>();
        Dictionary<T, T> parents = new Dictionary<T, T>();
        Dictionary<T, float> costs = new Dictionary<T, float>();

        pending.Enqueue(start, 0);
        costs[start] = 0;

        while (!pending.IsEmpty)
        {
            watchDog--;
            if (watchDog <= 0) break;
            T current = pending.Dequeue();

            if (verification(current))
            {                
                List<T> path = new List<T>();
                path.Add(current);
                while (parents.ContainsKey(path[path.Count - 1]))
                {
                    path.Add(parents[path[path.Count - 1]]);
                }

                path.Reverse();

                foreach (T node in path)
                {
                    print("Dijkstra:" + node);
                }
                return path;
            }
            else
            {
                visited.Add(current);
                List<T> connections = getConections(current);

                for(int i = 0; i < connections.Count; i++)
                {
                    T child = connections[i];
                    if (visited.Contains(child)) continue;
                    //**************
                    var currentCost = costs[current] + getCost(current,child);
                    if (costs.ContainsKey(child) && costs[child] <= currentCost) continue;
                    costs[child] = currentCost;

                    //**************
                    pending.Enqueue(child, currentCost);
                    parents[child] = current;
                }
            }
        }

        return new List<T>();
    }

}
