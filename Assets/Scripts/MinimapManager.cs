using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinimapManager : MonoBehaviour
{
    //[SerializeField] UIEventSystem uIEventSystem;
    [SerializeField] GameObject minimap1;
    [SerializeField] GameObject minimap2;
    [SerializeField] Transform player;
    [SerializeField] Transform mapRoot1;
    [SerializeField] Transform mapRoot2;
    [Header("Player Icon")]
    [SerializeField] GameObject playerIcon1;
    [SerializeField] GameObject playerIcon2;
    [Header("Save Icon")]
    [SerializeField] GameObject prefabSavePoint;
    [SerializeField] Transform[] SavePointOnGlobalMap1;
    [SerializeField] Transform[] SavePointOnGlobalMap2;
    [Header("Oxy")]
    [SerializeField] Transform Root00Map1;
    [SerializeField] Transform Root01Map1;
    [SerializeField] Transform Root10Map1;
    [SerializeField] Transform Root00Map2;
    [SerializeField] Transform Root01Map2;
    [SerializeField] Transform Root10Map2;
    Vector3 minimapRootPosition = Vector3.zero;
    Vector3 minimapPlayerPosition;
    Vector3 mapRootPosition;
    Vector3 mapPlayerPosition;
    Vector3 realOffset;
    [SerializeField] float scaleDistanceMap1 = 0.1132f;
    [SerializeField] float scaleDistanceMap2 = 0.0859f;
    void Awake()
    {
        //uIEventSystem.setPlayerIconPosition = setPlayerIconPosition;
    }
    void Start()
    {
        int playingMap = PlayerPrefs.GetInt("PlayingMap");
        setPlayerIconPosition(playingMap);
        setSavePointOnMinimap(playingMap);
    }
    void Update()
    {
        int playingMap = PlayerPrefs.GetInt("PlayingMap");
        setPlayerIconPosition(playingMap);
    }
    Vector3 calDistance(Vector3 start, Vector3 end)
    {
        return end - start;
    }
    public void setPlayerIconPosition(int index)
    {
        mapPlayerPosition = player.position;
        if (index == 1)
        {
            mapRootPosition = mapRoot1.position;
            realOffset = calDistance(mapRootPosition, mapPlayerPosition);
            playerIcon1.transform.localPosition = realOffset * scaleDistanceMap1 + minimapRootPosition;
        }
        else
        {
            mapRootPosition = mapRoot2.position;
            realOffset = calDistance(mapRootPosition, mapPlayerPosition);
            playerIcon2.transform.localPosition = realOffset * scaleDistanceMap2 + minimapRootPosition;
        }

        //Debug.Log(realOffset);

    }
    public void setSavePointOnMinimap(int index)
    {
        if (index == 1)
        {
            foreach (Transform point in SavePointOnGlobalMap1)
            {
                mapRootPosition = mapRoot1.position;
                Vector3 offsetSavePointOnMinimap = calDistance(mapRootPosition, point.position) * scaleDistanceMap1;
                GameObject iconSave;
                Vector3 localPostionSavePoint = offsetSavePointOnMinimap + minimapRootPosition;
                iconSave = Instantiate(prefabSavePoint, offsetSavePointOnMinimap + minimap1.transform.parent.transform.position, Quaternion.identity);
                iconSave.transform.parent = minimap1.transform.parent.transform;
            }
        }
        else
        {
            foreach (Transform point in SavePointOnGlobalMap2)
            {
                mapRootPosition = mapRoot2.position;
                Vector3 offsetSavePointOnMinimap = calDistance(mapRootPosition, point.position) * scaleDistanceMap2;
                GameObject iconSave;
                Vector3 localPostionSavePoint = offsetSavePointOnMinimap + minimapRootPosition;
                iconSave = Instantiate(prefabSavePoint, offsetSavePointOnMinimap + minimap2.transform.parent.transform.position, Quaternion.identity);
                iconSave.transform.parent = minimap2.transform.parent.transform;
            }
        }
    }
    public float[] getLocationIconPlayerMinimap(int index)
    {
        // a(a1,a2); b(b1,b2); c(c1;c2); x(x1;x2)
        // ax = (x1 - a1; x2 - a2);
        // bc = (c1 - b1; c2 - b2) = u;
        float offset_X, offset_Y;
        float[] PosIconPlayer = { 0, 0 };
        if (index == 1)
        {

            float u_a = Root10Map1.position.x - Root00Map1.position.x;
            offset_X = Mathf.Abs(u_a);
            float u_b = Root10Map1.position.y - Root00Map1.position.y;
            float x_0 = playerIcon1.transform.position.x;
            float y_0 = playerIcon1.transform.position.y;
            float a = -u_b;
            float b = u_a;
            float c = (-u_b) * (-Root00Map1.position.x) + u_a * (-Root00Map1.position.y);
            PosIconPlayer[1] = (Mathf.Abs(a * x_0 + b * y_0 + c) / Mathf.Sqrt(a * a + b * b));
            u_a = Root01Map1.position.x - Root00Map1.position.x;
            u_b = Root01Map1.position.y - Root00Map1.position.y;
            offset_Y = Mathf.Abs(u_b);
            a = -u_b;
            b = u_a;
            c = (-u_b) * (-Root00Map1.position.x) + u_a * (-Root00Map1.position.y);
            PosIconPlayer[0] = Mathf.Abs(a * x_0 + b * y_0 + c) / Mathf.Sqrt(a * a + b * b);

            PosIconPlayer[0] = PosIconPlayer[0] / offset_X;
            PosIconPlayer[1] = PosIconPlayer[1] / offset_Y;

            //Debug.Log(PosIconPlayer[0] + ";" + PosIconPlayer[1]);

            return PosIconPlayer;
        }
        else
        {
            float u_a = Root10Map2.position.x - Root00Map2.position.x;
            offset_X = Mathf.Abs(u_a);
            float u_b = Root10Map2.position.y - Root00Map2.position.y;
            float x_0 = playerIcon2.transform.position.x;
            float y_0 = playerIcon2.transform.position.y;
            float a = -u_b;
            float b = u_a;
            float c = (-u_b) * (-Root00Map2.position.x) + u_a * (-Root00Map2.position.y);
            PosIconPlayer[1] = (Mathf.Abs(a * x_0 + b * y_0 + c) / Mathf.Sqrt(a * a + b * b));
            u_a = Root01Map2.position.x - Root00Map2.position.x;
            u_b = Root01Map2.position.y - Root00Map2.position.y;
            offset_Y = Mathf.Abs(u_b);
            a = -u_b;
            b = u_a;
            c = (-u_b) * (-Root00Map2.position.x) + u_a * (-Root00Map2.position.y);
            PosIconPlayer[0] = Mathf.Abs(a * x_0 + b * y_0 + c) / Mathf.Sqrt(a * a + b * b);

            PosIconPlayer[0] = PosIconPlayer[0] / offset_X;
            PosIconPlayer[1] = PosIconPlayer[1] / offset_Y;

            //Debug.Log(PosIconPlayer[0] + ";" + PosIconPlayer[1]);

            return PosIconPlayer;
        }
    }
}
