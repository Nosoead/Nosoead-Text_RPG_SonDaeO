using System.Reflection.Emit;
//Text_RPG_5가 제출내용입니다!//Text_RPG_5가 제출내용입니다!//Text_RPG_5가 제출내용입니다!//Text_RPG_5가 제출내용입니다!//Text_RPG_5가 제출내용입니다!
namespace Text_RPG_4
{
    public enum EMenu
    {
        상태보기 = 1,
        인벤토리,
        상점
    }
    public class Item
    {
        public string ItemName { get; set; }
        public string ItemExplanation { get; set; }
        public int ItemPrice { get; set; }
        public bool IsPurchaseItem { get; set; }
    }

    public class Weapon : Item
    {
        public int ItemATK { get; set; }

        public Weapon(string itemName, int itemATK, string itemExplanation, int itemPrice, bool isPurchaseItem)
        {
            ItemName = itemName;
            ItemATK = itemATK;
            ItemExplanation = itemExplanation;
            ItemPrice = itemPrice;
            IsPurchaseItem = isPurchaseItem;
        }
        public void ItemTextInventory()
        {
            if (IsPurchaseItem)
            {
                Console.WriteLine($"{ItemName}\t| 공격력 +{ItemATK}\t| {ItemExplanation}");
            }
        }
        public void ItemTextShop()
        {
            if (IsPurchaseItem)
            {
                Console.WriteLine($"{ItemName}\t| 공격력 +{ItemATK}\t| {ItemExplanation}\t| 구매완료");
            }
            else
            {
                Console.WriteLine($"{ItemName}\t| 공격력 +{ItemATK}\t| {ItemExplanation}\t| {ItemPrice} G");
            }
        }
    }
    public class Armor : Item
    {
        public int ItemDEF { get; set; }
        public Armor(string itemName, int itemDEF, string itemExplanation, int itemPrice, bool isPurchaseItem)
        {
            ItemName = itemName;
            ItemDEF = itemDEF;
            ItemExplanation = itemExplanation;
            ItemPrice = itemPrice;
            IsPurchaseItem = isPurchaseItem;
        }
        public void ItemTextInventory()
        {
            if (IsPurchaseItem)
            {
                Console.WriteLine($"{ItemName}\t| 방어력 +{ItemDEF}\t| {ItemExplanation}");
            }
        }
        public void ItemTextShop()
        {
            if (IsPurchaseItem)
            {
                Console.WriteLine($"{ItemName}\t| 방어력 +{ItemDEF}\t| {ItemExplanation}\t| 구매완료");
            }
            else
            {
                Console.WriteLine($"{ItemName}\t| 방어력 +{ItemDEF}\t| {ItemExplanation}\t| {ItemPrice} G");
            }
        }
    }
    //아이템 구매
    public class Program
    {
        static int menuNum = 0;
        static string Name = "DaeO";
        static string job = "전사";
        static int Level = 1;
        static int ATK = 10;
        static int DEF = 5;
        static int itemATK = 0;
        static int itemDEF = 0;
        static int HP = 100;
        static int Gold = 1500;
        static bool IsNumberTag = false;
        static List<Weapon> weaponItems = new List<Weapon>();
        static List<Armor> armorItems = new List<Armor>();
        static void Main(string[] args)
        {
            MakeItem();
            //메뉴 및 진입
            while (true)
            {
                switch (menuNum)
                {
                    case 0:
                        menuNum = EnterMenu();
                        break;
                    case 1:
                        menuNum = EnterStatus();
                        break;
                    case 2:
                        menuNum = EnterInventory();
                        break;
                    case 3:
                        menuNum = EnterStore(ref Gold);
                        break;
                }

            }

        }
        static public int EnterMenu()
        {
            Console.Clear();
            do
            {

                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
                for (int i = 1; i <= Enum.GetValues(typeof(EMenu)).Length; i++)
                {
                    Console.WriteLine(i + ". " + (EMenu)i);
                }
                Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                //ConsoleKeyInfo villageChoice = Console.ReadKey();
                int menuChoice = (int)Char.GetNumericValue(Console.ReadKey().KeyChar);
                if (menuChoice > 0 && menuChoice <= Enum.GetValues(typeof(EMenu)).Length)
                {
                    return menuChoice;
                }
                Console.Clear();
            }
            while (true);
        }
        public static int EnterStatus()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("상태보기");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
                Console.WriteLine($"Chad ({job})");
                Console.WriteLine("Lv. " + Level);
                Console.WriteLine($"공격력 : {ATK}+{itemATK} (+{itemATK}");
                Console.WriteLine($"방어력 : {DEF}+{itemDEF} (+{itemDEF})");
                Console.WriteLine("체  력 : + HP");
                Console.WriteLine("Gold : " + Gold);
                Console.WriteLine("\n0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                int menuChoice = (int)Char.GetNumericValue(Console.ReadKey().KeyChar);
                switch (menuChoice)
                {
                    case 0:
                        return menuChoice;
                }
            }
            while (true);
        }
        static public int EnterInventory()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유중인 아이템을 관리할 수 있습니다.\n");

