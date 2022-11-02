using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Cube : MonoBehaviour
{
    //path
    public List<Vector3> pathList;
    
    //bool/events
    public bool isGoal;
    public bool isFail;
    public UnityEvent endMovementEvent;
    
    //hide options
    private float _speed = 5;
    private Tweener _moveTween;
    
    private IEnumerator MoveStep(Vector3 toPosition, float time) //это один маленький шаг для человека...
    {
        var toPositionNew = new Vector3(toPosition.x, 2, toPosition.z);
        
        _moveTween?.Kill();
        _moveTween = transform.DOMove(toPositionNew, time).SetEase(Ease.InOutCubic);
        
        yield return _moveTween.WaitForCompletion();
    }

    private IEnumerator MoveAllPath() //, но гигантский скачок для всего человечества
    {
        if (isFail || isGoal) yield break; 

        var gridManager = AllSingleton.Instance.gridController;
        var pathListCount = pathList.Count;
        
        for (int i = 0; i < pathListCount; i++)
        {
            //ищем соседнюю клетку в пути
            var pathStep = pathList.Find(path => Vector3.Distance(transform.position, path) < 10); 

            if (pathStep != Vector3.zero) //если есть такая
            {
                //подготовка
                var distance = Vector3.Distance(transform.position, pathStep);
                var time = distance / _speed;

                //ожидание прибытия
                yield return MoveStep(pathStep, time);
                
                //после прибытия очищаем карту и путь
                gridManager.RemovePathTile(gridManager.GetCellCenterFromWorldPosition(pathStep), true);
                pathList.Remove(pathStep);

                if (gridManager.CheckGoal(pathStep, this)) //проверка на достижение цели
                {
                    foreach (var path in pathList)
                    {
                        gridManager.RemovePathTile(gridManager.GetCellCenterFromWorldPosition(path), false);
                    }
                    pathList.Clear();
                    endMovementEvent?.Invoke();
                    yield break;
                }
            }
        }
        
        // не добрался
        isFail = true;
        endMovementEvent?.Invoke();
    }

    public void StartMove() //обертка для запуска движения
    {
        StopAllCoroutines();
        StartCoroutine(MoveAllPath());
    }
}
