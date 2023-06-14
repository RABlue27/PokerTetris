using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameGrid : MonoBehaviour
{
    public GameObject square;
    public int rows = 10;
    public int cols = 5;
    private int frame = 0;

    private GameObject lastSummonedBlock;

    void Start()
    {
        createGrid();
        summonBlock();

    }

    // Update is called once per frame
    void Update()
    {

        frame++;

    // if (Input.GetKeyDown(KeyCode.Space)) {
    //     summonBlock();
    
    // }
    bool blockExists = lastSummonedBlock != null;
    if (blockExists) {
        if (frame % 600 == 0) {
            moveDown(lastSummonedBlock);
    
        }
        lastSummonedBlock.GetComponent<Renderer>().material.color = new Color(150f / 255f, 50f / 255f, 50f / 255f);
    }

if (blockExists)
{
    if (Input.GetKeyDown(KeyCode.DownArrow))
    {
        moveDown(lastSummonedBlock);
    }

    if (Input.GetKeyDown(KeyCode.LeftArrow))
    {
        // Move the last summoned block to the left if X is greater than 0 and no collision
        if (lastSummonedBlock.transform.position.x > 0)
        {
            Vector3 targetPosition = lastSummonedBlock.transform.position + Vector3.left;
            if (!CheckCollision(targetPosition))
            {
                lastSummonedBlock.transform.position = targetPosition;
            }
        }
    }

    if (Input.GetKeyDown(KeyCode.RightArrow))
    {
        // Move the last summoned block to the right if X is less than 4 and no collision
        if (lastSummonedBlock.transform.position.x < 4)
        {
            Vector3 targetPosition = lastSummonedBlock.transform.position + Vector3.right;
            if (!CheckCollision(targetPosition))
            {
                lastSummonedBlock.transform.position = targetPosition;
            }
        }
    }

    if (Input.GetKeyDown(KeyCode.Q))
    {
        // Rotate the last summoned block counterclockwise
        lastSummonedBlock.transform.Rotate(Vector3.up, 90f);
    }

    if (Input.GetKeyDown(KeyCode.E))
    {
        // Rotate the last summoned block clockwise
        lastSummonedBlock.transform.Rotate(Vector3.up, -90f);
    }

    if (Input.GetKeyDown(KeyCode.UpArrow))
    {
        // Move the last summoned block up if Z is greater than 0 and no collision
        if (lastSummonedBlock.transform.position.z > 0)
        {
            Vector3 targetPosition = lastSummonedBlock.transform.position + Vector3.back;
            if (!CheckCollision(targetPosition))
            {
                lastSummonedBlock.transform.position = targetPosition;
            }
        }
    }
}

void moveDown(GameObject lastSummonedBlock) {
        // Move the last summoned block down if Z is greater than 0 and no collision
        if (lastSummonedBlock.transform.position.z < 9)
        {
            Vector3 targetPosition = lastSummonedBlock.transform.position + Vector3.forward;
            if (!CheckCollision(targetPosition))
            {
                lastSummonedBlock.transform.position = targetPosition;
            }
            else {
                summonBlock();
            }
        }
        else {
            summonBlock();
        }
}

bool CheckCollision(Vector3 targetPosition)
{
    Collider[] colliders = Physics.OverlapSphere(targetPosition, 0.1f);
    return colliders.Length > 0;
}

    for (int z = 0; z < 10; z++) {
        // Debug.Log(isCompleteLine(z));
        isCompleteLine(z);
    }

    }

    void createGrid() {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {

                Vector3 position = new Vector3(col, 1f, row);
                GameObject gridObject = Instantiate(square, position, Quaternion.identity);
 
                gridObject.transform.parent = transform;


            }
        }
    }


    void summonBlock() {


        Vector3 position = new Vector3(2, 1.5f, -2);

        GameObject block = Instantiate(square, position, Quaternion.Euler(0f, 0f, 0f));
        block.tag = "Player";
    


        string[] suits = { "♠️", "♥️", "♣️", "♦️" };
        string[] numbers = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10"};

        // Generate random indices for suit and number
        int randomSuitIndex = UnityEngine.Random.Range(0, suits.Length);
        int randomNumberIndex = UnityEngine.Random.Range(0, numbers.Length);

        // Set the suit and number of the block
        block.GetComponent<CubeScript>().suit = suits[randomSuitIndex];
        block.GetComponent<CubeScript>().value = numbers[randomNumberIndex];


        // Set parent 
        block.transform.parent = transform;


        lastSummonedBlock = block;

    }



    private bool isCompleteLine(int z) {
        int sum = 0;
        for (int x = 0; x < 5; x++) {
            Vector3 pos = new Vector3(x, 1.5f, z);
            Collider[] collider = Physics.OverlapSphere(pos, 0.1f);

            if (collider.Length > 0) {
                continue;
            }
            else {
                return false; 
            }
        }
    // delete if full line
    for (int x = 0; x < 5; x++)
    {   
        Vector3 pos = new Vector3(x, 1.5f, z);
        Collider[] colliders = Physics.OverlapSphere(pos, 0.1f);
        foreach (Collider collider in colliders)
        {
            string suit = collider.gameObject.GetComponent<CubeScript>().suit;
            string value = collider.gameObject.GetComponent<CubeScript>().value;

            Debug.Log("Destroyed Object - Suit: " + suit + ", Value: " + value);
            Destroy(collider.gameObject);
        }

    }
        onClear(z);
        return true;

    }   

void onClear(int row)
{
    GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
    foreach (GameObject obj in gameObjects)
    {
        if (obj.transform.position.z <= row && obj.transform.position.y == 1.5f)
        {
            obj.transform.position += Vector3.forward;
        }
    }
    summonBlock();
}
}
