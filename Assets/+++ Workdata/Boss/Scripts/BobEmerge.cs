using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BobEmerge : MonoBehaviour
{
    // Fail Safe wenn Cat nicht richtig stirbt wegen Bug / Hidden Feature

    private Vector2 _moveTo;
    
    public UnityEvent OnBobRise;

    private void Awake()
    {
        StartCoroutine(BobRise());
        
        _moveTo = new Vector2(11.5f, -4.64f);
    }

    private IEnumerator BobRise()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("BobRise");
        gameObject.transform.position = _moveTo;
        
        OnBobRise.Invoke();
    }
}
