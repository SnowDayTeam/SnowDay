using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarDriveToGizmo : MonoBehaviour {
    public Color gizmoColor = Color.green;
    public void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawCube(transform.position, Vector3.one);
    }
}
