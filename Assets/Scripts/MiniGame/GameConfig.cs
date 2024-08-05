using UnityEngine;

[CreateAssetMenu(menuName = "Configs/CardGame")]
public class CardGameConfig : ScriptableObject, ICardGameConfig
{
    [field: SerializeField] public float Time { get; private set; }
    [field: SerializeField] public int Size { get; private set; }

    public string Name => nameof(CardGameConfig);
}