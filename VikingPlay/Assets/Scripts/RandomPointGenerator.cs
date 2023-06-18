using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public static class RandomPointGenerator
{
    public static Vector3 GetRandomPointOnNavMesh(float radius, Vector3 center)
    {
        Vector3 randomPoint = Vector3.zero;

        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += center;

        NavMeshHit hit;
        
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
        {
            if(Vector3.Distance(center, hit.position) < 40f)
            {
                randomPoint = GetRandomPointOnNavMesh(radius, center);
            }
            else
            {
                randomPoint = hit.position;
            }
        }
        
        return randomPoint;
    }
}
