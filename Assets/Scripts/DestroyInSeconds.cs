using UnityEngine;

public class DestroyInSeconds : MonoBehaviour
{
 [SerializeField]
 private float secondsToDestroy = 5f;
 private void Start()
 {
    Destroy(gameObject, secondsToDestroy);
 }
}
