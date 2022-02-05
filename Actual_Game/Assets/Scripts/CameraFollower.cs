using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    
    void Update()
    {
        if (target != null)
        {
            Vector3 newPos = Vector3.Lerp(this.transform.position, target.position, smoothing * Time.deltaTime);
            newPos.z = this.transform.position.z;
            this.transform.position = newPos;
        } 
    }
}
