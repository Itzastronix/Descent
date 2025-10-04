using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuCameraAnimation : MonoBehaviour
{
    [SerializeField] private float cameraAnimSpeed = 0.3f;
    [SerializeField] private Vector3 targetPosition = new Vector3(1.09f, 4.09f, 11.12f);


    void Start()
    {
        transform.position = new Vector3(1.09f, 4.09f, 35f);

    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * cameraAnimSpeed);
    }
}
