using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

internal enum Multiplier {
    NONE = 1,
    DOUBLE = 2,
    TRIPLE = 3,
    QUADRUPLE = 4
}

public class MultiplierZone : MonoBehaviour {
    
    private Multiplier _currentMultiplier = Multiplier.NONE;
    private SpriteRenderer _sr;
    private static GameObject[] _zones;

    [SerializeField] private Sprite[] spriteArray;

    private void Awake() {
        _zones = GameObject.FindGameObjectsWithTag("MultiplierZone");
        _sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;

        switch (_currentMultiplier) {
            case Multiplier.NONE:
                _currentMultiplier = Multiplier.DOUBLE;
                break;
            case Multiplier.DOUBLE:
                _currentMultiplier = Multiplier.TRIPLE;
                break;
            case Multiplier.TRIPLE:
                _currentMultiplier = Multiplier.QUADRUPLE;
                break;
            case Multiplier.QUADRUPLE:
                _currentMultiplier = Multiplier.NONE;
                break;
        }

        _sr.sprite = spriteArray[((int) _currentMultiplier) - 1];
    }

    public static int GetMultiplier() {
        if (_zones.Length == 0) return 1;

        var cm = _zones[0].GetComponent<MultiplierZone>()._currentMultiplier;

        if (_zones.Select(zone => zone.GetComponent<MultiplierZone>())
            .Any(mz => cm != mz._currentMultiplier)) {
            cm = Multiplier.NONE;
        }

        return (int) cm;
    }
}