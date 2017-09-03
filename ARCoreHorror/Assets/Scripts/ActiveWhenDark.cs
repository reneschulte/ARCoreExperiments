using System.Text;
using GoogleARCore;
using UnityEngine;

public class ActiveWhenDark : MonoBehaviour
{
    public float IntensityThreshold = 0.15f;
    public int MoveCloserSteps = 4;
    public bool IsDark;
    public GameObject[] ObjectsToSwitchOnWhenDark;
    public GameObject ProjectilePrefabDay;
    public GameObject[] ProjectilePrefabsNight;

    private bool _isDarkPrevious;
    private CannonBehavior _cannonBehavior;
    private int _nightIdx;

    void Start()
    {
        if (ObjectsToSwitchOnWhenDark != null)
        {
            for (var i = 0; i < ObjectsToSwitchOnWhenDark.Length; i++)
            {
                ObjectsToSwitchOnWhenDark[i].SetActive(false);
            }
        }
        _cannonBehavior = Camera.main.GetComponent<CannonBehavior>();
    }

    void Update()
    {
#if !UNITY_EDITOR
        IsDark = Frame.LightEstimate.PixelIntensity < IntensityThreshold;
#endif
        if (ObjectsToSwitchOnWhenDark != null)
        {
            for (var i = 0; i < ObjectsToSwitchOnWhenDark.Length; i++)
            {
                ObjectsToSwitchOnWhenDark[i].SetActive(IsDark);
                if (IsDark && _isDarkPrevious != IsDark)
                {
                    var tra = ObjectsToSwitchOnWhenDark[i].transform;
                    var dif = Vector3.ProjectOnPlane(Camera.main.transform.position, Vector3.up) - Vector3.ProjectOnPlane(tra.position, Vector3.up);
                    tra.position = tra.position + dif / MoveCloserSteps;
                }
            }
        }

        if (_cannonBehavior != null && _isDarkPrevious != IsDark)
        {
            if (_nightIdx >= ProjectilePrefabsNight.Length)
            {
                _nightIdx = 0;
            }
            _cannonBehavior.ProjectilePrefab = IsDark ? ProjectilePrefabsNight[_nightIdx] : ProjectilePrefabDay;
            if (IsDark)
            {
                _nightIdx++;
            }
        }

        _isDarkPrevious = IsDark;
    }
}