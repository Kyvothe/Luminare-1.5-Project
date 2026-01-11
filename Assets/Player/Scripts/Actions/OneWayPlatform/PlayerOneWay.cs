using UnityEngine;

public class PlayerOneWay : MonoBehaviour
{
  private PlatformEffector2D _oneWayPlatformEffector;

  public void SetPlatformEffector(PlatformEffector2D effector2D)
  {
    _oneWayPlatformEffector = effector2D;
  }

  public void CheckForOneWayPlatform()
  {
    if (!_oneWayPlatformEffector) return;

    _oneWayPlatformEffector.surfaceArc = 0;
    _oneWayPlatformEffector.GetComponent<OneWayPlatformBehaviour>().StartSetBackTimer();
    _oneWayPlatformEffector = null;
  }
}
