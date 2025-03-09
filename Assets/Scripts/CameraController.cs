using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    [SerializeField][Range(0.1f, 99999f)] private float movementSpeed = 700f;
    [SerializeField][Range(1, 1000)] private float rotationSpeed = 200;
    [SerializeField][Range(1, 1000)] private float mouseSpeed = 500f;
    [SerializeField][Range(1, 1000)] private float turboSpeed = 2f;

    [SerializeField] bool useMouseLook = true;
    [SerializeField] CursorLockMode useLockState = CursorLockMode.Locked;

    private float _turbo;
    private float _h;
    private float _v;

    void Start()
    {
        if (useMouseLook)
        {
            Cursor.lockState = useLockState;
        }
    }

    void Update()
    {
        _turbo = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? turboSpeed : 1;

        float mouse = Input.GetAxis("Mouse X");

        _h = useMouseLook ? mouse : Input.GetAxis("Horizontal");
        _v = Input.GetAxis("Vertical");

        float xDirection = useMouseLook ? Input.GetAxis("Horizontal") : 0;
        float zDirection = _v * movementSpeed;

        Vector3 direction = new Vector3(xDirection, 0, zDirection).normalized * (_turbo * Time.deltaTime);

        transform.Translate(direction);

        if (useMouseLook)
        {
            transform.Rotate(new Vector3(0, mouse * mouseSpeed * Time.deltaTime, 0));
        }
        else
        {
            transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime * _h * _turbo));
        }
    }

}
