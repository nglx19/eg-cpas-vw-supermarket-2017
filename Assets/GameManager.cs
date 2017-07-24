using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingItem
{
    public string m_name;
    public Vector3 m_location;
    public bool m_isHigh;
    public string m_mesh;

    public ShoppingItem(string name, Vector3 location, bool isHigh, string mesh)
    {
        m_name = name;
        m_location = location;
        m_isHigh = isHigh;
        m_mesh = mesh;
    }
}


public class GameManager : MonoBehaviour
{


    private ShoppingItem[] allList = new ShoppingItem[] {
        new ShoppingItem( "COOKIES", new Vector3(-19.7f,1.0f,-17.50f), false, "mesh_food_00"),
        new ShoppingItem( "PIZZA", new Vector3(-19.7f,1.0f,-24.80f), false, "mesh_food_01"),
        new ShoppingItem( "CHICKEN", new Vector3(-19.7f,1.0f,-32.20f), false, "mesh_food_02"),
        new ShoppingItem( "CHOCOLATE", new Vector3(-19.7f,1.0f,-41.0f), false, "mesh_food_03"),
        new ShoppingItem( "MILK", new Vector3(-18.30f,1.0f,-53.0f), false, "mesh_food_04"),
        new ShoppingItem( "ORANGE JUICE", new Vector3(-13.0f,1.0f,-45.70f), false, "mesh_food_05"),
        new ShoppingItem( "CEREALS", new Vector3(-9.20f,1.0f,-35.70f), false, "mesh_food_06"),
        new ShoppingItem( "BUTTER", new Vector3(-7.30f,1.0f,-45.60f), false, "mesh_food_07"),
        new ShoppingItem( "FISH", new Vector3(-1.40f,1.0f,-46.30f), false, "mesh_food_08"),
        new ShoppingItem( "SWEETS", new Vector3(4.50f,1.0f,-42.20f), false, "mesh_food_09"),
        new ShoppingItem( "APPLE", new Vector3(4.0f,1.0f,-21.90f), false, "mesh_food_10"),
        new ShoppingItem( "ORANGE", new Vector3(-2.9f,1.0f,-24.40f), false, "mesh_food_11"),
        new ShoppingItem( "BANANA", new Vector3(-8.5f,1.0f,-26.0f), false, "mesh_food_12"),
        new ShoppingItem( "BREAD", new Vector3(-14.2f,1.0f,-22.20f), false, "mesh_food_13"),
        new ShoppingItem( "SOUP", new Vector3(-4.20f,1.0f,-28.50f), false, "mesh_food_14"),
        new ShoppingItem( "VEGETABLE", new Vector3(4.0f,1.0f,-32.0f), false, "mesh_food_15")
    };
    List<int> usedValues = new List<int>();
    private ShoppingItem[] shoppingItems;
    private ShoppingItem currItem;
    private int currIdx;
    private Transform playerTransform;
    private GameObject targetObjects;
    private bool gameCompleted;


    public GameObject playerController;
    public Text textGUI;

    public ShoppingItem[] AllList
    {
        get
        {
            return allList;
        }

        set
        {
            allList = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        gameCompleted = false;
        currIdx = 0;
        shoppingItems = new ShoppingItem[10];
        for (int i = 0; i < 10; i++)
        {
            int x = UniqueRandomInt(0, 10);
            shoppingItems[i] = AllList[x];
            usedValues.Add(x);
            //Debug.Log(shoppingItems[i].m_name);
        }
        targetObjects = transform.Find("TargetObjects").gameObject;

        currItem = shoppingItems[currIdx];
        targetObjects.transform.position = currItem.m_location;
        ShowItem (currItem.m_name, targetObjects);
        textGUI.text = ("Please find " + currItem.m_name);

        //Debug.Log(currItem.m_name);
        //Debug.Log(debugSphere.transform.position.ToString());

        playerTransform = playerController.transform;

        //Debug.Log(playerTransform.transform.position.ToString());

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(playerTransform.transform.position.ToString());
        if ((playerTransform.transform.position - targetObjects.transform.position).sqrMagnitude < 1.5 && !gameCompleted)
        {
            //Debug.Log(currItem.m_name + " has been found!");
            textGUI.text = (currItem.m_name + " has been found!");
            currIdx++;
            if (currIdx >= 10)
            {
                gameCompleted = true;
            }

            currIdx %= 10;
            currItem = shoppingItems[currIdx];
            targetObjects.transform.position = currItem.m_location;
            ShowItem (currItem.m_name, targetObjects);
            textGUI.text = ("Please find " + currItem.m_name);
            //Debug.Log("Please find " + currItem.m_name);
            //Debug.Log(debugSphere.transform.position.ToString());
        }


    }

    public int UniqueRandomInt(int min, int max)
    {
        int val = Random.Range(min, max);
        while (usedValues.Contains(val))
        {
            val = Random.Range(min, max);
        }
        return val;
    }

    void ShowItem(string name, GameObject target)
    {
        foreach (Transform t in target.transform)
        {
            if (t.name == name) {
                t.gameObject.SetActive(true);
            }
            else{
                t.gameObject.SetActive(false);
            }
        }
    }

}
