using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// Гарантированный спавн настольной арены по ЛКМ в симуляторе и на реальном AR-устройстве.
/// </summary>
public class ARPlaceArenaController : MonoBehaviour
{
    [Header("Префаб Арены")]
    [SerializeField] private GameObject arenaPrefab;

    private ARRaycastManager raycastManager;
    private GameObject spawnedArena;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        // Считываем нажатие ЛКМ или касание экрана
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Vector2 screenPos = Input.touchCount > 0 ? Input.GetTouch(0).position : (Vector2)Input.mousePosition;

            Vector3 spawnPosition = Vector3.zero;
            Quaternion spawnRotation = Quaternion.identity;
            bool hitFound = false;

            // 1. Пробуем AR-Raycast
            if (raycastManager != null && raycastManager.Raycast(screenPos, hits, TrackableType.AllTypes))
            {
                spawnPosition = hits[0].pose.position;
                spawnRotation = hits[0].pose.rotation;
                hitFound = true;
            }
            // 2. Если AR-плоскость не нашлась — используем прямой физический луч по комнате
            else if (Camera.main != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(screenPos);
                if (Physics.Raycast(ray, out RaycastHit physicsHit))
                {
                    spawnPosition = physicsHit.point;
                    hitFound = true;
                }
            }

            // 3. Создаем или перемещаем арену
            if (hitFound && arenaPrefab != null)
            {
                if (spawnedArena == null)
                {
                    spawnedArena = Instantiate(arenaPrefab, spawnPosition, spawnRotation);
                    spawnedArena.SetActive(true);
                    
                    // Задаем миниатюрный настольный размер
                    spawnedArena.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
                    Debug.Log("<color=green>[AR]</color> Арена успешно спроецирована на поверхность!");
                }
                else
                {
                    spawnedArena.transform.position = spawnPosition;
                }
            }
        }
    }
}