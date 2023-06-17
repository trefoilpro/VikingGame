using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class RandomPointGenerator: MonoBehaviour
{
    public static RandomPointGenerator Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }
    
    
    public Vector3 GetRandomPointOnNavMesh(float radius, Vector3 center)
    {
        Vector3 randomPoint = Vector3.zero;

        // Генерируем случайную точку вокруг текущей позиции
        Vector3 randomDirection = Random.insideUnitSphere * 10f;
        randomDirection += center;

        NavMeshHit hit;

        // Проверяем, есть ли точка на NavMesh
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
        {
            randomPoint = hit.position;
        }

        return randomPoint;
    }
    

    
}
