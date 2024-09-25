using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//클래스는 따로 뺐습니다.
namespace Text_RPG_5
{
    internal class Item
    {
        public string ItemName { get; set; }
        public bool IsWeapon { get; set; }
        public int ItemPower { get; set; }
        public string ItemExplanation { get; set; }
        public int ItemPrice { get; set; }
        public bool IsPurchaseItem { get; set; }
        public bool IsItemEqipment { get; set; }

        public Item(string itemName, bool isWeapon, int itemPower, string itemExplanation, int itemPrice, bool isPurchaseItem, bool isItemEqipment)
        {
            ItemName = itemName;
            IsWeapon = isWeapon;
            ItemPower = itemPower;
            ItemExplanation = itemExplanation;
            ItemPrice = itemPrice;
            IsPurchaseItem = isPurchaseItem;
            IsItemEqipment = isItemEqipment;
        }

        //inventory text 생성기
        public void ItemTextInventory()
        {
            if (IsPurchaseItem && IsWeapon)
            {
                Console.WriteLine($"{ItemName}\t| 공격력 +{ItemPower}\t| {ItemExplanation}");
            }
            else if (IsPurchaseItem && !IsWeapon)
            {
                Console.WriteLine($"{ItemName}\t| 방어력 +{ItemPower}\t| {ItemExplanation}");
            }
        }


        //store text 생성기
        public void ItemTextStore()
        {
            if (IsPurchaseItem && IsWeapon)
            {
                Console.WriteLine($"{ItemName}\t| 공격력 +{ItemPower}\t| {ItemExplanation}\t| 구매완료");
            }
            else if (!IsPurchaseItem && IsWeapon)
            {
                Console.WriteLine($"{ItemName}\t| 공격력 +{ItemPower}\t| {ItemExplanation}\t| {ItemPrice} G");
            }
            else if (IsPurchaseItem && !IsWeapon)
            {
                Console.WriteLine($"{ItemName}\t| 방어력 +{ItemPower}\t| {ItemExplanation}\t| 구매완료");
            }
            else
            {
                Console.WriteLine($"{ItemName}\t| 방어력 +{ItemPower}\t| {ItemExplanation}\t| {ItemPrice} G");
            }
        }
    }
}
