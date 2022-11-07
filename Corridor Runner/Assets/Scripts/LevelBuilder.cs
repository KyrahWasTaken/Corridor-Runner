using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelBuilder : MonoBehaviour
{
    public Transform[] prefabBG;
    public Transform prefabParallax1;
    public Transform prefabParallax2;
    public Transform prefabWallHorizontal;
    public Transform prefabWallVertical;
    public Transform prefabPlayer;

    public TextMeshProUGUI uiScore;

    public float corridorWidth;
    public float wallHeight;

    public float wallSpeed;
    public float bg1Speed;
    public float bg2Speed;
    public float bg3Speed;
    public float blockLength;


    void Start()
    {
        Transform player = Instantiate(prefabPlayer);
        player.GetComponent<Player>().uiScore = uiScore;

        BuildLevelBlock(Vector3.left*blockLength);
        BuildLevelBlock(Vector3.right*blockLength);
        BuildLevelBlock(Vector3.zero);

        BuildBG(Vector3.left * blockLength);
        BuildBG(Vector3.right * blockLength);
        BuildBG(Vector3.zero);
        
        BuildParallax(bg1Speed, prefabParallax1, Vector3.left * blockLength);
        BuildParallax(bg1Speed, prefabParallax1, Vector3.right * blockLength);
        BuildParallax(bg1Speed, prefabParallax1, Vector3.zero);

        BuildParallax(bg2Speed, prefabParallax2, Vector3.left * blockLength);
        BuildParallax(bg2Speed, prefabParallax2, Vector3.right * blockLength);
        BuildParallax(bg2Speed, prefabParallax2, Vector3.zero);

        StartCoroutine(GenerateLevel());
        StartCoroutine(GenerateBG());
        StartCoroutine(GenerateParallax(bg1Speed,prefabParallax1));
        StartCoroutine(GenerateParallax(bg2Speed,prefabParallax2));
    }

    private IEnumerator GenerateParallax(float Speed, Transform bgPrefab)
    {
        while (true)
        {
            BuildParallax(Speed, bgPrefab, Vector3.right * 2 * blockLength);
            yield return new WaitForSeconds(blockLength / Speed);
        }
    }


    private IEnumerator GenerateBG()
    {
        while(true)
        {
            BuildBG(Vector3.right*2*blockLength);
            yield return new WaitForSeconds(blockLength / bg3Speed);
        }
    }


    IEnumerator GenerateLevel()
    {
        while (true)
        {
            BuildLevelBlock(Vector3.right*2*blockLength);
            BuildWall(Vector3.right * 2 * blockLength);
            yield return new WaitForSeconds(blockLength / wallSpeed);
        }
    }
    private void BuildParallax(float Speed, Transform bgPrefab, Vector3 position)
    {
        Transform t = Instantiate(bgPrefab, Vector3.zero + position, Quaternion.identity, transform);
        t.GetComponent<LevelPart>().speed = Speed;
        t = Instantiate(bgPrefab, Vector3.zero + position, Quaternion.Euler(0, 0, 180), transform);
        t.GetComponent<LevelPart>().speed = Speed;
    }
    private void BuildBG(Vector3 position)
    {
        int n = Random.Range(0, prefabBG.Length);
        Transform t = Instantiate(prefabBG[n], Vector3.zero + position, Quaternion.identity, transform);
        t.GetComponent<LevelPart>().speed = bg3Speed;
    }
    private void BuildLevelBlock(Vector3 position)
    {
        Transform t = Instantiate(prefabWallHorizontal, new Vector3(0, corridorWidth) + position, Quaternion.identity, transform);
        t.GetComponent<LevelPart>().speed = wallSpeed;
        Transform t2 = Instantiate(prefabWallHorizontal, new Vector3(blockLength/2, -corridorWidth) + position, Quaternion.Euler(0, 0, 180), transform);
        t2.GetComponent<LevelPart>().speed = wallSpeed;
    }
    private void BuildWall(Vector3 position)
    {
        Transform t3 = Instantiate(prefabWallVertical, new Vector3(0, Random.Range(corridorWidth - wallHeight, -corridorWidth + wallHeight)) + position, Quaternion.identity, transform);
        t3.GetComponent<LevelPart>().speed = wallSpeed;
    }
}
