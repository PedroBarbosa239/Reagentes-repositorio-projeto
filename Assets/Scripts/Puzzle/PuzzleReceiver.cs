using UnityEngine;

public class PuzzleReceiver : MonoBehaviour
{
    [Header("Puzzle")]
    public string requiredItemID = "wood";
    public GameObject bridge;

    [Header("Área Visual")]
    public Material areaMaterial;

    private Material runtimeMaterial;
    private GameObject borderParent;

    void Start()
    {
        CreateBorder();
    }

    void Update()
    {
        if (runtimeMaterial == null)
            return;

        float alpha = Mathf.Lerp(
            0.25f,
            0.9f,
            (Mathf.Sin(Time.time * 3f) + 1f) * 0.5f);

        Color c = runtimeMaterial.color;
        c.a = alpha;
        runtimeMaterial.color = c;
    }

    void CreateBorder()
    {
        BoxCollider box = GetComponent<BoxCollider>();

        if (box == null)
        {
            Debug.LogWarning("PuzzleReceiver precisa de um BoxCollider.");
            return;
        }

        borderParent = new GameObject("Puzzle Border");
        borderParent.transform.SetParent(transform);
        borderParent.transform.localPosition = Vector3.zero;
        borderParent.transform.localRotation = Quaternion.identity;

        runtimeMaterial = new Material(areaMaterial);

        float t = 0.03f;      // espessura da borda
        float h = 0.04f;   // altura acima do chão

        CreatePiece(
            "Top",
            new Vector3(box.center.x, h, box.center.z + box.size.z / 2),
            new Vector3(box.size.x, t, t));

        CreatePiece(
            "Bottom",
            new Vector3(box.center.x, h, box.center.z - box.size.z / 2),
            new Vector3(box.size.x, t, t));

        CreatePiece(
            "Left",
            new Vector3(box.center.x - box.size.x / 2, h, box.center.z),
            new Vector3(t, t, box.size.z));

        CreatePiece(
            "Right",
            new Vector3(box.center.x + box.size.x / 2, h, box.center.z),
            new Vector3(t, t, box.size.z));
    }

    void CreatePiece(string name, Vector3 pos, Vector3 scale)
    {
        GameObject piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

        piece.name = name;

        piece.transform.SetParent(borderParent.transform);
        piece.transform.localPosition = pos;
        piece.transform.localScale = scale;

        Destroy(piece.GetComponent<BoxCollider>());

        Renderer r = piece.GetComponent<Renderer>();
        r.material = runtimeMaterial;
    }

    public bool UseItem(Item item)
    {
        if (item.itemID == requiredItemID)
        {
            bridge.SetActive(true);

            if (borderParent != null)
                borderParent.SetActive(false);

            Debug.Log("Ponte criada!");

            return true;
        }

        Debug.Log("Item errado!");
        return false;
    }
}