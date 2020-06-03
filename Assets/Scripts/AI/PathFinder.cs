using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathFinder {



    Vector3[] navPoints;


    public int[][] navPointStructure = {

        /*new int[] { 5, 1 },
        new int[] { 0, 2 },
        new int[] { 1, 3 },3333
        new int[] { 2, 4 },
        new int[] { 3, 5 },
        new int[] { 4, 0 }*/


        new int[] {1,3}, // 0
        new int[] {0,2,4}, // 1
        new int[] {1,5,6}, // 2
        new int[] {0,4,7,8}, // 3
        new int[] {1,3,5,9}, // 4
        new int[] {2,4,6,14}, // 5
        new int[] {2,5,15}, // 6
        new int[] {3,10,11}, // 7
        new int[] {3,9,12}, // 8
        new int[] {4,8,13}, // 9
        new int[] {7,11,17}, // 10
        new int[] {7,10,12,18}, // 11
        new int[] {8,11,13,19}, // 12
        new int[] {9,12,14,20,21}, // 13
        new int[] {5,13,15,22}, // 14
        new int[] {6,14,16}, // 15
        new int[] {15,22}, // 16
        new int[] {10,18,24}, // 17
        new int[] {11,17,19,25}, // 18
        new int[] {12,18,20,23}, // 19
        new int[] {13,19,21,23}, // 20
        new int[] {13,20,22,28}, // 21
        new int[] {14,16,21,29}, // 22
        new int[] {19,20,26,27}, // 23
        new int[] {17,25,31}, // 24
        new int[] {18,24,26,32}, // 25
        new int[] {23,25,27,32}, // 26
        new int[] {23,26,28,33}, // 27
        new int[] {21,27,29,34}, // 28
        new int[] {22,28,30}, // 29
        new int[] {29,35}, // 30
        new int[] {24,32,36}, // 31
        new int[] {25,26,31,33,37}, // 32
        new int[] {27,32,34,38}, // 33
        new int[] {28,33,35,39}, // 34
        new int[] {30,34,40}, // 35
        new int[] {31,37,41}, // 36
        new int[] {32,36,38,42}, // 37
        new int[] {33,37,39,43}, // 38
        new int[] {34,38,40,44}, // 39
        new int[] {35,39,45}, // 40
        new int[] {36,42,46}, // 41
        new int[] {37,41,43,47}, // 42
        new int[] {38,42,44,48}, // 43
        new int[] {39,43,45,49}, // 44
        new int[] {40,44,50}, // 45
        new int[] {41,47,51}, // 46
        new int[] {42,46,48,51}, // 47
        new int[] {43,47,49,52}, // 48
        new int[] {44,48,50,53}, // 49
        new int[] {45,49,53}, // 50
        new int[] {46,47,52,54}, // 51
        new int[] {48,51,53,55}, // 52
        new int[] {49,50,52,56}, // 53
        new int[] {51,55,57}, // 54
        new int[] {52,54,56,58}, // 55
        new int[] {53,55,59}, // 56
        new int[] {54,58}, // 57
        new int[] {55,57,59}, // 58
        new int[] {56,58}  // 59



    };


    public PathFinder(Vector3[] points)
    {
        navPoints = points;

        /*if (navPointStructure.Length == navPoints.Length)
        {
            //RunTest();
            //Debug.Log("TEST");
        }*/
        
    }
    

    public int[] FindPath(int[][] pointStructure, int pointA, int pointB)
    {

        float[] pointCost = new float[pointStructure.Length];
        int[] shortestPath = new int[pointStructure.Length];

        pointCost[pointA] = 0.01f;

        int[] currentPoints = { pointA };
        List<int> nextPoints = new List<int>();


        while (currentPoints.Length != 0)
        {

            for (int i = 0; i < currentPoints.Length; ++i)
            {

                for (int j = 0; j < pointStructure[currentPoints[i]].Length; ++j)
                {
                    int indexFrom = currentPoints[i];
                    int indexTo = pointStructure[currentPoints[i]][j];

                    if (pointCost[indexTo] == 0 || pointCost[indexTo] > Vector3.Distance(navPoints[indexFrom], navPoints[indexTo]) + pointCost[indexFrom])
                    {
                        pointCost[indexTo] = Vector3.Distance(navPoints[indexFrom], navPoints[indexTo]) + pointCost[indexFrom];
                        shortestPath[indexTo] = indexFrom;

                        nextPoints.Add(pointStructure[currentPoints[i]][j]);
                    }


                }

                
            }

            currentPoints = nextPoints.ToArray();
            nextPoints.Clear();

        }

        int currentPoint = pointB;
        List<int> path = new List<int>();
        path.Add(pointB);

        while (currentPoint != pointA)
        {
            //currentPoint = FindLowestCost(navPointStructure[currentPoint], pointCost);
            currentPoint = shortestPath[currentPoint];
            path.Add(currentPoint);
        }

        path.Reverse();


        string disp = "";

        for (int i = 0; i < pointCost.Length; ++i)
        {
            disp = String.Concat(disp, " || " + i + " -- " + pointCost[i]);
        }

        //Debug.Log(disp);


        int[] array = path.ToArray();

        for (int i = 1; i < array.Length; ++i)
        {
            Debug.DrawLine(navPoints[array[i]], navPoints[array[i - 1]], Color.red, 3600);
        }

        return path.ToArray();
    }


    /*
    public void RunTest()
    {
        int[] array = FindPath(navPointStructure, 2, 57);

        //float total = 0;

        for (int i = 1; i < array.Length; ++i)
        {
            Debug.DrawLine(navPoints[array[i]], navPoints[array[i - 1]], Color.red);
            //total += Vector3.Distance(navPoints[array[i]], navPoints[array[i - 1]]);

        }

        //Debug.Log(total);
    }*/
    
}
