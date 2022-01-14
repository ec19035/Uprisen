using static scr_Models;
using UnityEngine;


public class scr_CameraController : MonoBehaviour
{
    [Header("References")]
    public scr_PlayerController playerController;
    [HideInInspector]
    public Vector3 targetRotation;
    public GameObject yGimbal;
    private Vector3 yGimbalRotation;

    [Header("Settings")]
    public CameraSettingsModel settings;
    //private Vector3 movementVelocity;
    //public float movementSmoothTime = 0.1f;

    #region - Update -
    private void Update()
    {
        CameraRotation();
        FollowPlayerCameraTarget();
    }

    #endregion

    #region - Position/Rotation -
    private void CameraRotation()
    {
        var viewInput = playerController.input_View;

        targetRotation.y += (settings.InvertedX ? -(viewInput.x * settings.SensitivityX) : (viewInput.x * settings.SensitivityX)) * Time.deltaTime;

        transform.rotation = Quaternion.Euler(targetRotation);

        yGimbalRotation.x += (settings.InvertedY ? (viewInput.y * settings.SensitivityY) : -(viewInput.y * settings.SensitivityY)) * Time.deltaTime;
        yGimbalRotation.x = Mathf.Clamp(yGimbalRotation.x, settings.YClampMin, settings.YClampMax);

        yGimbal.transform.localRotation = Quaternion.Euler(yGimbalRotation);

        // if (playerController.isTargetMode)
        // {
        //     var currentRotation = playerController.transform.rotation;

        //     var newRotation = currentRotation.eulerAngles;
        //     newRotation.y = targetRotation.y;

        //     currentRotation = Quaternion.Lerp(currentRotation, Quaternion.Euler(newRotation), settings.CharacterRotationSmoothdamp);
        //     playerController.transform.rotation = currentRotation;
        // }
    }

    private void FollowPlayerCameraTarget()
    {
        //transform.position = Vector3.SmoothDamp(transform.position, playerController.cameraTarget.position, ref movementVelocity, movementSmoothTime);
        transform.position = playerController.cameraTarget.position;
    }

    #endregion
}