using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RenderMap : MapManagement
{
    public enum PrefabsType
    {
        disableZone = 0,
        brick = 1,
        lostBrick = 2,
        Player = 3,
        ways = 4,
        victoryZone = 5
    }

    [SerializeField] private List<GameObject> listPrefabs = new List<GameObject>();

    public override void Oninit()
    {
        base.Oninit();
        ReadFileMaps();
    }

    private void ReadFileMaps()
    {
        string[] readFile = File.ReadAllLines(pathFile);
        for (int z = 0; z < readFile.Length; z++)
        {
            char[] chars = readFile[z].ToCharArray();
            for (int x = 0; x < chars.Length; x++)
            {
                int indexChar = int.Parse(chars[x].ToString());
                if (indexChar == 3)
                {
                    listPrefabs[indexChar].transform.position = new Vector3(x, 0.2f, z);
                }
                else
                {
                    GenerateMaps(listPrefabs[indexChar], new Vector3(x, 0, z)).transform.SetParent(gameObject.transform, gameObject);
                }
            }
        }
    }

    private GameObject GenerateMaps(GameObject prefabs, Vector3 positionMaps)
    {
        return Instantiate(prefabs, positionMaps, transform.rotation);
    }

}
