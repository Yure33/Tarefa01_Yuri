using UnityEngine;

public class CameraMoveset_Script : MonoBehaviour
{
    public Transform Alvo;
    [SerializeField] float Suavidez;

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, Alvo.position, Suavidez);
    }
}
