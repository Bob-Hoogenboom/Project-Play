using UnityEngine;

/// <summary>
///A script to paste on every object that the player can pick-up (basicly everything)
/// </summary>

//A better method to parse this data?
[SelectionBase]
public class PickUpOBJ : MonoBehaviour
{
    public float objectGetValue;
    public Sprite objectImage;
    public Vector3 objectSize = new Vector3(1f,1f,1f);

    private Vector3 _objectCenter;

    private void OnDrawGizmos()
    {
        var pos = transform.position;
        var centerY = pos.y + (objectSize.y / 2);

        _objectCenter = new Vector3(pos.x, centerY, pos.z);
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(_objectCenter, objectSize);
    }
}
