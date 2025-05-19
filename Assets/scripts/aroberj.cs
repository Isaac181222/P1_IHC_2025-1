using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class ARObjectPlacer : MonoBehaviour
{
    public GameObject objectToPlace;
    public TMP_Text phraseText;

    private ARRaycastManager raycastManager;
    private GameObject spawnedObject;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (Touchscreen.current == null || spawnedObject != null)
            return;

        if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

            if (raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;

                spawnedObject = Instantiate(objectToPlace, hitPose.position, hitPose.rotation);
                spawnedObject.transform.Rotate(0, 180f, 0);

                var interaction = spawnedObject.GetComponent<ARCatInteraction>();
                if (interaction != null)
                {
                    interaction.phraseText = phraseText;
                }
            }
        }
    }
}
