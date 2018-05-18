using System.Collections;
using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float maxSize = 0;

    public float timer;
    public bool startTimer;

    private Vector3 Origin; 
    private Vector3 Diference;

    public BoxCollider2D boundBox;
    private Vector3 minBounds;
    private Vector3 maxBounds;

    private Camera theCamera;
    private float halfHeight;
    private float halfWidth;

    float orthoZoomSpeed = 0.05f;

    void Start()
    {
        minBounds = boundBox.bounds.min;
        maxBounds = boundBox.bounds.max;
    }

    void LateUpdate()
    {
        if (!FieldCreator.askWindowIsOpen)
        {
            GameObject.Find("Main Camera").GetComponent<AudioSource>().volume =
		    (PlayerPrefs.GetString("Sound") == "true") ? 1 : 0;
		
            theCamera = GetComponent<Camera>();
            halfHeight = theCamera.orthographicSize;
            halfWidth = halfHeight * Screen.width / Screen.height;

            if (startTimer) timer += Time.deltaTime;
            else timer = 0;

            if (Input.GetMouseButtonDown(0))
            {
                Origin = MousePos();
            }
            if (Input.GetMouseButton(0))
            {
                Diference = MousePos() - transform.position;
                transform.position = Origin - Diference;
            }

            float clampedX = Mathf.Clamp(transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
            float clampedY = Mathf.Clamp(transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);

            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0), touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                gameObject.GetComponent<Camera>().orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
                gameObject.GetComponent<Camera>().orthographicSize = Mathf.Max(gameObject.GetComponent<Camera>().orthographicSize, 0.1f);

                if (gameObject.GetComponent<Camera>().orthographicSize > maxSize) gameObject.GetComponent<Camera>().orthographicSize = maxSize;
                if (gameObject.GetComponent<Camera>().orthographicSize < 3) gameObject.GetComponent<Camera>().orthographicSize = 3f;
            }
        }
    }

    Vector3 MousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void setBounds(BoxCollider2D newBounds)
    {
        boundBox = newBounds;

        minBounds = boundBox.bounds.min;
        maxBounds = boundBox.bounds.max;
    }
}
