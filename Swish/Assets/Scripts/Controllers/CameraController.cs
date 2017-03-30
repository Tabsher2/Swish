using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    static Camera mainCam;
    static float transitionDuration = 0.5f;
    static Vector3 birdView = new Vector3(0, 20, 0);
    static Vector3 satellite = new Vector3(90, 0, 270);
    static Vector3 hoopLocation = new Vector3(10, 1, 0);

	// Use this for initialization
	void Start () {
        mainCam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        if (mainCam.transform.position.y == 20)
        {
            mainCam.transform.eulerAngles = satellite;
            GameController.slideCameraUp = false;
        }
    }

    public static float GetCameraDirection()
    {
        return Camera.main.transform.eulerAngles.y;
    }

    public static void MoveToShotSelection(Vector3 startPos, Vector3 startAngle)
    {
        //0.03345f
        if (Vector3.Distance(mainCam.transform.position, birdView) > 0.05f)
            mainCam.transform.position = Vector3.Lerp(startPos, birdView, Time.deltaTime * (Time.timeScale / transitionDuration));
        else
            mainCam.transform.position = birdView;
        if (Vector3.Distance(mainCam.transform.eulerAngles, satellite) > 0.05f)
            mainCam.transform.eulerAngles = Vector3.Slerp(startAngle, satellite, Time.deltaTime * (Time.timeScale / transitionDuration));
        else
            mainCam.transform.eulerAngles = satellite;
    }

    public static void MoveToSelectedSpot(Vector3 startPos, Vector3 startAngle, Vector3 endPos, Vector3 endAngle)
    {
        //0.03345f
        if (Vector3.Distance(mainCam.transform.position, endPos) > 0.05f)
            mainCam.transform.position = Vector3.Lerp(startPos, endPos, Time.deltaTime * (Time.timeScale / transitionDuration));
        else
        {
            mainCam.transform.position = endPos;
            LocationSelector.slideCameraDown = false;
        }
        if (Vector3.Distance(mainCam.transform.eulerAngles, endAngle) > 0.05f)
            mainCam.transform.eulerAngles = Vector3.Slerp(startAngle, endAngle, Time.deltaTime * (Time.timeScale / transitionDuration));
        else
            mainCam.transform.eulerAngles = endAngle;
    }

}
