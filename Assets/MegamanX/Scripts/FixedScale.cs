using UnityEngine;
using System.Collections;

public class FixedScale : MonoBehaviour
{
	void OnWillRenderObject()
	{
		if(transform.lossyScale.x < 0)
		{
			transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
		}
	}
}
