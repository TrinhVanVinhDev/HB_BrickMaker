using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject player;
    [SerializeField] private Material materialWay;
    [SerializeField] private GameObject prefabsWay;
    [SerializeField] private GameObject gameObjectMap;

    private Vector2 pointMouseDown;
    private Vector2 pointMouseUp;
    private Vector3 directionMouse;
    private Vector3 movingDirection;
    private Vector3 pointBrickHit;

    //private GameObject waysRender;

    private float sign = 1f;
    private float speedRun = 5f;

    private int oldChild;
    private int listBrickCount;

    private bool moving = false;

    private enum LayerName
    {
        DisableZone = 3,
        Brick = 6,
        LostBrick = 7,
        VictoryZone = 8,
        Way = 9
    }

    private List<GameObject> listBricks = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 rayCastPoint = new Vector3(player.transform.position.x, 0f, player.transform.position.z);
        directionMouse = GetDirectionMouse();
        Physics.Raycast(rayCastPoint, transform.TransformDirection(directionMouse), out hit, 0.5f, layerMask);
        if (hit.collider != null)
        {
            int layerNumber = hit.collider.gameObject.layer;
            listBrickCount = listBricks.Count;

            if (layerNumber == (int)LayerName.DisableZone || layerNumber == (int)LayerName.VictoryZone)
            {
                moving = false;
                directionMouse = Vector3.zero;
            } else if(layerNumber == (int)LayerName.Brick)
            {
                oldChild = transform.childCount;
                pointBrickHit = hit.collider.gameObject.transform.position;
                hit.collider.gameObject.transform.SetParent(gameObject.transform);
                GameObject waysRender = Instantiate(prefabsWay, pointBrickHit, transform.rotation);
                waysRender.transform.SetParent(gameObjectMap.transform);
                if (oldChild != transform.childCount)
                {
                    listBricks.Add(hit.collider.gameObject);
                    CaculatorPositionPlus(listBricks, directionMouse);
                }
            } else if(layerNumber == (int)LayerName.LostBrick)
            {
                if (listBrickCount == 0)
                {
                    Debug.Log("you Lost");
                    player.transform.position = player.transform.position;
                    return;
                }

                listBricks.RemoveAt(listBrickCount - 1);
                if ((gameObject.transform.childCount - 1) > listBricks.Count)
                {
                    CaculatorPositionDiff(listBricks, directionMouse);
                    hit.collider.gameObject.layer = LayerMask.NameToLayer("Way");
                    hit.collider.gameObject.GetComponent<MeshRenderer>().material = materialWay;
                    Destroy(gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject);
                }
            }
        }

        if (directionMouse != Vector3.zero)
        {
            moving = true;
        }

        if (moving && directionMouse != Vector3.zero)
        {
            transform.Translate(directionMouse * speedRun * Time.deltaTime, Space.World);
        }
    }

    private Vector3 GetDirectionMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pointMouseDown = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            pointMouseUp = Input.mousePosition;
            Vector2 targetDir = pointMouseUp - pointMouseDown;
            if(targetDir == Vector2.zero)
            {
                movingDirection = Vector3.zero;
            } else
            {
                sign = (targetDir.y >= 0) ? 1 : -1;
                float angle = Vector2.Angle(Vector2.right, targetDir) * sign;

                if (angle <= 45f && angle >= -45f)
                {
                    movingDirection = Vector3.right;
                    player.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
                }
                else if (angle > 45 && angle <= 135)
                {
                    movingDirection = Vector3.forward;
                    player.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                }
                else if (angle <= 180 && angle > 135 || angle > -180 && angle <= -135)
                {
                    movingDirection = Vector3.left;
                    player.transform.rotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));
                }
                else if (angle > -135f || angle <= -45f)
                {
                    movingDirection = Vector3.back;
                    player.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
                }
            }
        }
        return movingDirection;
    }

    private void CaculatorPositionPlus(List<GameObject> listBricks, Vector3 directionMouse)
    {
        player.transform.position = player.transform.position + new Vector3(0f, 0.2f, 0f);
        if(directionMouse == Vector3.forward)
        {
            listBricks[listBricks.Count - 1].transform.position = listBricks[listBricks.Count - 1].transform.position + new Vector3(0f, 0f, -1f);
        } else if(directionMouse == Vector3.right)
        {
            listBricks[listBricks.Count - 1].transform.position = listBricks[listBricks.Count - 1].transform.position + new Vector3(-1f, 0f, 0f);
        } else if(directionMouse == Vector3.left)
        {
            listBricks[listBricks.Count - 1].transform.position = listBricks[listBricks.Count - 1].transform.position + new Vector3(1f, 0f, 0f);
        } else if(directionMouse == Vector3.back)
        {
            listBricks[listBricks.Count - 1].transform.position = listBricks[listBricks.Count - 1].transform.position + new Vector3(0f, 0f, 1f);
        }

        for (int i = 0; i < listBricks.Count; i++)
        {
            listBricks[i].transform.position = listBricks[i].transform.position + new Vector3(0f, 0.2f, 0f);
        }
    }

    private void CaculatorPositionDiff(List<GameObject> listBricks, Vector3 directionMouse)
    {
        player.transform.position = player.transform.position - new Vector3(0f, 0.2f, 0f);

        for (int i = 0; i < listBricks.Count; i++)
        {
            listBricks[i].transform.position = listBricks[i].transform.position - new Vector3(0f, 0.2f, 0f);
        }
    }
}
