using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {    

    public IEnumerator ShakeCamera(float duration, float magnitude) {
        Vector3 startPos = transform.localPosition;

        float timeElapsed = 0.0f;

        while (timeElapsed < duration) {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(transform.localPosition.x + x, transform.localPosition.y + y, startPos.z);

            timeElapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = startPos;
    }


}
