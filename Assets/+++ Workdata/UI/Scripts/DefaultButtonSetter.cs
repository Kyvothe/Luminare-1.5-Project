
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DefaultButtonSetter : MonoBehaviour
{
    public Button defaultButton;

    private void OnEnable()
    {
        Selectable newSelection;
        newSelection = defaultButton;
        StartCoroutine(DelaySelection(newSelection));
    }

    IEnumerator DelaySelection(Selectable newSelection)
    {
        yield return null;
        newSelection.Select();
    }
}
