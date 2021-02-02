using UnityEngine;

public enum Phase {
    THROW,
    PLAYING
}

public class GameManager : MonoBehaviour {
    public static Phase CurrentPhase = Phase.THROW;
}