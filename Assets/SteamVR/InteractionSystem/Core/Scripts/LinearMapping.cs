//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: A linear mapping value that is used by other components
//
//=============================================================================

using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	public class LinearMapping : MonoBehaviour
	{
        protected float value;
        public virtual float Value { get { return value; } set { this.value = value; } }
	}
}
