using UnityEngine;
public class DoorTemp : MonoBehaviour
{
    [SerializeField, Tooltip("DieCutting‚ð“o˜^")]
    DieCutting dc_;

    void Update()
    {
        if (dc_.Is_complete)
        {
            move();
        }
    }

    void move()
    {
        transform.position += new Vector3(0, 0.1f, 0);

    }
}
