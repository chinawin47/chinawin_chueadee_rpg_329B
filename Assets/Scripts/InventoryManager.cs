using UnityEngine;
using System.Collections.Generic;
public class InventoryManager : MonoBehaviour
{
   public const int MAXSLOT = 17;

    [SerializeField]
    private GameObject[] itemPrefads;
    public GameObject[] ItemPrefabs
    {  get { return itemPrefads; } set { itemPrefads = value; } }

    [SerializeField]
    private ItemData[] itemData;
    public ItemData[] ItemData 
    { get {return itemData; } set {itemData = value; } }

    public static InventoryManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AddItemShopToNPC(1, 0); // health potion
        AddItemShopToNPC(1, 3); // shield B
        AddItemShopToNPC(1, 4); // magic potion
        AddItemShopToNPC(1, 1);
        AddItemShopToNPC(1, 2);
        AddItemShopToNPC(1, 5);
        AddItemShopToNPC(1, 0);
        AddItemShopToNPC(1, 2);
        AddItemShopToNPC(1, 3);
        AddItemShopToNPC(1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AddItem(Character character, int id)
    {
        Item item = new Item(itemData[id]);

       for(int i = 0; i < MAXSLOT; i++)
        {
            if (character.InventoryItems[i] == null)
            {
                character.InventoryItems[i] = item;
                return true;
            }
        }
        Debug.Log("Inventory Full");
        return false;
    }

    public void SaveItemInBag(int index, Item item)
    {
        if (PartyManager.instance.SelectChars.Count == 0)
            return;
        PartyManager.instance.SelectChars[0].InventoryItems[index] = item;

        switch(index)
        {
          
            case 16:
                PartyManager.instance.SelectChars[0].EquipShield(item);
                break;
            case 17:
                PartyManager.instance.SelectChars[0].EquipSword(item);
                break;

        }
    }
    

    public void RemoveItemInBag(int index)
    {
        if (PartyManager.instance.SelectChars.Count == 0)
            return;

        PartyManager.instance.SelectChars[0].InventoryItems[index] = null;
        switch(index)
        {
       
            case 16:
                PartyManager.instance.SelectChars[0].UnEquipShield();
                break;

            case 17:
                PartyManager.instance.SelectChars[0].UnEquipSword();
                break;
        }
    }

    private void SpawnDropItem(Item item, Vector3 pos)
    {
        int id;
        switch(item.Type)
        {
            case ItemType.Consumable:
                id = 1;
                break;
            default:
                id = 0;
                break;
        }

        GameObject itemObj = Instantiate(itemPrefads[id], pos , Quaternion.identity);
        itemObj.AddComponent<ItemPick>();

        ItemPick itemPick = itemObj.GetComponent<ItemPick>();
        itemPick.Init(item, instance, PartyManager.instance);
    }

    public void SpawnDropInventory(Item[] items, Vector3 pos)
    {
        for(int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
                SpawnDropItem(items[i], pos);
        }
    }

    public void DrinkConsumableItem(Item item, int slotId)
    {
        string s = string.Format("Drink: {0}", item.ItemName);
        Debug.Log(s);

        if(PartyManager.instance.SelectChars.Count > 0)
        {
            PartyManager.instance.SelectChars[0].Recover(item.Power);
            RemoveItemInBag(slotId);
        }
    }

    public bool CheckPartyForItem(int id)
    {
        Item item = new Item(itemData[id]);
        Debug.Log(item.ItemName);

        List<Character> party = PartyManager.instance.Members;

        foreach (Character hero in party)
        {
            for (int i = 0; i < hero.InventoryItems.Length; i++)
            {
                Debug.Log(hero.InventoryItems[i].ItemName);
                if (hero.InventoryItems[i].ID == item.ID)
                    return true;
            }
        }
        return false;
    }

    public bool RemoveItemFromParty(int id)
    {
        Item item = new Item(itemData[id]);
        Debug.Log($"Finding {item.ItemName}");

        List<Character> selectedHero = PartyManager.instance.SelectChars;

        foreach (Character hero in selectedHero)
        {
            for (int i = 0; i < hero.InventoryItems.Length; i++)
            {
                if (hero.InventoryItems[i].ID == item.ID)
                {
                    Debug.Log($"Removing {hero.InventoryItems[i].ItemName}");
                    hero.InventoryItems[i] = null;
                    Debug.Log($"Removed {hero.InventoryItems[i]}");
                    return true;
                }
            }
        }
        return false;
    }
    private void AddItemShopToNPC(int npcId, int itemId)
    {
        Item item = new Item(itemData[itemId]);
        QuestManager.instance.NPCPerson[npcId].ShopItems.Add(item);
    }
}
