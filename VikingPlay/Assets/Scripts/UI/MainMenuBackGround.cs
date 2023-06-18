using DG.Tweening;
using UnityEngine;

public class MainMenuBackGround : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private GameObject _mapCenter;
    [SerializeField] private Camera _camera;
    
    private float _radius = 200f; 
    private float _duration = 60f;
    private Sequence _cameraAnimation;
    
   
    public void CreateBackGround()
    {
        _enemySpawner.SpawnEnemy(30, 1, 1, _mapCenter.transform.position, 250f);
        
        Vector3 firstPosition = _mapCenter.transform.position + new Vector3(0, 120f, -_radius);
        Vector3 secondPosition = _mapCenter.transform.position + new Vector3(-_radius, 120f, 0);
        Vector3 thirdPosition = _mapCenter.transform.position + new Vector3(0, 120f, _radius);
        Vector3 fourthPosition = _mapCenter.transform.position + new Vector3(_radius, 120f, 0);

        Vector3[] firstPathPoints = { firstPosition, secondPosition, thirdPosition };
        Vector3[] secondPathPoints = { thirdPosition, fourthPosition, firstPosition };

        _cameraAnimation = DOTween.Sequence();

        if (Vector3.Distance(_camera.transform.position, firstPosition) > 0.1f)
        {
            _cameraAnimation.Append(_camera.transform.DOMove(firstPosition, 1f).SetEase(Ease.Linear));
        }

        _cameraAnimation.Append(_camera.transform.DOPath(firstPathPoints, _duration / 2, PathType.CatmullRom).SetEase(Ease.Linear))
            .Join(_camera.transform.DORotate(_camera.transform.rotation.eulerAngles + new Vector3(0, 180f, 0), _duration / 2).SetEase(Ease.Linear))
            .Append(_camera.transform.DOPath(secondPathPoints, _duration / 2, PathType.CatmullRom).SetEase(Ease.Linear))
            .Join(_camera.transform.DORotate(_camera.transform.rotation.eulerAngles + new Vector3(0, 360f, 0), _duration / 2).SetEase(Ease.Linear));
        
        _cameraAnimation.SetLoops(-1);
    }

    public void StopAnimation()
    {
        _cameraAnimation.Kill();
    }
}