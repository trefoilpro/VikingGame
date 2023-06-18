using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MainMenuBackGround : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private GameObject _mapCenter;
    [SerializeField] private Camera _camera;
    
    private float _radius = 200f; 
    private float _duration = 60f;
    
   
    public void CreateBackGround()
    {
        _enemySpawner.SpawnEnemy(30, 1, 1, _mapCenter.transform.position, 250f);
        StartCoroutine(BackGroundFlyingCamera());
    }
    
    

    IEnumerator BackGroundFlyingCamera()
    {
        Vector3 firstPosition = _mapCenter.transform.position + new Vector3(0, 120f, -_radius);
        Vector3 secondPosition = _mapCenter.transform.position + new Vector3(-_radius, 120f, 0);
        Vector3 thirdPosition = _mapCenter.transform.position + new Vector3(0, 120f, _radius);
        Vector3 fourthPosition = _mapCenter.transform.position + new Vector3(_radius, 120f, 0);

        Vector3[] pathPoints = { firstPosition, secondPosition, thirdPosition, fourthPosition, firstPosition };

        if (Vector3.Distance(_camera.transform.position, firstPosition) > 0.1f)
        {
            _camera.transform.DOMove(firstPosition, 1f).SetEase(Ease.Linear);
            yield return new WaitForSeconds(1f);
        }

        _camera.transform.DOPath(pathPoints, _duration, PathType.CatmullRom).SetEase(Ease.Linear);
        
        _camera.transform.DORotate(_camera.transform.rotation.eulerAngles + new Vector3(0, 180f, 0), _duration / 2).SetEase(Ease.Linear);
        yield return new WaitForSeconds(_duration / 2);
        _camera.transform.DORotate(_camera.transform.rotation.eulerAngles + new Vector3(0, 180f, 0), _duration / 2).SetEase(Ease.Linear);
        yield return new WaitForSeconds(_duration / 2);

        yield return StartCoroutine(BackGroundFlyingCamera());

    }
}