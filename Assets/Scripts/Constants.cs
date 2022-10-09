using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants 
{
    public static Vector3 SlimeCenterOffsetFromSprite = new Vector3(0, -0.655f, 0);
    public static Quaternion WorldPlaneRotation = Quaternion.Euler(new Vector3(45f, 0, 0));
    public static Vector3 WorldGravity = 4.9f * new Vector3(0, -0.707f, 0.707f);
}