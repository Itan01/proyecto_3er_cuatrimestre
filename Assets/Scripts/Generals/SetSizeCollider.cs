using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSizeCollider
{
    private CapsuleCollider _collider;
    public SetSizeCollider(CapsuleCollider Collider)
    {
        _collider=Collider;
    }

    public void SetSize(float ySize)
    {
        float Center = ySize/2, Size=0.3f;
        _collider.radius = Size;
        _collider.height =ySize;
        _collider.center = new Vector3(0, Center, 0);

    }
}
