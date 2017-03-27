using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationSelector : MonoBehaviour {

    //Selection Token
    public GameObject selectionToken;
    private GameObject tokenInstance;
    private bool tokenDown = false;

    public GameObject bannerPanel;
    public GameObject bannerButton;
    public Text bannerText;

    public static Vector3 selectedLocation;
    public static Vector3 cameraLocation;
    public static Vector3 cameraAngle;
    public static bool slideCameraDown = false;
    public static bool allowSelection = false;
    private static bool displayInitialBanner = true;

    Vector3 clickPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (slideCameraDown)
            CameraController.MoveToSelectedSpot(Camera.main.transform.position, Camera.main.transform.eulerAngles, cameraLocation, cameraAngle);

        if (allowSelection && displayInitialBanner)
        {
            bannerPanel.SetActive(true);
            bannerButton.SetActive(false);
            bannerText.text = "   You are aiming for the top hoop.";
        }
	}

    private void OnMouseDown()
    {
        if (tokenDown)
            Destroy(tokenInstance);
        if (allowSelection)
        {
            clickPos = Input.mousePosition;
            clickPos.x -= (Screen.width / 2);
            clickPos.y -= (Screen.height / 2);
            float ourX = (clickPos.y / (0.435f * Screen.height)) * 10;
            float ourZ = -(clickPos.x / (0.325f * Screen.width)) * 5;
            selectedLocation = new Vector3(ourX, 0.7f, ourZ);
            tokenInstance = Instantiate(selectionToken, new Vector3(ourX, 0, ourZ), Quaternion.identity);
            tokenDown = true;
            displayInitialBanner = false;
            bannerPanel.SetActive(true);
            bannerButton.SetActive(true);
            bannerText.text = "Shoot from here?";
        }
    }

    public void SelectSpot()
    {
        Destroy(tokenInstance);
        GameController.spotSelected = true;
        bannerPanel.SetActive(false);
        Vector3 hoopLocation = new Vector3(9f, 2, 0);
        float distance = CalculateDistance(hoopLocation, selectedLocation);

        float yAngle = Mathf.Atan(1/distance);

        float angle = Mathf.Acos((9f - selectedLocation.x) / distance);
        float cameraX = Mathf.Cos(angle) * (distance + 1);
        float cameraZ = Mathf.Sin(angle) * (distance + 1);
        cameraX = 9f - cameraX;
        if (selectedLocation.z < 0)
        {
            cameraZ *= -1;
            angle *= -1;
        }
        cameraLocation = new Vector3(cameraX, 1, cameraZ);
        angle *= Mathf.Rad2Deg;
        yAngle *= (-1 * Mathf.Rad2Deg);
        cameraAngle = new Vector3(0, angle + 90, 0);
        slideCameraDown = true;
    }

    public float CalculateDistance(Vector3 v1, Vector3 v2)
    {
        return Mathf.Sqrt((v1.x - v2.x) * (v1.x - v2.x) + (v1.z - v2.z) * (v1.z - v2.z));
    }
}
