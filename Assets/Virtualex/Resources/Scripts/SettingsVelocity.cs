using UnityEngine;
using System.Collections;
using System;
using DarkerSmile;
using System.Collections.Generic;

[Serializable]
public class SettingsVelocity
{
	[SerializeField] public int NumSmoothingFrames=5;
	[SerializeField] public Vector3 ThrowScale = new Vector3(0.6f,0.6f,0.6f);
	[SerializeField] public bool UseAngularVelocity = true;
}
