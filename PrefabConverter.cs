#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class PrefabConverter : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] GameObject Prefab;

    [SerializeField, Header("(Change Manually) Listeye eklenen objeleri Prefab ile deðiþtirir")]
    List<GameObject> referances = new List<GameObject>();

    [NaughtyAttributes.Button]
    void ChangeManually()
    {
        if (Prefab)
        {
            foreach (var item in referances)
            {
                GameObject temp = PrefabUtility.InstantiatePrefab(Prefab, item.transform.parent) as GameObject;
                temp.transform.position = item.transform.position;
                temp.transform.SetParent(item.transform.parent);
                DestroyImmediate(item);
                
            }
            referances.Clear();
        }
    }

    [SerializeField, Header("(ChangeByName) Obje ismi ile aranarak eþleþenleri deðiþtirir")]
    string searchString;
    [SerializeField] bool FullMatch;
    [SerializeField] bool IncludeInActiveObjects;
    List<GameObject> referancesByName = new List<GameObject>();
    [NaughtyAttributes.Button]
    void ChangeByName()
    {
        if (searchString.Length > 0)
        {
            referancesByName.Clear();
            foreach (GameObject go in FindObjectsOfType(typeof(GameObject), IncludeInActiveObjects))
            {
                if (FullMatch ? go.name == searchString : go.name.Contains(searchString))
                    referancesByName.Add(go);
            }

            foreach (var item in referancesByName)
            {
                GameObject temp = PrefabUtility.InstantiatePrefab(Prefab, item.transform.parent) as GameObject;
                temp.transform.position = item.transform.position;
                temp.transform.SetParent(item.transform.parent);
                DestroyImmediate(item);
            }
            referancesByName.Clear();
        }
    }
#endif
}