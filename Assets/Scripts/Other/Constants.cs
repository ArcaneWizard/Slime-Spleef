using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants 
{
    public static Vector3 SlimeCenterOffsetFromSprite = new Vector3(0, -0.655f, 0);
    public static Quaternion WorldPlaneRotation = Quaternion.Euler(new Vector3(45f, 0, 0));
    public static Vector3 WorldGravity = 21f * new Vector3(0, -0.707f, 0.707f);

    public static Vector2 MapXBounds = new Vector2(-40f, 40f);
    public static Vector2 MapYBounds = new Vector2(-40f, 40f);

    public static LayerMask PuddleLM = 1 << 8;
    public static LayerMask FoodLM = 1 << 10;
}