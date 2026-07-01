using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragItemUI : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    private Canvas canvas;
    private InventorySlotUI slot;

    private GameObject dragIcon;
    private RectTransform dragRect;

    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        slot = GetComponent<InventorySlotUI>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (slot.currentItem == null)
            return;

        dragIcon = new GameObject("DragIcon");
        dragIcon.transform.SetParent(canvas.transform, false);

        Image img = dragIcon.AddComponent<Image>();
        img.sprite = slot.currentItem.icon;
        img.raycastTarget = false;

        dragRect = dragIcon.GetComponent<RectTransform>();
        dragRect.sizeDelta = GetComponent<RectTransform>().sizeDelta;
        dragRect.position = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragIcon != null)
            dragRect.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragIcon != null)
            Destroy(dragIcon);

        if (slot.currentItem == null)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            PuzzleReceiver receiver =
                hit.collider.GetComponent<PuzzleReceiver>();

            if (receiver != null)
            {
                bool success = receiver.UseItem(slot.currentItem);

                if (success)
                {
                    slot.ConsumeItem();
                }
            }
        }
    }
}