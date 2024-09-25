using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//Text_RPG_5가 제출내용입니다!//Text_RPG_5가 제출내용입니다!//Text_RPG_5가 제출내용입니다!//Text_RPG_5가 제출내용입니다!//Text_RPG_5가 제출내용입니다!
namespace savetest
{
    public enum EMenu
    {
        상태보기 = 1,
        인벤토리,
        상점,
        던전입장,
        휴식
    }
    public class Dungeon
    {
        public string DungeonName { get; set; }
        public float RecommendedDEF { get; set; }
        public float ATKValueReward { get; set; }
        public int GoldReward { get; set; }
        public int EXPReward { get; set; }

        public Dungeon(string dungeonName, float recommendedDEF, float aTKValueReward, int goldReward, int eXPReward)
        {
            DungeonName = dungeonName;
            RecommendedDEF = recommendedDEF;
            ATKValueReward = aTKValueReward;
            GoldReward = goldReward;
            EXPReward = eXPReward;
        }
    }
    public class Item
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
        public void ItemTextShop()
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
    public class Player
    {
        public string Name { get; set; }
        public string Job { get; set; }
        public int Level { get; set; }
        public float ATK { get; set; }
        public float DEF { get; set; }
        public float ItemATK { get; set; }
        public float ItemDEF { get; set; }
        public float HP {  get; set; }
        public int Gold { get; set; }
        public int EXP { get; set; }
        public bool IsSale { get; set; }
        public bool IsClearDungeon { get; set; }

        public Player(string name, string job, int level, float aTK, float dEF, float itemATK, float itemDEF, float hP, int gold, int eXP, bool isSale, bool isClearDungeon)
        {
            Name = name;
            Job = job;
            Level = level;
            ATK = aTK;
            DEF = dEF;
            HP = hP;
            Gold = gold;
            EXP = eXP;
            IsSale = isSale;
            IsClearDungeon = isClearDungeon;
        }
    }

    //아이템 구매
    public class Program
    {
        static int menuNum = 0;
        //static string name = "DaeO";
        //static string job = "전사";
        //static int level = 1;
        //static float ATK = 10f;
        //static float DEF = 5f;
        //static float itemATK = 0f;
        //static float itemDEF = 0f;
        //static float HP = 100f;
        //static int gold = 1500;
        //static int EXP = 0;
        //static bool isSale = false;
        //static bool isClearDungeon = false;
        static bool isNumberTag = false;
        static Player player;
        static List<Item> storeItems = new List<Item>();
        static List<Item> inventoryItems = new List<Item>();
        static List<Dungeon> dungeons = new List<Dungeon>();
        static Item[] weaponEquipment = new Item[1];
        static Item[] armorEquipment = new Item[1];
        static Random random = new Random();
        static void Main(string[] args)
        {
            MakeItem();
            MakeDunGeon();
            MakePlayer();
            //LoadData();
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
                        menuNum = EnterStore();
                        break;
                    case 4:
                        menuNum = EnterDungeon();
                        break;
                    case 5:
                        menuNum = EnterRestArea();
                        break;
                }

