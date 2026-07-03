using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class LetterJitter : MonoBehaviour
{
    [Header("Configuração")]
    [SerializeField]
    private float intensity = 0.5f;

    [SerializeField]
    private float updateInterval = 0.05f;

    private TMP_Text textMesh;

    private float timer;

    public void SetIntensity(float value)
    {
        intensity = value;
    }

    private void Awake()
    {
        textMesh = GetComponent<TMP_Text>();
    }

    private void LateUpdate()
    {
        timer += Time.deltaTime;

        if (timer < updateInterval)
            return;

        timer = 0f;

        textMesh.ForceMeshUpdate();

        TMP_TextInfo textInfo = textMesh.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            TMP_CharacterInfo charInfo =
                textInfo.characterInfo[i];

            if (!charInfo.isVisible)
                continue;

            int materialIndex =
                charInfo.materialReferenceIndex;

            int vertexIndex =
                charInfo.vertexIndex;

            Vector3[] vertices =
                textInfo.meshInfo[materialIndex].vertices;

            Vector2 offset =
                Random.insideUnitCircle * intensity;

            vertices[vertexIndex + 0] += (Vector3)offset;
            vertices[vertexIndex + 1] += (Vector3)offset;
            vertices[vertexIndex + 2] += (Vector3)offset;
            vertices[vertexIndex + 3] += (Vector3)offset;
        }

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            Mesh mesh = textInfo.meshInfo[i].mesh;

            mesh.vertices =
                textInfo.meshInfo[i].vertices;

            textMesh.UpdateGeometry(mesh, i);
        }
    }
}