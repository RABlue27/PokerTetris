using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//TODO: Fix collision logic for movement. Currently the child block will not check for collisiosn.
//TODO: Rotation logic to prevent out of bounds exploits


public class GameGrid : MonoBehaviour
{
    public GameObject square;
    public int rows = 10;
    public int cols = 5;
    private int frame = 0;

    private GameObject firstSummonedBlock;
    private GameObject secondSummonedBlock;

    public AudioSource source;
    public AudioClip click;
    public AudioClip rift;

    public int points = 0;


    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        source = audioSources[0];
        click = audioSources[0].clip;
        rift = audioSources[1].clip;
        createGrid();
        summonBlock();

    }

    // Update is called once per frame
    void Update()
    {
        frame++;

    bool blockExists = firstSummonedBlock != null;
    if (blockExists) {
        if (frame % 6000 == 0) {
            moveDown(firstSummonedBlock);
             playOne();
    
        }
        firstSummonedBlock.GetComponent<Renderer>().material.color = new Color(150f / 255f, 50f / 255f, 50f / 255f);
    }

if (blockExists)
{

    if (Input.GetKeyDown(KeyCode.DownArrow))
    {
        moveDown(firstSummonedBlock);   
        playOne();
    }

    if (Input.GetKeyDown(KeyCode.LeftArrow))
    {
        // Move the last summoned block to the left if X is greater than 0 and no collision
        moveLeft();
        playOne();
    }

    if (Input.GetKeyDown(KeyCode.RightArrow))
    {
        // Move the last summoned block to the right if X is less than 4 and no collision
        moveRight();
        playOne();
    }

    if (Input.GetKeyDown(KeyCode.Q))
    {
        // Rotate the last summoned block counterclockwise
        firstSummonedBlock.transform.Rotate(Vector3.up, 90f);
        playOne();
    }

    if (Input.GetKeyDown(KeyCode.E))
    {
        // Rotate the last summoned block clockwise
        firstSummonedBlock.transform.Rotate(Vector3.up, -90f);
        playOne();
    }


    // only for testing, really.
    if (Input.GetKeyDown(KeyCode.UpArrow))
    {
        playOne();
        // Move the last summoned block up if Z is greater than 0 and no collision
        if (firstSummonedBlock.transform.position.z > 0)
        {
            Vector3 targetPosition = firstSummonedBlock.transform.position + Vector3.back;
            if (!CheckCollision(targetPosition))
            {
                firstSummonedBlock.transform.position = targetPosition;
            }
        }
        Debug.Log(points);
        frame = 0;
    }
}

// bound is 4
void moveRight () {
        float first = firstSummonedBlock.transform.position.x;
        float second = secondSummonedBlock.transform.position.x;
        // Move the last summoned block down if Z is greater than 0 and no collision
        if (first < 4 && first > second)
        {
            Vector3 targetPosition = firstSummonedBlock.transform.position + Vector3.right;
            if (!CheckCollision(targetPosition))
            {
                firstSummonedBlock.transform.position = targetPosition;
            }
            return;
        }
        if (second < 4 && first < second) {
            Vector3 targetPosition = secondSummonedBlock.transform.position + Vector3.right;
            if (!CheckCollision(targetPosition))
            {
                targetPosition += Vector3.left;
                firstSummonedBlock.transform.position = targetPosition;
            }
            return;
        }
        if (second < 4 && first == second) {
            Vector3 targetPosition = firstSummonedBlock.transform.position + Vector3.right;
            if (!CheckCollision(targetPosition))
            {
                firstSummonedBlock.transform.position = targetPosition;
            }
        }

        return;
        
}


void moveLeft(){
    int first = Mathf.RoundToInt(firstSummonedBlock.transform.position.x);
    int second = Mathf.RoundToInt(secondSummonedBlock.transform.position.x);

 
        // Move the last summoned block down if Z is greater than 0 and no collision
        if (first > 0 && first <= second)
        {
            Vector3 targetPosition = firstSummonedBlock.transform.position + Vector3.left;
            if (!CheckCollision(targetPosition))
            {
                firstSummonedBlock.transform.position = targetPosition;
            }
            return;
        }
        if (second > 0 && first >=second) {
 

            Vector3 targetPosition = secondSummonedBlock.transform.position + Vector3.left;
            if (!CheckCollision(targetPosition))
            {
                targetPosition += Vector3.right;
                firstSummonedBlock.transform.position = targetPosition;
            }
            return;
        }
        if (second > 0  && first == second) {
  
            Vector3 targetPosition = secondSummonedBlock.transform.position + Vector3.left;
            if (!CheckCollision(targetPosition))
            {
                firstSummonedBlock.transform.position = targetPosition;
            }
        }

        return;
        
}


void moveDown(GameObject firstSummonedBlock) {
int first = (int)firstSummonedBlock.transform.position.z;
int second = (int)secondSummonedBlock.transform.position.z;

// Move the last summoned block down if Z is greater than 0 and no collision

if (second == first && second < 9) {
    Vector3 targetPosition = firstSummonedBlock.transform.position + Vector3.forward;
    Vector3 secondPosition = secondSummonedBlock.transform.position + Vector3.forward;


    if (!CheckCollision(targetPosition) && !CheckCollision(secondPosition))
    {   
        firstSummonedBlock.transform.position = targetPosition;
    }
    else
    {
        summonBlock();
    }
    return;
}

if (second > first && second < 9)
{
    Vector3 targetPosition = secondSummonedBlock.transform.position + Vector3.forward;
    if (!CheckCollision(targetPosition))
    {   
        targetPosition += Vector3.back;
        firstSummonedBlock.transform.position = targetPosition;
    }
    else
    {
        summonBlock();
    }
}
else if (firstSummonedBlock.transform.position.z < 9)
{
    Vector3 targetPosition = firstSummonedBlock.transform.position + Vector3.forward;

    if (!CheckCollision(targetPosition))
    {
        firstSummonedBlock.transform.position = targetPosition;
    }
    else
    {
        summonBlock();
    }
}
else
{
    summonBlock();
}
}


bool CheckCollision(Vector3 targetPosition)
{
    Collider[] colliders = Physics.OverlapSphere(targetPosition, 0.1f);
    return colliders.Length > 0;
}

    for (int z = 0; z < 10; z++) {
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

        if (secondSummonedBlock != null) {
            secondSummonedBlock.transform.parent = transform;
        }


        Vector3 position = new Vector3(2, 1.5f, -2);

        GameObject block = Instantiate(square, position, Quaternion.Euler(0f, 0f, 0f));
        position += Vector3.right;
        GameObject block2 = Instantiate(square, position, Quaternion.Euler(0f, 0f, 0f));
        block.tag = "Player";
        block2.tag = "Player";

        string[] suits = { "♠️", "♥️", "♣️", "♦️" };
        string[] numbers = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10"};

        // Generate random indices for suit and number
        int randomSuitIndex = UnityEngine.Random.Range(0, suits.Length);
        int randomNumberIndex = UnityEngine.Random.Range(0, numbers.Length);
        // Set the suit and number of the block
        block.GetComponent<CubeScript>().suit = suits[randomSuitIndex];
        block.GetComponent<CubeScript>().value = numbers[randomNumberIndex];

        randomSuitIndex = UnityEngine.Random.Range(0, suits.Length);
        randomNumberIndex = UnityEngine.Random.Range(0, numbers.Length);
        block2.GetComponent<CubeScript>().suit = suits[randomSuitIndex];
        block2.GetComponent<CubeScript>().value = numbers[randomNumberIndex];

        // Set parent 
        block.transform.parent = transform;
        block2.transform.parent = block.transform;

        firstSummonedBlock = block;
        secondSummonedBlock = block2;

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

            if (secondSummonedBlock != null) {
            secondSummonedBlock.transform.parent = transform;
    }
    // delete if full line
    playTwo();
    for (int x = 0; x < 5; x++)
    {   

        Vector3 pos = new Vector3(x, 1.5f, z);
        Collider[] colliders = Physics.OverlapSphere(pos, 0.1f);
        foreach (Collider collider in colliders)
        {
            string suit = collider.gameObject.GetComponent<CubeScript>().suit;
            string value = collider.gameObject.GetComponent<CubeScript>().value;

        

            // "♠️", "♥️", "♣️", "♦️"
            //"A", "2", "3", "4", "5", "6", "7", "8", "9", "10"
            int multipler = 1;
            switch (suit) {
                case "♠️":
                    multipler = 8;
                    break;
                case "♥️":
                    multipler = 4;
                    break;
                case "♣️":
                    multipler = 2;
                    break;
                case "♦️":
                    multipler = 1;
                    break;     
                default:
                    multipler = 1;
                    Debug.Log("Broken!");
                    break;              
            } 

        int v = 0;

        switch (value)
        {
            case "A":
                v = 11;
                break;
            case "2":
                v = 2;
                break;
            case "3":
                 v = 3;
                 break;
            case "4":
                v = 4;
                break;
            case "5":
                v = 5;
                break;
            case "6":
                v = 6;
                break;
            case "7":
                v = 7;
                break;
            case "8":
                v = 8;
                break;
            case "9":
                v = 9;
                break;
            case "10":
                v = 10;
                break;
            default:
                throw new ArgumentException("Invalid value");
        }


            points += v * multipler;

            Destroy(collider.gameObject);
        }

    }
        onClear(z);
        Debug.Log(points);
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


void playOne() {
    source.PlayOneShot(click);
}

void playTwo() {
    source.PlayOneShot(rift);
}

}
