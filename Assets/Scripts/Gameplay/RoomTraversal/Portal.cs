using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Portal : MonoBehaviour
{
    private Room currentRoom;
    private Transform playerInsidePortal;

    [Header("UI")]
    public GameObject selectionMenuPrefab; 
    public Vector3 menuOffset = new Vector3(2f, 0f, 0f);
    private Canvas roomSelectionCanvas;

    private GameObject currentMenu;

    public void Initialize(Room room)
    {
        currentRoom = room;
    }
    private void Start()
{
    roomSelectionCanvas = GameObject.Find("RoomSelectionCanvas")?.GetComponent<Canvas>();

    if (roomSelectionCanvas == null)
    {
        Debug.LogError("RoomSelectionCanvas not found in scene.");
    }
}

    private void Update()
    {
        if (playerInsidePortal != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (currentMenu == null)
            {
                ShowSelectionMenu();
            }
        }
    }

    private void ShowSelectionMenu()
    {
        if (currentRoom.connectedRooms.Count == 0) return;

        currentMenu = Instantiate(selectionMenuPrefab, roomSelectionCanvas.transform);

        RectTransform rt = currentMenu.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(0, 0);
        Transform buttonTemplate = currentMenu.transform.Find("RoomButtonTemplate");
        if (buttonTemplate == null)
        {
            Debug.LogError("RoomButtonTemplate not found!");
            return;
        }

        foreach (Room targetRoom in currentRoom.connectedRooms)
        {
            GameObject newButton = Instantiate(buttonTemplate.gameObject, buttonTemplate.parent);
            newButton.SetActive(true);
            string label = targetRoom.isCleared ? $"{targetRoom.roomName} (Completed)" : targetRoom.roomName;
            newButton.GetComponentInChildren<TMP_Text>().text = label;

            newButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                StartCoroutine(TeleportWithFade(targetRoom));
                Destroy(currentMenu);
            });
        }

        buttonTemplate.gameObject.SetActive(false);
    }

    private IEnumerator TeleportWithFade(Room targetRoom)
    {
        yield return StartCoroutine(ScreenFader.Instance.FadeOutToBlack());

        Vector3 targetPos = new Vector3(targetRoom.center.x, targetRoom.center.y, 0f);
        Rigidbody2D rb = playerInsidePortal.GetComponent<Rigidbody2D>();

        if (rb != null)
            rb.position = targetPos;
        else
            playerInsidePortal.position = targetPos;

        var confinerHandler = Camera.main.GetComponent<CameraConfinerHandler>();
        if (confinerHandler != null)
        confinerHandler.SetConfinerBounds(targetRoom.roomBoundsCollider);
        yield return new WaitForSeconds(0.4f);

        yield return StartCoroutine(ScreenFader.Instance.FadeInFromBlack());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MainCharacter"))
        {
            playerInsidePortal = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("MainCharacter") && playerInsidePortal == other.transform)
        {
            playerInsidePortal = null;

            if (currentMenu != null)
            {
                Destroy(currentMenu);
            }
        }
    }
}
