using UnityEngine;

public class InventoryManager : MonoBehaviour
{
   public const int MAXSLOT = 16;

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
    }

    public void RemoveItemInBag(int index)
    {
        if (PartyManager.instance.SelectChars.Count == 0)
            return;

        PartyManager.instance.SelectChars[0].InventoryItems[index] = null;
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

}
