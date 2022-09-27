using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerritoryControllerScript : MonoBehaviour{
	private Transform tr;
	[SerializeField] private Transform cornerLeftTop, cornerRightBottom;
	[SerializeField] private float scale;
	void Awake(){
		tr = base.transform;
		Vector3 currentScale = tr.localScale;
		currentScale.x *= scale;
		currentScale.z *= scale;
		tr.localScale = currentScale;
	}
	public Vector3 GetRandomPosition(){
		Vector3 result = new Vector3(0, tr.position.y + 1, 0);
		result.x = UnityEngine.Random.Range(cornerLeftTop.position.x, cornerRightBottom.position.x);
		result.z = UnityEngine.Random.Range(cornerLeftTop.position.z, cornerRightBottom.position.z);
		return result;
	}
}
