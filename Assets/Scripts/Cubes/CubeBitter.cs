using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class CubeBitter : Cube
{
    private bool _isCollision = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "CubeNominal")
        {
            if (WeightId < 11 &&
                !_isCollision)
            {
                _isCollision = true;
                CubeNominal collisionCube = collision.transform.GetComponent<CubeNominal>();
                if (collisionCube.Weight == Weight)
                {
                    collisionCube.ConnectCubes();
                    Destroy(gameObject);
                }
                else
                {
                    GetComponent<CubeBitter>().enabled = false;
                    GetComponent<CubeNominal>().enabled = true;
                    GetComponent<CubeNominal>().IsNominal = true;
                    transform.tag = "CubeNominal";
                }
            }
        }
    }
}
