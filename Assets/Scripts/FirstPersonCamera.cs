using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField]
    public float xSensitivity = .8f;
    [SerializeField]
    public float ySensitivity = .6f;
    [SerializeField]
    public float smoothRate = 2f;
    public GameObject player;
    private Vector2 mouseMovement;
    private Vector2 lerpedMouseMovement;
    // Start is called before the first frame update
    void Start()
    {
        player = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseDelta = Vector2.Scale(
            mouseDelta,
            new Vector2(xSensitivity * smoothRate, ySensitivity * smoothRate)
        );
        lerpedMouseMovement.x = Mathf.Lerp(lerpedMouseMovement.x, mouseDelta.x, 1f / smoothRate);
        lerpedMouseMovement.y = Mathf.Lerp(lerpedMouseMovement.y, mouseDelta.y, 1f / smoothRate);
        mouseMovement += lerpedMouseMovement;

        // TODO need to clamp the y rotation
        transform.localRotation = Quaternion.AngleAxis(-mouseMovement.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(mouseMovement.x, player.transform.up);
    }
}
