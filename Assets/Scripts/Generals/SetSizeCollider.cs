using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSizeCollider
{
    private CapsuleCollider _capsuleCollider;
    private BoxCollider _boxCollider;
    public SetSizeCollider(CapsuleCollider Capsule, BoxCollider Box)
    {
        _capsuleCollider = Capsule;
        _boxCollider= Box;
    }

    public void SetSize(float ySize)
    {
        float Center = ySize/2, Size=0.3f;
        _capsuleCollider.radius = Size;
        _capsuleCollider.height =ySize;
        _capsuleCollider.center = new Vector3(0, Center, 0);
        _boxCollider.center = new Vector3(0, Center, 0); ;
        _boxCollider.size = new Vector3(Size, ySize, Size);

    }
}
