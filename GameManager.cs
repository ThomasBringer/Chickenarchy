using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public static bool speaking = false;

    public AnimationCurve curve;

    public enum ItemType { Horn, Carrots, Wine, Gold, Sword, None }
    public Item[] items;

    void Awake()
    {
        gm = this;
    }

    public Vector2 itemSpawn = Vector2.up;

    static ItemType currentItem = ItemType.None;
    public static ItemType CurrentItem
    {
        get { return currentItem; }
        set
        {
            if (value == ItemType.None)
            {
                if (itemGo != null) Destroy(itemGo);
                return;
            }

            currentItem = value;
            itemGo = SpawnItem(Player.chickens[0].transform, value);
        }
    }
    static GameObject itemGo;

    public static GameObject SpawnItem(Transform trans, ItemType item)
    {
        return Instantiate(FindGo(item), (Vector2)trans.position + gm.itemSpawn, Quaternion.identity, trans);
    }

    static GameObject FindGo(ItemType type)
    {
        foreach (var item in gm.items)
            if (type == item.type)
                return item.go;

        Debug.LogError("No GameObject of item type " + type + " was find.");
        return null;
    }
}

public static class ExtensionMethods
{
    public static IEnumerator TranslateLerp(this Transform trans, Vector3 target, float time, AnimationCurve curve)
    {
        var origin = trans.position;

        for (float i = 0; i < time; i += Time.deltaTime)
        {
            float progress = i / time;
            trans.position = Vector3.Lerp(origin, target, progress * curve.Evaluate(progress));

            yield return null;
        }

        trans.position = target;
    }

    public static Vector2 Round(this Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x * 32) / 32, Mathf.Round(pos.y * 32) / 32);
    }
}

[System.Serializable]
public struct Item
{
    public GameObject go;
    public GameManager.ItemType type;
}