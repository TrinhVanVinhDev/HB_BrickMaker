using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ListLevels : MapManagement
{
    [SerializeField] private GameObject prefabsMaps;
    private Vector2 firstPoint = new Vector3(-220f, 480f);
    private Vector2 pointRenderj;
    private Vector2 pointRenderi;
    private GameObject mapRended;
    public override void Oninit()
    {
        base.Oninit();
        CreateListMaps();
    }

    private void CreateListMaps()
    {
        FileInfo[] arrFiles = folderMap.GetFiles("*.txt");
        pointRenderj = pointRenderi = firstPoint;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                mapRended = Instantiate(prefabsMaps, new Vector3(0f, 0f, 0f), transform.rotation);
                Debug.Log(mapRended.gameObject.name);
                mapRended.transform.SetParent(transform);
                mapRended.GetComponent<RectTransform>().anchoredPosition = pointRenderj;
                //mapRended.gameObject.GetComponentInChildren<>().text = arrFiles[i+j].Name;
                pointRenderj = pointRenderj + new Rect(220f, 0, 0, 0).position;
            }
            pointRenderi = pointRenderi - new Rect(0, 480f, 0, 0).position;
            pointRenderj = pointRenderi;
        }
    }
}
