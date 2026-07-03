using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
[SerializeField] private float pressDistance = 0.1f;

private Vector3 originalPosition;
    [SerializeField] private Renderer targetRenderer;

[SerializeField] private Color activatedColor = Color.green;

private Color originalColor;
    private bool playerInside = false;
    private bool activated = false;

    public bool IsActivated => activated;

    [SerializeField]
    private int buttonID;

    private void Start()
{
    if (targetRenderer == null)
        targetRenderer = GetComponent<Renderer>();
    originalPosition = transform.localPosition;
    originalColor = targetRenderer.material.color;
}

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("NPC"))
            return;

        playerInside = true;

        SaveMessageUI.Instance.ShowMessage("Pressione [I] para ativar");
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("NPC"))
            return;

        playerInside = false;
    }

    private void Update()
    {
        if (!playerInside)
            return;

        if (activated)
            return;

        if (Input.GetKeyDown(KeyCode.I))
        {
            Activate();

            ButtonPuzzleManager.Instance.ButtonPressed();

            SaveMessageUI.Instance.ShowMessage("Botão ativado!");
        }
    }

 public void Activate()
{
    if (activated)
        return;

    activated = true;

    if (targetRenderer != null)
        targetRenderer.material.color = activatedColor;
    
     transform.localPosition =
        originalPosition + Vector3.down * pressDistance;
}
}