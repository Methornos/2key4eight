using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class CubeNominal : Cube
{
    public bool IsNominal = true;

    private bool _isOver = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "CubeNominal" &&
            WeightId < 11 &&
            !collision.transform.GetComponent<CubeBitter>())
        {
            CubeNominal collisionCube = collision.transform.GetComponent<CubeNominal>();
            if (collisionCube.Weight == Weight)
            {
                collisionCube.ConnectCubes();
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "GoChecker" &&
            IsNominal &&
            !_isOver)
        {
            _isOver = true;
            GameProcess.GameOver();
        }
        
    }

    public override void ConnectCubes()
    {
        base.ConnectCubes();
        GetComponent<Rigidbody>().AddForce(Vector3.up * 400);
        GetComponent<Rigidbody>().AddForce(Vector3.right * Random.Range(-150, 151));
    }
}
