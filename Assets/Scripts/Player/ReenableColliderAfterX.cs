using System.Collections;
using UnityEngine;

public class ReenableColliderAfterX : MonoBehaviour
{
    [SerializeField]
    Collider colliderToEnable;

    [SerializeField]
    float time = 0.2f;

    Coroutine coroutine = null;

    public void StartCoroutine()
    {
        if (coroutine != null) return;
        coroutine = StartCoroutine(ReenableAfterX());
    }

    IEnumerator ReenableAfterX()
    {
        colliderToEnable.enabled = false;
        yield return new WaitForSeconds(time);
        colliderToEnable.enabled = true;
    }

}
