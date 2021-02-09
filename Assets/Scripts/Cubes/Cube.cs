using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public int Weight;
    public int WeightId;

    public CubesStorage CubesStorage;

    private void Start()
    {
        CubesStorage = GameObject.FindWithTag("CubesStorage").GetComponent<CubesStorage>();
    }

    public virtual void ConnectCubes()
    {
        Weight *= 2;
        WeightId += 1;
        GetComponent<MeshRenderer>().material = CubesStorage.CubesMaterials[WeightId];

        if (Weight == 2048) GameProcess.GameEnd();
    }
}
