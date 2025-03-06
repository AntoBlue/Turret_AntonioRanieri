using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField][Range(0.1f, 10f)] private float movementSpeed = 0.5f;
    [SerializeField][Range(1f, 180f)] private float rotationSpeed = 45f;
    [SerializeField][Range(1f, 10f)] private float mouseSpeed = 8f;
    [SerializeField][Range(1f, 10f)] private float turboSpeed = 2f;

    [SerializeField] bool useMouseLook = true;
    [SerializeField] CursorLockMode useLockState = CursorLockMode.Locked;

    private float _turbo;
    private float _h;
    private float _v;

    private void Start()
    {
        if(useMouseLook)
        {
            Cursor.lockState = useLockState;
        }
    }

    void Update()
    {
        _turbo = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? turboSpeed : 1;

        float mouse = Input.GetAxis("Mouse X");

        //Debug.LogWarning (Mouse)

        _h = useMouseLook ? mouse : Input.GetAxis("Horizontal");
        _v = Input.GetAxis("Vertical");

        float xDirection = useMouseLook ? Input.GetAxis("Horizontal") : 0;
        float zDirection = _v * movementSpeed;

        Vector3 direction = new Vector3( xDirection, 0, zDirection).normalized * (_turbo * Time.deltaTime); //Move

        transform.Translate(direction);

        if(useMouseLook )
        {
            transform.Rotate(new Vector3(0, mouse * mouseSpeed * Time.deltaTime, 0));
        }

        else // left/right, up/down
        {
            transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime * _h * _turbo));
        }
    }
}
