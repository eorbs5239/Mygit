using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPig : MonoBehaviour
{
    // Start is called before the first frame update
    public bool enableSpawn = false;
    public GameObject Enemy; //Prefab�� ���� public ���� �Դϴ�.
    void SpawnEnemy()
    {
        float randomX = Random.Range(7f, 8f); //���� ��Ÿ�� X��ǥ�� �������� ������ �ݴϴ�.
        
        if (enableSpawn)
        {
            GameObject enemy = (GameObject)Instantiate(Enemy, new Vector3(randomX, -2f, 0f), Quaternion.identity); //������ ��ġ��, ȭ�� ���� ������ Enemy�� �ϳ� �������ݴϴ�.
        }
    }
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 3, 3); //3���� ����, SpawnEnemy�Լ��� 3�ʸ��� �ݺ��ؼ� ���� ��ŵ�ϴ�.
    }
    void Update()
    {

    }
}