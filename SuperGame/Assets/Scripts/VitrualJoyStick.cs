using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class VitrualJoyStick : MonoBehaviour, IDragHandler,IPointerUpHandler,IPointerDownHandler {

	private Image joyStickImg;
	private Image joyBkgImg;
	public Vector3 inputVector;

	private void Start(){
		joyBkgImg = GetComponent<Image> ();
		joyStickImg = transform.GetChild(0).GetComponent<Image> (); 
	}

	public virtual void OnDrag(PointerEventData ped){
		Vector2 pos;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle (
			joyBkgImg.rectTransform, ped.position, ped.pressEventCamera, out pos)) {
		
			pos.x = pos.x / joyBkgImg.rectTransform.sizeDelta.x;
			pos.y = pos.y / joyBkgImg.rectTransform.sizeDelta.y;

			pos.x = (joyBkgImg.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
			pos.y = (joyBkgImg.rectTransform.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;

			inputVector = new Vector3 (pos.x, pos.y, 0);
			inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

			joyStickImg.rectTransform.anchoredPosition = new Vector3 (
				inputVector.x * joyBkgImg.rectTransform.sizeDelta.x / 3,
				inputVector.y * joyBkgImg.rectTransform.sizeDelta.y / 3,
				inputVector.z);
		}
			
	}

	public virtual void OnPointerUp(PointerEventData ped){
		inputVector = Vector3.zero;
		joyStickImg.rectTransform.anchoredPosition = Vector3.zero;
	}

	public virtual void OnPointerDown(PointerEventData ped){
		OnDrag(ped);
	}
}
