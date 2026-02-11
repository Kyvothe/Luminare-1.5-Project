using UnityEngine;

public class HealItem : MonoBehaviour
{
    [SerializeField] private int _healAmount;

    public int ReturnHealthAmount()
    {
        return _healAmount;
    }
}
