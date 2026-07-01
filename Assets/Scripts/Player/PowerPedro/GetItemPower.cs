using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactDistance = 3f;
    public InventoryUI inventoryUI;
    public float viewAngle = 60f;

    public Camera cam;

    public Inventory inventory;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            interactDistance
        );

        foreach (Collider hit in hits)
        {
            WorldItem item = hit.GetComponent<WorldItem>();

            if (item != null)
            {
                Vector3 directionToItem =
                    (item.transform.position - transform.position).normalized;

                directionToItem.y = 0;

                Vector3 cameraForward = cam.transform.forward;

                cameraForward.y = 0;

                float angle =
                    Vector3.Angle(cameraForward, directionToItem);

                if (angle <= viewAngle)
                {
                    inventory.AddItem(item.item);

                    GameManager.Instance.RegisterCollectedItem(
                        item.worldItemID
                    );

                    item.gameObject.SetActive(false);
                    inventoryUI.UpdateUI();
    
                    break;
                }
            }
        }
    }
}