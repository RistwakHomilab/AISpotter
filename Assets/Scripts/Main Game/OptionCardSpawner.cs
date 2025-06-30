using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class OptionCardSpawner : MonoBehaviour
{
    [Header("Prefabs and Layout")]
    public GameObject optionCardPrefab;
    public Transform optionContainer;

    [Header("JSON & Sprites")]
    public string jsonFileName = "JSON/optionData"; // inside Resources
    public Sprite[] aiSprites;
    public Sprite[] notAiSprites;

    private List<OptionData> optionDataList = new List<OptionData>();
    private Dictionary<string, Sprite> spriteLookup = new Dictionary<string, Sprite>();

    void Start()
    {
        BuildSpriteLookup();
        LoadOptionsFromJSON();
        OptionDatabase.allOptions = optionDataList; // ✅ This line adds all loaded options to the DB

        if (!GameStateManager.Instance.resumeMode)
            SpawnRandomCards();
    }

    void BuildSpriteLookup()
    {
        spriteLookup.Clear();

        foreach (Sprite sprite in aiSprites)
        {
            if (sprite != null && !spriteLookup.ContainsKey(sprite.name))
                spriteLookup[sprite.name] = sprite;
        }

        foreach (Sprite sprite in notAiSprites)
        {
            if (sprite != null && !spriteLookup.ContainsKey(sprite.name))
                spriteLookup[sprite.name] = sprite;
        }
    }

    void LoadOptionsFromJSON()
    {
        TextAsset jsonText = Resources.Load<TextAsset>(jsonFileName);
        if (jsonText != null)
        {
            OptionData[] data = JsonHelper.FromJson<OptionData>(jsonText.text);
            optionDataList.AddRange(data);
        }
        else
        {
            Debug.LogError("JSON file not found: " + jsonFileName);
        }
    }

    public void SpawnRandomCards()
    {
        if (optionDataList == null || optionDataList.Count < 4)
        {
            Debug.LogError("❌ Not enough options to spawn 4 cards. Found: " + optionDataList.Count);
            return;
        }

        List<OptionData> selected = new List<OptionData>();

        while (selected.Count < 4)
        {
            int index = Random.Range(0, optionDataList.Count);
            OptionData pick = optionDataList[index];

            if (!selected.Exists(x => x.id == pick.id))
            {
                selected.Add(pick);
            }
        }

        foreach (OptionData option in selected)
        {
            GameObject card = Instantiate(optionCardPrefab, optionContainer);
            OptionCard cardScript = card.GetComponent<OptionCard>();

            if (cardScript == null)
            {
                Debug.LogError("❌ OptionCard component not found on spawned card!");
                continue;
            }

            Debug.Log($"Spawning card: {option.id}");

            Sprite image = spriteLookup.ContainsKey(option.id) ? spriteLookup[option.id] : null;

            if (image == null)
                Debug.LogWarning($"⚠️ No sprite found for ID: {option.id}");

            cardScript.SetupCard(option.text, image, option.id, option.type);
        }

    }

    public void SpawnOptions(System.Action<OptionCard> onClickCallback)
    {
        if (optionDataList == null || optionDataList.Count < 4)
        {
            Debug.LogError("❌ Not enough options to spawn 4 cards. Found: " + optionDataList.Count);
            return;
        }

        List<OptionData> selected = new List<OptionData>();

        while (selected.Count < 4)
        {
            int index = Random.Range(0, optionDataList.Count);
            OptionData pick = optionDataList[index];

            if (!selected.Exists(x => x.id == pick.id))
            {
                selected.Add(pick);
            }
        }

        foreach (OptionData option in selected)
        {
            GameObject card = Instantiate(optionCardPrefab, optionContainer);
            OptionCard cardScript = card.GetComponent<OptionCard>();

            Sprite image = spriteLookup.ContainsKey(option.id) ? spriteLookup[option.id] : null;

            if (image == null)
                Debug.LogWarning($"No sprite found for ID: {option.id}");

            cardScript.SetupCard(option.text, image, option.id, option.type);
            cardScript.onCardClicked = onClickCallback;
        }
    }

    public void SpawnOptionsWithPreset(List<string> ids, List<bool> selections, System.Action<OptionCard> callback)
    {
        for (int i = 0; i < ids.Count; i++)
        {
            OptionData data = OptionDatabase.GetById(ids[i]);
            if (data == null)
            {
                Debug.LogWarning($"❌ No option data found for ID: {ids[i]}");
                continue;
            }

            GameObject cardObj = Instantiate(optionCardPrefab, optionContainer);
            OptionCard card = cardObj.GetComponent<OptionCard>();

            Sprite image = spriteLookup.ContainsKey(data.id) ? spriteLookup[data.id] : null;
            if (image == null)
            {
                Debug.LogWarning($"⚠️ Sprite not found for ID: {data.id}");
            }

            card.SetupCard(data.text, image, data.id, data.type);  // ✅ You pass the correct sprite here
            card.SetSelected(selections[i]);                        // ✅ Restore selection
            card.onCardClicked = callback;
        }
    }
}

[System.Serializable]
public class OptionData
{
    public string id;
    public string text;
    public Sprite image;
    public string type;
}

[System.Serializable]
public class OptionDataList
{
    public List<OptionData> options;
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        string wrapped = "{\"options\":" + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(wrapped);
        return wrapper.options;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] options;
    }
}

public static class OptionDatabase
{
    public static List<OptionData> allOptions = new List<OptionData>();

    public static OptionData GetById(string id)
    {
        return allOptions.Find(o => o.id == id);
    }
}
