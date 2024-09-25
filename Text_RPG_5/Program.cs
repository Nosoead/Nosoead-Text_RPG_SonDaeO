using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Text_RPG_5
{
    //첫화면 메뉴창을 열거로 했습니다. 메뉴가 추가될 때, 열거를 추가하고 Main에 switch로 연결점을 늘릴 수 있습니다.
    public enum EMenu
    {
        메뉴,
        상태보기,
        인벤토리,
        상점,
        던전입장,
        휴식
    }

    //캐릭터도, 장소도 전부 클래스로하고 Main에 While문 하나로 하고 싶었으나 지식이 부족해 그러지 못 했습니다.
    public class Program
    {   
        //메뉴를 지배하는 정수입니다.
        static int menuNum = 0;
        //캐릭터와 관련된 지표를 static으로 선언 & 초깃값을 주었습니다.
        static string name = "DaeO";
        static string job = "전사";
        static int level = 1;
        static float ATK = 10f;
        static float DEF = 5f;
        static float itemATK = 0f;
        static float itemDEF = 0f;
        static float HP = 100f;
        static int gold = 1500;
        static int EXP = 0;
        static bool isNumberTag = false;
        static bool isSale = false;
        static bool isClearDungeon = false;

        //아이템을 클래스로 두었고, storeItems에는 모든 아이템이 inventoryItems에는 구매한 것이 보관(Add)되고 파는 것은 제거(Remove) 했습니다.
        //착용여부를 위해 배열을 했습니다. 매개변수가 없는 생성자를 하나 만들까 했다가 그냥 배열이 편해보였습니다.
        static List<Item> storeItems = new List<Item>();
        static List<Item> inventoryItems = new List<Item>();
        static Item[] weaponEquipment = new Item[1];
        static Item[] armorEquipment = new Item[1];

        //던전을 추가할 수 있습니다. 사실 이제 보니 캐릭터도 이렇게 될 것 같다는 느낌이 드네요.
        static List<Dungeon> dungeons = new List<Dungeon>();

        //확률과 관련된 요소를 random을 사용하여 해결했습니다.
        static Random random = new Random();
        static void Main(string[] args)
        {
            //아이템을 만들고, 던전도 만들고 나서 데이터를 불러옵니다.
            MakeItem();
            MakeDunGeon();

            LoadData();

            //While문을 통해 메뉴에서 창으로 들어가고 숫자값을 리턴받아 해당 값으로 다시 들어가는 무한 반복을 썼고, 리턴 받아 올 때, 데이터를 저장합니다.
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

                SaveData();

            }

        }


        //열거 0번 메뉴입니다.
        //열거를 한 번 사용해 보고 싶었습니다. ReadKey는 GPT로 배웠는데 완전이해보다는 input output만 이해하고 활용했습니다.
        static public int EnterMenu()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
                for (int i = 1; i < Enum.GetValues(typeof(EMenu)).Length; i++)
                {
                    Console.WriteLine(i + ". " + (EMenu)i);
                }
                Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                int numChoice = (int)Char.GetNumericValue(Console.ReadKey().KeyChar);
                if (numChoice > 0 && numChoice < Enum.GetValues(typeof(EMenu)).Length)
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


        //열거 1번 스테이터스입니다.
        //초기값을 바탕으로 스테이터스가 표시되도록 했습니다.
        public static int EnterStatus()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("상태보기");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
                Console.WriteLine($"Chad ({job})");
                Console.WriteLine($"Lv. {level}");
                Console.WriteLine($"공격력 : {ATK + itemATK} (+{itemATK})");
                Console.WriteLine($"방어력 : {DEF + itemDEF} (+{itemDEF})");
                Console.WriteLine($"체  력 : {HP}");
                Console.WriteLine($"Gold : {gold}");
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


        //열거 2번 인벤토리입니다.
        //장비장착에 들어가면 숫자가 나오도록 Main에 있는 isNumberTag를 bool값으로 사용했습니다.
        //장비 장착 여부는 Item class내에서 변경합니다. 내용이 길어 반복문에 주석을 달겠습니다.
        static public int EnterInventory()
        {
            do
            {
                Console.Clear();
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

                //값을 return하여 나갈지, 인벤토리창에 들어온 화면을 보여줄지, 장착 관리 화면을 보여줄지 체크합니다.
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


                //storeitem에서 구입하면 inventoryItems의 List에 추가할 수 있습니다.
                //장비를 착용, 해체, 1장구만 착용하도록 검토하는 과정을 담고 있습니다.
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

                //장비를 착용, 해제했을 때, 스테이터스에 반영합니다.
                if (weaponEquipment[0] == null) { itemATK = 0; }
                else { itemATK = weaponEquipment[0].ItemPower; }
                if (armorEquipment[0] == null) { itemDEF = 0; }
                else { itemDEF = armorEquipment[0].ItemPower; }

            }
            while (true);
        }


        //열거 3번 상점입니다.
        //미리 나열한 아이템이 종합적으로 있고 아이템을 구매하여 inventoryitems에 add, 판매하여 Remove를 합니다.
        static public int EnterStore()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{gold} G\n");
                Console.WriteLine("[아이템 목록]");
                if (!isNumberTag)       //상점 화면
                {
                    for (int i = 0; i < storeItems.Count; i++)
                    {
                        Console.Write("- ");
                        storeItems[i].ItemTextStore();
                    }
                }
                else if (isNumberTag && !isSale)    //구매 화면
                {
                    for (int i = 0; i < storeItems.Count; i++)
                    {
                        Console.Write($"- {i + 1} ");
                        storeItems[i].ItemTextStore();
                    }
                }
                else if (isNumberTag && isSale)     //판매 화면
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
                            isSale = false; numChoice = -1; break;
                        case 2:
                            isSale = true; numChoice = -1; break;
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

                //구매할 때, 구매여부와 골드가 충분한지를 확인하고 구매를 합니다.
                if (numChoice >= 1 && numChoice <= (storeItems.Count) && isNumberTag && !isSale)
                {
                    if (gold >= storeItems[numChoice - 1].ItemPrice && !storeItems[numChoice - 1].IsPurchaseItem)
                    {
                        storeItems[numChoice - 1].IsPurchaseItem = true;
                        gold -= storeItems[numChoice - 1].ItemPrice;
                        inventoryItems.Add(storeItems[numChoice - 1]);
                    }
                    else if (storeItems[numChoice - 1].IsPurchaseItem)
                    {
                        Console.WriteLine("\n이미 구입했습니다.\n엔터키를 입력하세요");
                        Console.ReadLine();
                    }
                    else if (gold < storeItems[numChoice - 1].ItemPrice && !storeItems[numChoice - 1].IsPurchaseItem)
                    {
                        Console.WriteLine("\n돈이 모자랍니다.\n엔터키를 입력하세요");
                        Console.ReadLine();
                    }
                }

                //판매할 떄, 인벤토리창의 목록을 팔고, 파는 inventoryitems의 이름과 같은 storeitems을 찾아 다시 팔 수 있도록 합니다.
                if (numChoice >= 1 && numChoice <= (inventoryItems.Count) && isNumberTag && isSale)
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
                        gold += inventoryItems[numChoice - 1].ItemPrice * 85 / 100;
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


        //열거 4번 던전입니다.
        //MakeDungeon함수로 만든 던전을 방어력에 맞게 클리어 확률을 조절하고, 공격력에 맞게 추가보상을 획득합니다.
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
                if (numChoice > 0 && numChoice <= dungeons.Count && HP > 0)
                {   //던전 입장인데 체력이 0이나 음수면 못들어 옵니다. !!!!randHPNum할때 자꾸 int값아니라고 오류 뜨던데 뭔소리인지 모르겠습니다. float도 랜덤 되는걸로 아는데요.
                    randClearNum = random.Next(1, 11);
                    randReward = random.Next((int)(ATK + itemATK), (int)(ATK + itemATK) * 2);
                    randHPNum = random.Next((int)(20 - (DEF + itemDEF) + dungeons[numChoice - 1].RecommendedDEF),
                                            (int)(36 - (DEF + itemDEF) + dungeons[numChoice - 1].RecommendedDEF));
                    do
                    {
                        Console.Clear();
                        if ((DEF + itemDEF) >= dungeons[numChoice - 1].RecommendedDEF ||
                            ((DEF + itemDEF) < dungeons[numChoice - 1].RecommendedDEF && randClearNum > 4))
                        {
                            Console.WriteLine("던 전 클 리 어");
                            Console.WriteLine($"축하합니다!!\n{dungeons[numChoice - 1].DungeonName}을 클리어 하였습니다.\n");
                            Console.WriteLine("[탐험 결과]");
                            Console.WriteLine($"체력 {HP} -> {HP - randHPNum}");
                            Console.WriteLine($"Gold {gold} G -> {gold + dungeons[numChoice - 1].GoldReward + (dungeons[numChoice - 1].GoldReward * (int)randReward / 100)} G");
                            isClearDungeon = true;
                        }
                        else
                        {
                            Console.WriteLine($"던 전 클 리 어 실 패");
                            Console.WriteLine($"안타깝네요\n {dungeons[numChoice - 1].DungeonName}을 클리어 실패 했습니다.\n");
                            Console.WriteLine("[탐험 결과]");
                            Console.WriteLine($"체력 {HP} -> {HP / 2}");
                            isClearDungeon = false;
                        }

                        Console.WriteLine("\n0. 나가기");
                        Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                        int numChoice2 = (int)Char.GetNumericValue(Console.ReadKey().KeyChar);
                        switch (numChoice2)         //앞서 입력이 잘못되어 화면을 다시보여줄 때, 업데이트가 계속되어 화면이 전환될 때 업데이트가 되도록 했습니다.
                        {                           //아래는 클리어시 보상과 경험치, 경험치에 따른 스테이터스 변화를 나타내주고 있습니다.
                            case 0:
                                if (isClearDungeon)
                                {
                                    HP -= randHPNum;
                                    gold = gold + dungeons[numChoice - 1].GoldReward + dungeons[numChoice - 1].GoldReward * (int)randReward / 100;
                                    EXP += dungeons[numChoice - 1].EXPReward;
                                    if (EXP / 100 == 1)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("레벨업!");
                                        Console.WriteLine($"Lv{level} + 1");
                                        Console.WriteLine($"공격력 {ATK} + 0.5");
                                        Console.WriteLine($"방어력 {DEF} + 1");
                                        level += EXP / 100;
                                        ATK = ATK + (level - 1) * 0.5f;
                                        DEF = DEF + (level - 1) * 1f;
                                        EXP %= 100;
                                        Console.WriteLine("\n엔터키를 입력하세요");
                                        Console.ReadLine();
                                    }
                                }
                                else HP /= 2;
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
                else if (HP <= 0)
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


        //열거 6번 입니다.
        //휴식을 취하고 최대 100까지 회복할 수 있습니다.
        //체력이 100이 안넘도록 설정을 했고, 골드유무에 따라 다른 결과가 나옵니다.
        static public int EnterRestArea()
        {
            do
            {
                Console.Clear();
                Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {gold} G");
                Console.WriteLine("\n1. 휴식하기");
                Console.WriteLine("0. 나가기");

                Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                int numChoice = (int)Char.GetNumericValue(Console.ReadKey().KeyChar);
                switch (numChoice)
                {
                    case 0:
                        return numChoice;
                    case 1:
                        if (gold >= 500)
                        {
                            if (HP >= 0)
                            {
                                Console.WriteLine($"\n500G를 사용하여 체력이 {100 - HP}만큼 회복되었습니다.");
                                HP = 100;
                                Console.WriteLine($"현재체력 : {HP}");
                            }
                            else
                            {
                                Console.WriteLine("\n500G를 사용하여 체력이 100만큼 회복되었습니다.");
                                HP += 100;
                                Console.WriteLine($"현재체력 : {HP}");
                            }
                            gold -= 500;
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

        
        //던전생성 함수입니다.
        static public void MakeDunGeon()
        {
            dungeons.Add(new Dungeon("쉬운 던전", 5, 10, 1000, 5));
            dungeons.Add(new Dungeon("일반 던전", 11, 15, 1700, 15));
            dungeons.Add(new Dungeon("어려운 던전", 17, 20, 2500, 30));
        }


        //아이템생성 함수입니다.
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


        //세이브 함수입니다. 사실 다른 사람에 묻고 블로그 포스팅한것도 보고 GPT도 써서 오로지 제것이 아닙니다.
        static void SaveData()
        {
            JObject SaveData = new JObject
            {
                { "Name", name },
                { "Job", job },
                { "Level", level },
                { "ATK", ATK },
                { "DEF", DEF },
                { "ItemATK", itemATK },
                { "ItemDEF", itemDEF },
                { "HP", HP },
                { "Gold", gold },
                { "EXP", EXP },
                { "IsNumberTag", isNumberTag },
                { "IsSale", isSale },
                { "IsClearDungeon", isClearDungeon },
                { "StoreItems", JToken.FromObject(storeItems) }, // List<Item> 직렬화
                { "InventoryItems", JToken.FromObject(inventoryItems) }, // List<Item> 직렬화
                { "Dungeons", JToken.FromObject(dungeons) } // List<Dungeon> 직렬화
            };

            string path = @"C:\Users\thseo\OneDrive\Desktop\Unity6\2.Camp\2. C\Assignment\personal\Assignment_Personal\Text_RPG_5.json"; // 저장할 경로 설정
            File.WriteAllText(path, SaveData.ToString());
            Console.WriteLine("게임 데이터가 저장되었습니다.");
        }


        //로드 함수입니다. 이하 동문입니다.
        static void LoadData()
        {
            string path = @"C:\Users\thseo\OneDrive\Desktop\Unity6\2.Camp\2. C\Assignment\personal\Assignment_Personal\Text_RPG_5.json"; // 불러올 경로 설정

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                JObject loadedData = JObject.Parse(json);

                name = loadedData["Name"].ToString();
                job = loadedData["Job"].ToString();
                level = (int)loadedData["Level"];
                ATK = (float)loadedData["ATK"];
                DEF = (float)loadedData["DEF"];
                itemATK = (float)loadedData["ItemATK"];
                itemDEF = (float)loadedData["ItemDEF"];
                HP = (float)loadedData["HP"];
                gold = (int)loadedData["Gold"];
                EXP = (int)loadedData["EXP"];
                isNumberTag = (bool)loadedData["IsNumberTag"];
                isSale = (bool)loadedData["IsSale"];
                isClearDungeon = (bool)loadedData["IsClearDungeon"];

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

    }
}
