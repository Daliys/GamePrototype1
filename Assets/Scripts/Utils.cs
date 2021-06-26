using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 mouseWorldPosition;

    // Update is called once per frame

    private void Update()
    {
        WorldMousePosition();
    }

    private void WorldMousePosition()
    {
        Plane plane = new Plane(Vector3.up, 0);
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       
        if (plane.Raycast(ray, out distance)) mouseWorldPosition = ray.GetPoint(distance);
    }
}
