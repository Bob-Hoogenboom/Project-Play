using System;
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

    [SerializeField] private Material glowMaterial;
    [SerializeField] private Material originalMaterial;
    
    private bool _isGlowing;

    private void HighLight()
    {
        if (_isGlowing)
        {
            gameObject.GetComponentInChildren<Renderer>().material = originalMaterial;
        }
        else
        {
            gameObject.GetComponentInChildren<Renderer>().material = glowMaterial;
        }

        _isGlowing = !_isGlowing;
    }
    
    public void ToggleHighligh(bool state)
    {
        if (_isGlowing == state) return;
        _isGlowing = !state;
        HighLight();
    }
}
