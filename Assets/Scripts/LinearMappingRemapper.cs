using UnityEngine;
using Valve.VR.InteractionSystem;

public class LinearMappingRemapper : LinearMapping
{
    public AnimationCurve remapping;
    public override float Value { get { return remapping.Evaluate(value); } }
}
