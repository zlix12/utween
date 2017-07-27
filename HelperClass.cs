using UnityEngine;
using System.Collections;

public class HelperClass : MonoBehaviour {
	#region Sprite Based
	/// <summary>
	/// Tweens the sprite alpha.
	/// </summary>
	/// <returns>Nothing.</returns>
	/// <param name="sprite">Sprite.</param>
	/// <param name="desiredAlpha">Desired alpha.</param>
	/// <param name="speed">Tween speed.</param>
	/// <param name="destroyOnFinish">If set to <c>true</c> destroy tweened object on finish.</param>
	public static IEnumerator TweenSpriteAlpha (SpriteRenderer sprite, float desiredAlpha, float speed, bool destroyOnFinish) {
		Color wantedColor = sprite.color;
		wantedColor.a = desiredAlpha;

		while (sprite != null && sprite.color != wantedColor) {
			sprite.color = Vector4.Lerp (sprite.color, wantedColor, Time.deltaTime * speed);
			yield return null;
		}

		if (destroyOnFinish) {
            if (sprite != null)
            {
                Destroy(sprite.gameObject);
            }
		}
	}
	#endregion

	#region Transform Based

	#region Tween Positions
	/// <summary>
	/// Tweens the object position.
	/// </summary>
	/// <returns>Nothing.</returns>
	/// <param name="objectTransform">Object transform.</param>
	/// <param name="startPos">Start position.</param>
	/// <param name="endPos">End position.</param>
	/// <param name="speed">Speed.</param>
	/// <param name="delay">Delay.</param>
	/// <param name="deactivateOnFinish">If set to <c>true</c> deactivate on finish.</param>
	public static IEnumerator TweenObjectPosition (Transform objectTransform, Vector3 startPos, Vector3 endPos, float speed, float delay, bool deactivateOnFinish) {
		yield return new WaitForSeconds (delay);

		objectTransform.position = startPos;

		while (objectTransform.position != endPos) {
			objectTransform.position = Vector3.Lerp (objectTransform.position, endPos, Time.deltaTime * speed);
			yield return null;
		}

		if (deactivateOnFinish) {
			objectTransform.gameObject.SetActive (false);
		}
	}

	/// <summary>
	/// Tweens the object local position.
	/// </summary>
	/// <returns>The object position local.</returns>
	/// <param name="objectTransform">Object transform.</param>
	/// <param name="startPos">Start position.</param>
	/// <param name="endPos">End position.</param>
	/// <param name="speed">Speed.</param>
	/// <param name="delay">Delay.</param>
	/// <param name="deactivateOnFinish">If set to <c>true</c> deactivate on finish.</param>
	public static IEnumerator TweenObjectPositionLocal (Transform objectTransform, Vector3 startPos, Vector3 endPos, float speed, float delay, bool deactivateOnFinish) {
		yield return new WaitForSeconds (delay);

		objectTransform.localPosition = startPos;

		while (objectTransform.localPosition != endPos) {
			objectTransform.localPosition = Vector3.Lerp (objectTransform.localPosition, endPos, Time.deltaTime * speed);
			yield return null;
		}

		if (deactivateOnFinish) {
			objectTransform.gameObject.SetActive (false);
		}
	}
	#endregion 

    public static Quaternion TwoDRotationTo (Vector3 startPos, Vector3 toPosition, float offset)
    {
        Vector3 dirToWanted = (startPos - toPosition);
        dirToWanted.Normalize();
        float rotZ = Mathf.Atan2(dirToWanted.y, dirToWanted.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, rotZ + offset);
    }
	#endregion

	#region UI Based
	#region Tween Alphas
	/// <summary>
	/// Tweens the canvas group alpha.
	/// </summary>
	/// <returns>The canvas group alpha.</returns>
	/// <param name="canvasGroup">Canvas group.</param>
	/// <param name="startAlpha">Start alpha.</param>
	/// <param name="endAlpha">End alpha.</param>
	/// <param name="speed">Speed.</param>
	/// <param name="delay">Delay.</param>
	/// <param name="callBackObject">Call back object.</param>
	/// <param name="callBack">Call back.</param>
	/// <param name="deactivateOnFinish">If set to <c>true</c> deactivate on finish.</param>
	public static IEnumerator TweenCanvasGroupAlpha (CanvasGroup canvasGroup, float startAlpha, float endAlpha, float speed, float delay, GameObject callBackObject, string callBack, bool deactivateOnFinish) {
		canvasGroup.alpha = startAlpha;

		yield return new WaitForSeconds (delay);

        float distanceFrom = Mathf.Abs(canvasGroup.alpha - endAlpha);
		while (distanceFrom > 0.02F) {
            distanceFrom = Mathf.Abs(canvasGroup.alpha - endAlpha);
            canvasGroup.alpha = Mathf.Lerp (canvasGroup.alpha, endAlpha, Time.deltaTime * speed);
			yield return null;
		}

		if (callBackObject != null) {
			callBackObject.SendMessage (callBack, SendMessageOptions.DontRequireReceiver);
		}

		if (deactivateOnFinish) {
			canvasGroup.gameObject.SetActive (false);
		}
	}
	#endregion
	#endregion
}
