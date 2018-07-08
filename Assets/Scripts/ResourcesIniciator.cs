using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesIniciator : MonoBehaviour {

	// Use this for initialization
	void Awake () 
	{
		DefaultResources.Init ();
	}
}