                //SaveData();

            }

        }


        static public int EnterMenu()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
                for (int i = 1; i <= Enum.GetValues(typeof(EMenu)).Length; i++)
                {
                    Console.WriteLine(i + ". " + (EMenu)i);
                }
                Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                int numChoice = (int)Char.GetNumericValue(Console.ReadKey().KeyChar);
                if (numChoice > 0 && numChoice <= Enum.GetValues(typeof(EMenu)).Length)
                {
                    return numChoice;
                }
                else
                {
                    Console.WriteLine("\n잘못된입력입니다.\n엔터키를 입력하세요");
                    Console.ReadLine();
                }
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
                Console.WriteLine($"Chad ({player.Job})");
                Console.WriteLine($"Lv. {player.Level}");
                Console.WriteLine($"공격력 : {player.ATK + player.ItemATK} (+{player.ItemATK})");
                Console.WriteLine($"방어력 : {player.DEF + player.ItemDEF} (+{player.ItemDEF})");
                Console.WriteLine($"체  력 : {player.HP}");
                Console.WriteLine($"Gold : {player.Gold}");
                Console.WriteLine("\n0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                int numChoice = (int)Char.GetNumericValue(Console.ReadKey().KeyChar);
                switch (numChoice)
                {
                    case 0:
                        return numChoice;
                    default:
                        Console.WriteLine("\n잘못된입력입니다.\n엔터키를 입력하세요");
                        Console.ReadLine();
                        break;
                }
            }
            while (true);
        }


        static public int EnterInventory()
        {
            do
            {
                Console.Clear();
                if (player == null) return 0;
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유중인 아이템을 관리할 수 있습니다.\n");

                Console.WriteLine("[아이템 목록]");
                if (!isNumberTag)
                {
                    for (int i = 0; i < inventoryItems.Count; i++)
                    {
                        if (inventoryItems[i].IsItemEqipment) { Console.Write("- [E]"); }
                        else { Console.Write("- "); }
                        inventoryItems[i].ItemTextInventory();
                    }
                }
                else
                {
                    for (int i = 0; i < inventoryItems.Count; i++)
                    {
                        if (inventoryItems[i].IsItemEqipment) { Console.Write($"- {i + 1} [E]"); }
                        else { Console.Write($"- {i + 1} "); }
                        inventoryItems[i].ItemTextInventory();
                    }
                }
                if (!isNumberTag) Console.WriteLine("\n1. 장착 관리");
                else Console.WriteLine("\n");
                Console.WriteLine("0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                int numChoice = (int)Char.GetNumericValue(Console.ReadKey().KeyChar);

                if (numChoice == 0)
                {
                    if (isNumberTag)
                    {
                        isNumberTag = false; return numChoice + 2;
                    }
                    else { return numChoice; }
                }
                else if (numChoice == 1 && !isNumberTag)
                {
                    isNumberTag = true;
                    numChoice = -1;
                }
                else if ((numChoice > 1 || numChoice == -1) && !isNumberTag)
                {
                    Console.WriteLine("\n잘못된입력입니다.\n엔터키를 입력하세요");
                    Console.ReadLine();
                }
                else if ((numChoice > inventoryItems.Count || numChoice == -1) && isNumberTag)
                {
                    Console.WriteLine("\n잘못된입력입니다.\n엔터키를 입력하세요");
                    Console.ReadLine();
                }
                if (numChoice >= 1 && numChoice <= (inventoryItems.Count) && isNumberTag == true)
                {
                    if (inventoryItems[numChoice - 1].IsWeapon && weaponEquipment[0] == null)
                    {
                        inventoryItems[numChoice - 1].IsItemEqipment = true;
                        weaponEquipment[0] = inventoryItems[numChoice - 1];
                    }
                    else if (inventoryItems[numChoice - 1].IsWeapon && weaponEquipment[0] != null)
                    {
                        if (inventoryItems[numChoice - 1].ItemName == weaponEquipment[0].ItemName)
                        {
                            weaponEquipment[0].IsItemEqipment = false;
                            weaponEquipment[0] = null;
                        }
                        else
                        {
                            weaponEquipment[0].IsItemEqipment = false;
                            inventoryItems[numChoice - 1].IsItemEqipment = true;
                            weaponEquipment[0] = inventoryItems[numChoice - 1];
                        }
                    }
                    else if (!inventoryItems[numChoice - 1].IsWeapon && armorEquipment[0] == null)
                    {
                        inventoryItems[numChoice - 1].IsItemEqipment = true;
                        armorEquipment[0] = inventoryItems[numChoice - 1];
                    }
                    else if (!inventoryItems[numChoice - 1].IsWeapon && armorEquipment[0] != null)
                    {
                        if (inventoryItems[numChoice - 1].ItemName == armorEquipment[0].ItemName)
                        {
                            armorEquipment[0].IsItemEqipment = false;
                            armorEquipment[0] = null;
                        }
                        else
                        {
                            armorEquipment[0].IsItemEqipment = false;
                            inventoryItems[numChoice - 1].IsItemEqipment = true;
                            armorEquipment[0] = inventoryItems[numChoice - 1];
                        }
                    }
                }

                if (weaponEquipment[0] == null) { player.ItemATK = 0; }
                else { player.ItemATK = weaponEquipment[0].ItemPower; }
                if (armorEquipment[0] == null) { player.ItemDEF = 0; }
                else { player.ItemDEF = armorEquipment[0].ItemPower; }

            }
            while (true);
        }


        static public int EnterStore()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{player.Gold} G\n");
                Console.WriteLine("[아이템 목록]");
                if (!isNumberTag)
                {
                    for (int i = 0; i < storeItems.Count; i++)
                    {
                        Console.Write("- ");
                        storeItems[i].ItemTextShop();
                    }
                }
                else if (isNumberTag && !player.IsSale)
                {
                    for (int i = 0; i < storeItems.Count; i++)
                    {
                        Console.Write($"- {i + 1} ");
                        storeItems[i].ItemTextShop();
                    }
                }
                else if (isNumberTag && player.IsSale)
                {
                    for (int i = 0; i < inventoryItems.Count; i++)
                    {
                        if (inventoryItems[i].IsItemEqipment) { Console.Write($"- {i + 1} [E]"); }
                        else { Console.Write($"- {i + 1} "); }
                        inventoryItems[i].ItemTextInventory();
                    }
                }
                if (!isNumberTag) Console.WriteLine("\n1. 아이템 구매\n2. 아이템 판매");
                else Console.WriteLine("\n\n");
                Console.WriteLine("\n0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                int numChoice = (int)Char.GetNumericValue(Console.ReadKey().KeyChar);
                if (numChoice == 0)
                {
                    if (isNumberTag)
                    {
                        isNumberTag = false; return numChoice + 3;
                    }
                    else { return numChoice; }
                }
                else if ((numChoice == 1 || numChoice == 2) && !isNumberTag)
                {
                    isNumberTag = true;
                    switch (numChoice)
                    {
                        case 1:
                            player.IsSale = false; numChoice = -1; break;
                        case 2:
                            player.IsSale = true; numChoice = -1; break;
                    }
                }
                else if ((numChoice > 2 || numChoice == -1) && !isNumberTag)
                {
                    Console.WriteLine("\n잘못된입력입니다.\n엔터키를 입력하세요");
                    Console.ReadLine();
                }
                else if ((numChoice > storeItems.Count || numChoice == -1) && isNumberTag)
                {
                    Console.WriteLine("\n잘못된입력입니다.\n엔터키를 입력하세요");
                    Console.ReadLine();
                }
                if (numChoice >= 1 && numChoice <= (storeItems.Count) && isNumberTag && !player.IsSale)
                {
                    if (player.Gold >= storeItems[numChoice - 1].ItemPrice && !storeItems[numChoice - 1].IsPurchaseItem)
                    {
                        storeItems[numChoice - 1].IsPurchaseItem = true;
                        player.Gold -= storeItems[numChoice - 1].ItemPrice;
                        inventoryItems.Add(storeItems[numChoice - 1]);
                    }
                    else if (storeItems[numChoice - 1].IsPurchaseItem)
                    {
                        Console.WriteLine("\n이미 구입했습니다.\n엔터키를 입력하세요");
                        Console.ReadLine();
                    }
                    else if (player.Gold < storeItems[numChoice - 1].ItemPrice && !storeItems[numChoice - 1].IsPurchaseItem)
                    {
                        Console.WriteLine("\n돈이 모자랍니다.\n엔터키를 입력하세요");
                        Console.ReadLine();
                    }
                }

                if (numChoice >= 1 && numChoice <= (inventoryItems.Count) && isNumberTag && player.IsSale)
                {
                    if ((inventoryItems[numChoice - 1].IsPurchaseItem) &&
                        (inventoryItems[numChoice - 1].ItemName != armorEquipment[0].ItemName) &&
                        (inventoryItems[numChoice - 1].ItemName != weaponEquipment[0].ItemName))
                    {
                        for (int i = 0; i < storeItems.Count; i++)
                        {
                            if (inventoryItems[numChoice - 1].ItemName == storeItems[i].ItemName)
                            { storeItems[i].IsPurchaseItem = false; }
                        }
                        player.Gold += inventoryItems[numChoice - 1].ItemPrice * 85 / 100;
                        inventoryItems.Remove(inventoryItems[numChoice - 1]);
                    }
                    else
                    {
                        Console.WriteLine("\n장착중인 물건을 팔 수 없습니다.\n엔터키를 입력하세요");
                        Console.ReadLine();
                    }
                }
            }
            while (true);
        }


        static public int EnterDungeon()
        {
            int randClearNum = 0;
            int randHPNum = 0;
            float randReward = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
                for (int i = 0; i < dungeons.Count; i++)
                { Console.WriteLine($"{i + 1}. {dungeons[i].DungeonName}\t| 방어력 {dungeons[i].RecommendedDEF}이상 권장"); }
                Console.WriteLine("\n0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                int numChoice = (int)Char.GetNumericValue(Console.ReadKey().KeyChar);
                if (numChoice > 0 && numChoice <= dungeons.Count && player.HP > 0)
                {
                    randClearNum = random.Next(1, 11);
                    randReward = random.Next((int)(player.ATK + player.ItemATK), (int)(player.ATK + player.ItemATK) * 2);
                    randHPNum = random.Next((int)(20 - (player.DEF + player.ItemDEF) + dungeons[numChoice - 1].RecommendedDEF),
                                            (int)(36 - (player.DEF + player.ItemDEF) + dungeons[numChoice - 1].RecommendedDEF));
                    do
                    {
                        Console.Clear();
                        if ((player.DEF + player.ItemDEF) >= dungeons[numChoice - 1].RecommendedDEF ||
                            ((player.DEF + player.ItemDEF) < dungeons[numChoice - 1].RecommendedDEF && randClearNum > 4))
                        {
                            Console.WriteLine("던 전 클 리 어");
                            Console.WriteLine($"축하합니다!!\n{dungeons[numChoice - 1].DungeonName}을 클리어 하였습니다.\n");
                            Console.WriteLine("[탐험 결과]");
                            Console.WriteLine($"체력 {player.HP} -> {player.HP - randHPNum}");
                            Console.WriteLine($"Gold {player.Gold} G -> {player.Gold + dungeons[numChoice - 1].GoldReward + (player.ATK + player.ItemATK)} G");
                            player.IsClearDungeon = true;
                        }
                        else
                        {
                            Console.WriteLine($"던 전 클 리 어 실 패");
                            Console.WriteLine($"안타깝네요\n {dungeons[numChoice - 1].DungeonName}을 클리어 실패 했습니다.\n");
                            Console.WriteLine("[탐험 결과]");
                            Console.WriteLine($"체력 {player.HP} -> {player.HP / 2}");
                            player.IsClearDungeon = false;
                        }

                        Console.WriteLine("\n0. 나가기");
                        Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                        int numChoice2 = (int)Char.GetNumericValue(Console.ReadKey().KeyChar);
                        switch (numChoice2)
                        {
                            case 0:
                                if (player.IsClearDungeon)
                                {
                                    player.HP -= randHPNum;
                                    player.Gold = player.Gold + dungeons[numChoice - 1].GoldReward + dungeons[numChoice - 1].GoldReward * (int)randReward / 100;
                                    player.EXP += dungeons[numChoice - 1].EXPReward;
                                    if (player.EXP / 100 == 1)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("레벨업!");
                                        Console.WriteLine($"Lv{player.Level} + 1");
                                        Console.WriteLine($"공격력 {player.ATK} + 0.5");
                                        Console.WriteLine($"방어력 {player.DEF} + 1");
                                        player.Level += player.EXP / 100;
                                        player.ATK = player.ATK + (player.Level - 1) * 0.5f;
                                        player.DEF = player.DEF + (player.Level - 1) * 1f;
                                        player.EXP %= 100;
                                        Console.WriteLine("\n엔터키를 입력하세요");
                                        Console.ReadLine();
                                    }
                                }
                                else player.HP /= 2;
                                return numChoice2 + 4;
                            default:
                                Console.WriteLine("\n잘못된입력입니다.\n엔터키를 입력하세요");
                                Console.ReadLine();
                                break;
                        }
                    }
                    while (true);
                }
                else if (numChoice == 0) { return numChoice; }
                else if (player.HP <= 0)
                {
                    Console.WriteLine("\n체력이 없습니다. 마을로 돌아가세요.\n엔터키를 입력하세요");
                    Console.ReadLine();
                    return 0;
                }
                else
                {
                    Console.WriteLine("\n잘못된입력입니다.\n엔터키를 입력하세요");
                    Console.ReadLine();
                }
            }
            while (true);
        }


        static public int EnterRestArea()
        {
            do
            {
                Console.Clear();
                Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {player.Gold} G");
                Console.WriteLine("\n1. 휴식하기");
                Console.WriteLine("0. 나가기");

                Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                int numChoice = (int)Char.GetNumericValue(Console.ReadKey().KeyChar);
                switch (numChoice)
                {
                    case 0:
                        return numChoice;
                    case 1:
                        if (player.Gold >= 500)
                        {
                            if (player.HP >= 0)
                            {
                                Console.WriteLine($"\n500G를 사용하여 체력이 {100 - player.HP}만큼 회복되었습니다.");
                                player.HP = 100;
                                Console.WriteLine($"현재체력 : {player.HP}");
                            }
                            else
                            {
                                Console.WriteLine("\n500G를 사용하여 체력이 100만큼 회복되었습니다.");
                                player.HP += 100;
                                Console.WriteLine($"현재체력 : {player.HP}");
                            }
                            player.Gold -= 500;
                        }
                        else { Console.WriteLine("\nGold가 부족합니다."); }
                        Console.WriteLine("다른행동을 하기 위해 엔터키를 눌러주세요");
                        Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("\n잘못된입력입니다.\n엔터키를 입력하세요");
                        Console.ReadLine();
                        break;
                }
            }
            while (true);
        }


        static public void MakePlayer()
        {
            Player player = new Player("Daeo", "전사", 1, 10f, 5f, 0f, 0f, 100f, 1500, 0, false, false);
        }

        static public void MakeDunGeon()
        {
            dungeons.Add(new Dungeon("쉬운 던전", 5, 10, 1000, 5));
            dungeons.Add(new Dungeon("일반 던전", 11, 15, 1700, 15));
            dungeons.Add(new Dungeon("어려운 던전", 17, 20, 2500, 30));
        }
        static public void MakeItem()
        {
            storeItems.Add(new Item("수련자 갑옷", false, 5, "수련에 도움을 주는 갑옷입니다.", 1000, false, false));
            storeItems.Add(new Item("무쇠 갑옷", false, 2, "무쇠로 만들어져 튼튼한 갑옷입니다.", 1750, false, false));
            storeItems.Add(new Item("스파르타 갑옷", false, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, false, false));
            storeItems.Add(new Item("던전용 갑옷", false, 20, "던전을 뚫기 위해 만들어진 갑옷입니다.", 500, false, false));
            storeItems.Add(new Item("낡은 검", true, 2, "쉽게 볼 수 있는 낡은 검 입니다.", 600, false, false));
            storeItems.Add(new Item("청동 도끼", true, 5, "어디선가 사용됐던거 같은 도끼입니다.", 600, false, false));
            storeItems.Add(new Item("스파르타 창", true, 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2400, false, false));
            storeItems.Add(new Item("던전용 무기", true, 20, "던전을 뚫기 위해 만들어진 무기입니다.", 500, false, false));
        }

        /*
        static void SaveData()
        {
            JObject SaveRPG = new JObject
            {
                { "Name", player.Name },
                { "Job", player.Job },
                { "Level", player.Level },
                { "ATK", player.ATK },
                { "DEF", player.DEF },
                { "ItemATK", player.ItemATK },
                { "ItemDEF", player.ItemDEF },
                { "HP", player.HP },
                { "Gold", player.Gold },
                { "EXP", player.EXP },
                { "IsNumberTag", isNumberTag },
                { "IsSale", player.IsSale },
                { "IsClearDungeon", player.IsClearDungeon },
                { "StoreItems", JToken.FromObject(storeItems) }, // List<Item> 직렬화
                { "InventoryItems", JToken.FromObject(inventoryItems) }, // List<Item> 직렬화
                { "Dungeons", JToken.FromObject(dungeons) } // List<Dungeon> 직렬화
            };

            string path = @"C:\Users\thseo\source\Spartan\personal\Assignment_Personal\test.json"; // 저장할 경로 설정
            File.WriteAllText(path, SaveRPG.ToString());
            Console.WriteLine("게임 데이터가 저장되었습니다.");
        }
        */
        /*
        static void LoadData()
        {
            string path = @"C:\Users\thseo\source\Spartan\personal\Assignment_Personal\test.json"; // 불러올 경로 설정

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                JObject loadedData = JObject.Parse(json);

                player.Name = loadedData["Name"]?.ToString();
                player.Job = loadedData["Job"]?.ToString();
                player.Level = (int)loadedData["Level"];
                player.ATK = (float)loadedData["ATK"];
                player.DEF = (float)loadedData["DEF"];
                player.ItemATK = (float)loadedData["ItemATK"];
                player.ItemDEF = (float)loadedData["ItemDEF"];
                player.HP = (float)loadedData["HP"];
                player.Gold = (int)loadedData["Gold"];
                player.EXP = (int)loadedData["EXP"];
                isNumberTag = (bool)loadedData["IsNumberTag"];
                player.IsSale = (bool)loadedData["IsSale"];
                player.IsClearDungeon = (bool)loadedData["IsClearDungeon"];

                // List<Item> 및 List<Dungeon> 역직렬화
                storeItems = loadedData["StoreItems"].ToObject<List<Item>>();
                inventoryItems = loadedData["InventoryItems"].ToObject<List<Item>>();
                dungeons = loadedData["Dungeons"].ToObject<List<Dungeon>>();

                Console.WriteLine("게임 데이터가 로드되었습니다.");
            }
            else
            {
                Console.WriteLine("저장된 데이터가 없습니다.");
            }
        }
        */
    }
}
