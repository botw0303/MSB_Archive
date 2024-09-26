using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurnAction 
{
	public IEnumerator Execute();
	public bool CanUse();
}
