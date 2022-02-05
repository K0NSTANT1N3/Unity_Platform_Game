using UnityEngine;
using UnityEngine.UI;

public class TrapFaller : MonoBehaviour
{
    
    public GameObject faller;
    public void Drop()
    {
        faller.transform.localScale = new Vector3(1, 1, 0);
        faller.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        faller.GetComponent<Rigidbody2D>().mass = 10;
        faller.GetComponent<Rigidbody2D>().gravityScale = 13;
        
    }
}
