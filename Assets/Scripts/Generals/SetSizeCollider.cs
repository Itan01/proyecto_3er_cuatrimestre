using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSizeCollider
{
    private BoxCollider _triggerCollider;
    private CapsuleCollider _collider;
    public SetSizeCollider(BoxCollider TriggerCollider, CapsuleCollider Collider)
    {
        _triggerCollider=TriggerCollider;
        _collider=Collider;
    }

    public void SetSize(float ySize)
    {
        float Center = ySize/2, Size=0.3f;
        _triggerCollider.size = new Vector3(Size*2, ySize, Size * 2);
        _triggerCollider.center = new Vector3(0, Center, 0);
        _collider.radius = Size;
        _collider.height =ySize;
        _collider.center = new Vector3(0, Center, 0);

    }
}
