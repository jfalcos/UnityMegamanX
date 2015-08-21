using UnityEngine;
using System.Collections;

public class PlatformEvent : MonoBehaviour
{
	public int[,] arr = {
		{2,1,3},
		{7,3,9},
		{1,9,5}
	};

	void Start()
	{
	}

	void OnGUI()
	{
		if(GUILayout.Button("Do1"))
		{
			Do ();
		}
	}

	void Do()
	{		
		int[,] arr2 = new int[arr.GetLength (1), arr.GetLength (0)];
		
		for(int a = 0; a < arr2.GetLength(0); ++a)
		{
			for(int b = 0; b < arr2.GetLength(1); ++b)
			{
				arr2[a,b] = arr[b,a];
			}
		}

		for(int a = 0; a < arr2.GetLength(0); ++a)
		{
			for(int b = 0; b < arr2.GetLength(1); ++b)
			{
				arr[a,b] = arr2[a,b];
			}
		}
		
		int lastElement = arr.GetLength (1) - 1;
		for(int a = 0; a < arr.GetLength(0); ++a)
		{
			lastElement = arr.GetLength (1) - 1;
			for(int b = 0; b < arr.GetLength(1); ++b)
			{
				arr2[a,b] = arr[a,lastElement--];
			}
		}
		
		for(int a = 0; a < arr2.GetLength(0); ++a)
		{
			for(int b = 0; b < arr2.GetLength(1); ++b)
			{
				print ("(" + a + "," + b + ") = " + arr2[a,b]);
			}
		}
		/*
			[1,0] = [0,1]
		 */
	}
}
