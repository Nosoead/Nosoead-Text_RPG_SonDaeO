using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
//Text_RPG_5가 제출내용입니다!//Text_RPG_5가 제출내용입니다!//Text_RPG_5가 제출내용입니다!//Text_RPG_5가 제출내용입니다!//Text_RPG_5가 제출내용입니다!
namespace Text_RPG_3
{
    public enum EVillage
    {
        상태보기 = 1,
        인벤토리,
        상점
    }
    //게임 시작 화면
    class Menu
    {
        public int EnterMenu()
        {
            Console.Clear();
            do
            {

                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
                for (int i = 1; i <= Enum.GetValues(typeof(EVillage)).Length; i++)
                {
                    Console.WriteLine(i + ". " + (EVillage)i);
                }
                Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                //ConsoleKeyInfo villageChoice = Console.ReadKey();
                int villageChoice = (int)Char.GetNumericValue(Console.ReadKey().KeyChar);
                if (villageChoice > 0 && villageChoice <= Enum.GetValues(typeof(EVillage)).Length)
                {
                    return villageChoice;
                }

                //if ()
                Console.Clear();
            }
            while (true);
        }
    }

    //상태보기
    class Status : Player
    {
        public int EnterStatus()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("상태보기");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
                Console.WriteLine($"Chad ({job})");
                Console.WriteLine("Lv. " + Level);
                Console.WriteLine("공격력 : " + ATK);
                Console.WriteLine("방어력 : " + DEF);
                Console.WriteLine("체  력 : " + HP);
                Console.WriteLine("Gold : " + Gold);
                Console.WriteLine("\n0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                int villageChoice = (int)Char.GetNumericValue(Console.ReadKey().KeyChar);
                switch (villageChoice)
                {
                    case 0:
                        return villageChoice;
                }
            }
            while (true);
        }
    }

    //인벤토리
    class Inventory
    {
        public int EnterInventory(ref int ATK, ref int DEF)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유중인 아이템을 관리할 수 있습니다.\n");

                Console.WriteLine("[아이템 목록]");


                Console.WriteLine("\n1. 장착 관리");
                Console.WriteLine("0. 나가기");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                int villageChoice = (int)Char.GetNumericValue(Console.ReadKey().KeyChar);
                if (villageChoice == 0)
                {
                    return villageChoice;
                }
                if (villageChoice == 1)
                {
                    return villageChoice;
                }
            }
            while (true);
        }
    }


    //상점
    class Store
    {
        public int EnterStore(ref int Gold)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{Gold} G");
                
                Console.WriteLine("\n0.나가기");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>>");
                int villageChoice = (int)Char.GetNumericValue(Console.ReadKey().KeyChar);
                switch (villageChoice)
                {
                    case 0:
                        return villageChoice;
                }
            }
            while (true);
        }
    }

    class Player
    {
        public string Name { get; set; }
        public string job { get; set; }
        public int Level { get; set; }
        public int ATK { get; set; }
        public int DEF { get; set; }
        public int HP { get; set; }
        public int Gold { get; set; }
    }

    //아이템 구매
    internal class Program
    {
        static void Main(string[] args)
        {
            int menuNum = 0;
            int Gold = 1500;
            int ATK = 0;
            int DEF = 0;
            Player player = new Player();
            player.Name = "DaeO";
            player.job = "전사";
            player.Level = 1;
            player.ATK = 10;
            player.DEF = 5;
            player.HP = 100;
            player.Gold = Gold;
            Menu menu = new Menu();
            Status status = new Status();
            Inventory inventory = new Inventory();
            Store store = new Store();

            //입장
            //불러와서 행동
            while(true)
            {
                switch(menuNum)
                {
                    case 0:
                        menuNum = menu.EnterMenu(player);
                        break;
                    case 1:
                        menuNum = status.EnterStatus();
                        break;
                    case 2:
                        menuNum = inventory.EnterInventory(ref ATK, ref DEF);
                        player.ATK += ATK; player.DEF += DEF;
                        break;
                    case 3:
                        menuNum = store.EnterStore(ref Gold);
                        player.Gold = Gold;
                        break;
                }

            }
        }
    }
}
