using System;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    [Space]
    public Cube currentCube;
    [Space]
    public List<Cube> cubes;

    private void OnEnable()
    {
        foreach (var cube in cubes)
        {
            cube.endMovementEvent.AddListener(CheckEnding);
        }
    }

    private void OnDestroy()
    {
        foreach (var cube in cubes)
        {
            cube.endMovementEvent.RemoveAllListeners();
        }
    }

    public void CheckEnding()
    {
        var countGoal = 0;
        var countFail = 0;
        
        foreach (var cube in cubes)
        {
            if (cube.isGoal)
                countGoal++;
            if (cube.isFail)
                countFail++;
        }

        if (countGoal + countFail >= 2)
        {
            AllSingleton.Instance.gameManager.EndGame(countGoal);
        }
    }
    
    public void ChangeCurrentCube()
    {
        Camera.main.transform.RotateAround(Vector3.zero, Vector3.up, 180);
        currentCube = cubes.Find(cube => cube != currentCube);

        foreach (var cube in cubes)
        {
            cube.transform.Rotate(0, 180, 0, Space.Self);
        }
    }
    
    public void StartMove()
    {
        foreach (var cube in cubes)
        {
            cube.StartMove();
        }
    }
}
