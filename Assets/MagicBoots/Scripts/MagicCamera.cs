using UnityEngine;
using System.Collections;

public class MagicCamera : MonoBehaviour {
		
	public Transform targetCharacter;	
	
	public float posDamp; //2
	public float rotDamp; //3
	public float height; //2
	public float distance; //5
	
	private Vector3 targetPos;
	
	
	
	// Update is called once per frame
	void Update () {
		
		if (!targetCharacter) return;		
		
		targetPos = targetCharacter.position - targetCharacter.forward * distance + targetCharacter.up * height;
		
		transform.position = Vector3.Lerp(transform.position, targetPos, posDamp * Time.deltaTime);
		
		transform.rotation = Quaternion.Slerp(transform.rotation, targetCharacter.rotation, rotDamp * Time.deltaTime );				
     	
	}		
	

}
 