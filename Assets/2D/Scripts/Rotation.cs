using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotation : MonoBehaviour
{
    public float RotateSpeed = -90;


    private Transform thisTrans;
    void Start()
    {
        thisTrans = transform;
    }

    void Update()
    {
        thisTrans.transform.Rotate(0, 0, RotateSpeed * Time.deltaTime);
    }
}
