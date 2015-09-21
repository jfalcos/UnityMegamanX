using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Camera))]
public class CameraBoundaries : MonoBehaviour
{
	private Camera myCamera = null;
	private Vector3 limitVector = Vector3.zero; //Use to set the position when the camera is out of bounds.
	public float minX = 0f;
	public float maxX = 0f;
	public float minY = 0f;
	public float maxY = 0f;
	public float orthographicSize = 0f;
	public bool minXEnabled = false;
	public bool maxXEnabled = false;
	public bool minYEnabled = false;
	public bool maxYEnabled = false;
	public bool modifyCameraSize = false;

	void Awake()
	{
		myCamera = GetComponent<Camera> ();
	}

	void LateUpdate()
	{
		limitVector = myCamera.transform.position;

		if(myCamera.transform.position.x <= minX && minXEnabled)
		{
			limitVector.x = minX;
		}
		else if(myCamera.transform.position.x >= maxX && maxXEnabled)
		{
			limitVector.x = maxX;
		}

		if(myCamera.transform.position.y <= minY && minYEnabled)
		{
			limitVector.y = minY;
		}
		else if(myCamera.transform.position.y >= maxY && maxYEnabled)
		{
			limitVector.y = maxY;
		}

		if(modifyCameraSize)
		{
			myCamera.orthographicSize = orthographicSize;
		}

		myCamera.transform.position = limitVector;
	}
}
