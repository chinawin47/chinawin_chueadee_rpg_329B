using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private const int MAXSLOT = 16;

    [SerializeField]
    private GameObject[] itemPrefads;
    public GameObject[] ItemPrefabs
    {  get { return itemPrefads; } set { itemPrefads = value; } }

    [SerializeField]
    private ItemData[] itemData;
    public ItemData[] ItemData 
    { get {return itemData; } set {itemData = value; } }

    public static InventoryManager Instance;

    private void Awake()
    {
        Instance = this;
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

        if (character.InventoryItems.Count < MAXSLOT)
        {
            character.InventoryItems.Add(item);
            return true;
        }
        else
        {
            Debug.Log("Inventory Full");
            return false;
        }
    }
}
