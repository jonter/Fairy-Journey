using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] float scale = 0.1f;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float cameraX = cam.transform.position.x;
        float offsetX = -cameraX * scale;
        transform.localPosition = new Vector3(offsetX, 0, 20);
    }
}
