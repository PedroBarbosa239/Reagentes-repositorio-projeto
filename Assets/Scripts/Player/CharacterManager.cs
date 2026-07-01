using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;
    public bool CanSwitchCharacters = true;

    [SerializeField]
    private MovimentPlayer[] players;
    [SerializeField]
    private CameraFollow cameraFollow;
    private int currentPlayer = 0;

    public MovimentPlayer CurrentPlayer => players[currentPlayer];

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SetPlayer(0);
    }

    void Update()
    {
        if (!CanSwitchCharacters)
        return;
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            NextPlayer();
        }
    }

    void NextPlayer()
    {
        currentPlayer++;

        if (currentPlayer >= players.Length)
            currentPlayer = 0;

        SetPlayer(currentPlayer);
    }

    void SetPlayer(int index)
    {
        currentPlayer = index;

        for (int i = 0; i < players.Length; i++)
        {
            players[i].SetControlled(i == currentPlayer);
        }
    cameraFollow.target = players[currentPlayer].transform;
        Debug.Log("Controlando: " + players[currentPlayer].name);
    }

    public void TeleportAllPlayers(Transform[] spawnPoints)
{
    for (int i = 0; i < players.Length && i < spawnPoints.Length; i++)
    {
        players[i].transform.position = spawnPoints[i].position;
    }
}
}