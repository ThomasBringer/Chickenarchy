using UnityEngine;

public class Cam : MonoBehaviour
{
    public static Cam cam;
    public static Camera recorder;
    public float speed = 2;
    public float focusTime = 2;

    Vector2 mouseOrigin;
    Vector2 camOrigin;

    void Awake()
    {
        cam = this;
        recorder = GetComponent<Camera>();
    }

    void Update()
    {
        if (GameManager.speaking) return;

        if (Input.GetMouseButtonDown(0))
        {
            mouseOrigin = Input.mousePosition;
            camOrigin = transform.position;

            return;
        }

        if (Input.GetMouseButton(0))
        {
            transform.position = (camOrigin - speed * (Vector2)recorder.ScreenToViewportPoint((Vector2)Input.mousePosition - mouseOrigin));
        }
    }

    void LateUpdate()
    {
        RoundCamPos();
    }

    public static Vector2 MousePos { get { return recorder.ScreenToWorldPoint(Input.mousePosition); } }

    public static void Focus(Vector3 target)
    {
        cam.StartCoroutine(cam.transform.TranslateLerp(target, cam.focusTime, GameManager.gm.curve));
    }

    static void RoundCamPos()
    {
        cam.transform.position = ((Vector2)cam.transform.position).Round();
    }
}