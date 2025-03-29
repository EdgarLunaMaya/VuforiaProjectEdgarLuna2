using UnityEngine;

public class InstantiateShapeObject : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToInstantiate;
    public void Instantiate(Transform objectTransform)
    {
        Instantiate(objectToInstantiate, objectTransform.position, Quaternion.identity);
    }
}
