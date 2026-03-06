using UnityEngine;

public class SkipCutscene : MonoBehaviour
{
    public GameObject ratGang;

    public void SkippingCutscene()
    {
        ratGang.GetComponent<RatGangInteract>().SkipCutscene();
    }
}
