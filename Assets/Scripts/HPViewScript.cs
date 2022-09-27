using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPViewScript : MonoBehaviour{
	[SerializeField] private Slider slider;
	int maxValue = 1;
	public void SetMaxHP(int maxValue){
		this.maxValue = maxValue;
		slider.value = 1;
	}
	public void SetCurrentHP(int value){
		slider.value = ((float) value / (float) maxValue);
	} 
}
