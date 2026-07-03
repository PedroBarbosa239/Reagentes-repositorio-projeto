using UnityEngine;
using System.Collections.Generic;
public class SarahPower : MonoBehaviour
{
    private CharacterManager characterManager;

    private bool isSelecting = false;
    private bool isPossessing = false;

    private PossessableNPC[] npcs;
    private PossessableNPC possessedNPC;

    private int currentNPC = 0;

    private CameraFollow cameraFollow;

    [Header("Possessão")]
    [SerializeField] private float holdTime = 1f;

    private float holdTimer = 0f;

    private MovimentPlayer sarahMovement;

    void Start()
    {
        characterManager = CharacterManager.Instance;

        cameraFollow = Camera.main.GetComponent<CameraFollow>();

        sarahMovement = GetComponent<MovimentPlayer>();

        npcs = FindObjectsByType<PossessableNPC>(FindObjectsSortMode.None);
    }

    void Update()
    {
        // Enquanto estiver possuindo um NPC
        if (isPossessing)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                holdTimer += Time.deltaTime;

                if (holdTimer >= holdTime)
                {
                    holdTimer = 0f;
                    ReturnToSarah();
                }
            }

            if (Input.GetKeyUp(KeyCode.Q))
            {
                holdTimer = 0f;
            }

            return;
        }

        

        // ===========================
        // MODO DE SELEÇÃO
        // ===========================
        if (isSelecting)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ExitSelectionMode();
                return;
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                PreviousNPC();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                NextNPC();
            }

            // Segurar Q para possuir
            if (Input.GetKey(KeyCode.Q))
            {
                holdTimer += Time.deltaTime;

                if (holdTimer >= holdTime)
                {
                    holdTimer = 0f;
                    PossessNPC();
                }
            }

            if (Input.GetKeyUp(KeyCode.Q))
            {
                holdTimer = 0f;
            }

            return;
        }

        // Só funciona se a Sarah estiver sendo controlada
        if (!sarahMovement.IsControlled)
            return;
        // Entrar no modo de seleção
        if (Input.GetKeyDown(KeyCode.Q))
        {
            EnterSelectionMode();
        }
    }

   void EnterSelectionMode()
{
    PossessableNPC[] allNPCs =
        FindObjectsByType<PossessableNPC>(
            FindObjectsSortMode.None
        );

    List<PossessableNPC> activeNPCs =
        new List<PossessableNPC>();

    foreach (PossessableNPC npc in allNPCs)
    {
        if (npc.gameObject.activeInHierarchy)
        {
            activeNPCs.Add(npc);
        }
    }

    npcs = activeNPCs.ToArray();

    if (npcs.Length == 0)
    {
        Debug.Log("Nenhum NPC disponível.");
        return;
    }

    isSelecting = true;

    characterManager.CanSwitchCharacters = false;

    currentNPC = 0;

    sarahMovement.SetControlled(false);

    cameraFollow.target = npcs[currentNPC].transform;

    Debug.Log(
        "Selecionando: " +
        npcs[currentNPC].npcName
    );
}

    void NextNPC()
    {
        if (npcs.Length == 0)
            return;

        currentNPC++;

        if (currentNPC >= npcs.Length)
            currentNPC = 0;

        cameraFollow.target = npcs[currentNPC].transform;

        Debug.Log("Selecionando: " + npcs[currentNPC].npcName);
    }

    void PreviousNPC()
    {
        if (npcs.Length == 0)
            return;

        currentNPC--;

        if (currentNPC < 0)
            currentNPC = npcs.Length - 1;

        cameraFollow.target = npcs[currentNPC].transform;

        Debug.Log("Selecionando: " + npcs[currentNPC].npcName);
    }

    void PossessNPC()
    {
        possessedNPC = npcs[currentNPC];

        isPossessing = true;
        isSelecting = false;

        holdTimer = 0f;

        // Sarah para de ser controlada
        sarahMovement.SetControlled(false);

        // NPC passa a ser controlado
        possessedNPC.GetComponent<MovimentPlayer>().SetControlled(true);

        // Continua seguindo o NPC
        cameraFollow.target = possessedNPC.transform;

        Debug.Log("NPC Possuído: " + possessedNPC.npcName);
    }

    void ReturnToSarah()
    {
        if (possessedNPC == null)
            return;

        // NPC deixa de ser controlado
        possessedNPC.GetComponent<MovimentPlayer>().SetControlled(false);

        // Sarah volta a ser controlada
        sarahMovement.SetControlled(true);

        // Câmera volta
        cameraFollow.target = transform;

        possessedNPC = null;

        isPossessing = false;

        holdTimer = 0f;

        characterManager.CanSwitchCharacters = true;

        Debug.Log("Sarah recuperou o controle.");
    }

    void ExitSelectionMode()
    {
        isSelecting = false;

        holdTimer = 0f;

        characterManager.CanSwitchCharacters = true;
        sarahMovement.SetControlled(true);
        cameraFollow.target = characterManager.CurrentPlayer.transform;

        Debug.Log("Modo de seleção encerrado.");
    }

    public bool IsSelecting => isSelecting;
}