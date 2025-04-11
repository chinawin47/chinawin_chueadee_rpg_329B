using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private int id;
    public int ID
    {  get { return id; } set { id = value;  } }

    [SerializeField]
    private ItemType itemType;
    public ItemType ItemType
    {  get { return itemType; } set {  itemType = value; } }

    [SerializeField]
    private InventoryManager inventoryManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryManager = InventoryManager.instance;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        /* if (transform.childCount > 0)
             return;

         GameObject objDrop = eventData.pointerDrag;
         ItemDrag item = objDrop.GetComponent<ItemDrag>();
         item.IconParent = transform; */

        //Get Item A
        GameObject objA = eventData.pointerDrag;
        ItemDrag itemDragA = objA.GetComponent<ItemDrag>();
        InventorySlot slotA = itemDragA.IconParent.GetComponent<InventorySlot>();

        if(itemType == ItemType.Shield)
        {
            if (itemDragA.Item.Type != itemType)
                return;
        }
        
        //Remove Item A from Slot A
        inventoryManager.RemoveItemInBag(slotA.ID);

        //There is an Item B in Slot A
        if (transform.childCount > 0)
        {
            GameObject objB = transform.GetChild(0).gameObject;
            ItemDrag itemDragB = objB.GetComponent<ItemDrag>();

            if (slotA.ItemType == ItemType.Shield)
            {
                if(itemDragB.Item.Type != slotA.ItemType)
                    return;
            }

            //Remove Item A from Slot A
            inventoryManager.RemoveItemInBag(slotA.ID);

            //Set Item B on Slot A
            itemDragB.transform.SetParent(itemDragA.IconParent);
            itemDragB.IconParent = itemDragA.IconParent;
            inventoryManager.SaveItemInBag(slotA.ID, itemDragB.Item);
            //Remove Item B from Slot B
            inventoryManager.RemoveItemInBag(id);
        }
        else
        {
            inventoryManager.RemoveItemInBag(slotA.ID);
        }
              
        //Set Item A on Slot B
        itemDragA.IconParent = transform;
        inventoryManager.SaveItemInBag(id, itemDragA.Item);
    }

}
