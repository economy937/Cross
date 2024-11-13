using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour
{
    public GameObject[] mapPrefabs;

    int _random;
    float position;
    float scale;

    public void Map()
    {
        _random = Random.Range(0, mapPrefabs.Length);
        for (int i = 0; i < mapPrefabs.Length; i++)
        {
            CreatLevel(mapPrefabs[i], i);
        }
    }

    public void CreatLevel(GameObject obj, int value)
    {
        if (_random == value)
        {
            GameObject go = Instantiate(obj) as GameObject;

            float offset = position + (scale * 0.5f);
            offset += (go.transform.localScale.z) * 0.5f;

            Vector3 pos = new Vector3(0, 0, offset);

            go.transform.position = pos;

            position = go.transform.position.z;
            scale = go.transform.localScale.z;

            go.transform.parent = this.transform;
        }
    }


}