                Console.WriteLine("[아이템 목록]");
                if (!IsNumberTag)
                {
                    for (int i = 0; i < armorItems.Count; i++)
                    {
                        if (armorItems[i].IsPurchaseItem) { Console.Write("- "); }
                        armorItems[i].ItemTextInventory();
                    }
                    for (int i = 0; i < weaponItems.Count; i++)
                    {
                        if (weaponItems[i].IsPurchaseItem) { Console.Write("- "); }
                        weaponItems[i].ItemTextInventory();
                    }
                }
                else
                {
                    for (int i = 0; i < armorItems.Count; i++)
                    {
                        Console.Write("- ");
                        armorItems[i].ItemTextInventory();
                    }
                    for (int i = 0; i < weaponItems.Count; i++)
                    {
                        Console.Write("- ");
                        weaponItems[i].ItemTextInventory();
                    }
                }
                Console.WriteLine("\n1. 장착 관리");
                Console.WriteLine("0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                int menuChoice = (int)Char.GetNumericValue(Console.ReadKey().KeyChar);
                if (menuChoice == 0)//리턴 참트루 &&으로 붙히고 다른 if 문 돌리게 유도
                {
                    IsNumberTag = false;
                    return menuChoice;
                }
                else if (menuChoice == 1 && IsNumberTag == false)
                {
                    IsNumberTag = true;
                }
            }
            while (true);
        }
        static public int EnterStore(ref int Gold)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{Gold} G\n");
                Console.WriteLine("[아이템 목록]");
                if (!IsNumberTag)
                {
                    for (int i = 0; i < armorItems.Count; i++)
                    {
                        Console.Write("- ");
                        armorItems[i].ItemTextShop();
                    }
                    for (int i = 0; i < weaponItems.Count; i++)
                    {
                        Console.Write("- ");
                        weaponItems[i].ItemTextShop();
                    }
                }
                else
                {
                    for (int i = 0; i < armorItems.Count; i++)
                    {
                        Console.Write($"- {i+1} ");
                        armorItems[i].ItemTextShop();
                    }
                    for (int i = 0; i < weaponItems.Count; i++)
                    {
                        Console.Write($"- {i+armorItems.Count+1} ");
                        weaponItems[i].ItemTextShop();
                    }
                }
                Console.WriteLine("\n1. 아이템 구매");
                Console.WriteLine("\n0.나가기");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                int menuChoice = (int)Char.GetNumericValue(Console.ReadKey().KeyChar);
                if (menuChoice == 0)//리턴 참트루 &&으로 붙히고 다른 if 문 돌리게 유도
                {
                    IsNumberTag = false;
                    return menuChoice;
                }
                else if (menuChoice == 1 && IsNumberTag == false)
                {
                    IsNumberTag = true;
                    menuChoice = -1;
                }
                if (menuChoice >= 1 && menuChoice < (armorItems.Count + weaponItems.Count + 1) && IsNumberTag == true)
                {
                    if ((menuChoice - 1) / armorItems.Count == 0 && Gold >= armorItems[menuChoice - 1].ItemPrice)
                    { armorItems[menuChoice - 1].IsPurchaseItem = true; Gold -= armorItems[menuChoice - 1].ItemPrice; }
                    else if ((menuChoice - 1) / armorItems.Count == 1 && Gold >= weaponItems[menuChoice - 1 - armorItems.Count].ItemPrice)
                    {
                        weaponItems[menuChoice - 1 - armorItems.Count].IsPurchaseItem = true;
                        Gold -= weaponItems[menuChoice - 1 - armorItems.Count].ItemPrice;
                    }

                }
            }
            while (true);
        }

        static public void MakeItem()
        {
            armorItems.Add(new Armor("수련자 갑옷", 5, "수련에 도움을 주는 갑옷입니다.", 1000, false));
            armorItems.Add(new Armor("무쇠 갑옷", 2, "무쇠로 만들어져 튼튼한 갑옷입니다.", 1750, false));
            armorItems.Add(new Armor("스파르타 갑옷", 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, false));
            weaponItems.Add(new Weapon("낡은 검", 2, "쉽게 볼 수 있는 낡은 검 입니다.", 600, false));
            weaponItems.Add(new Weapon("청동 도끼", 5, "어디선가 사용됐던거 같은 도끼입니다.", 600, false));
            weaponItems.Add(new Weapon("스파르타 창", 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2400, false));
        }
    }
}
