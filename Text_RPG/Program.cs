using System.Runtime.CompilerServices;
//Text_RPG_5가 제출내용입니다!//Text_RPG_5가 제출내용입니다!//Text_RPG_5가 제출내용입니다!//Text_RPG_5가 제출내용입니다!//Text_RPG_5가 제출내용입니다!
namespace Text_RPG
{
    class GameManager
    {
        //Class간 이동관리, 데이터관리

        //도전과제 저장
    }


    // Text_RPG.. Link .. Start!!
    class LinkStart
    {
        public void MakeNameJob(ref string name, ref string job)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("원하시는 이름을 입력해주세요.");
                Console.Write(">> ");
                name = Console.ReadLine();
                Console.WriteLine($"\n정말로 {name}(으)로 하시겠습니까?\n");
                Console.WriteLine("1. 예     2. 아니오");
                Console.Write(">> ");
                do
                {
                    ConsoleKeyInfo nameChoice = Console.ReadKey();
                    if (nameChoice.Key == ConsoleKey.D1)
                    {
                        break;
                    }
                    else if (nameChoice.Key == ConsoleKey.D2)
                    {
                        name = null;
                        break;
                    }
                    else
                    {
                        Console.Write("\b");
                    }
                }
                while (true);
            }
            while (name == null);

            //!! InVillage class로 가는 코드
            //Console.WriteLine($"반갑습니다. {name}. 직업을 선택해 주세요");
            //Console.WriteLine("");
        }
    }


    // in Village
    public enum EVillage
    {
        상태보기 = 1,
        인벤토리,
        상점
    }
    class InVillage
    {
        public void Village()
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
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
                //ConsoleKeyInfo villageChoice = Console.ReadKey();
                int villageChoice = (int)Char.GetNumericValue(Console.ReadKey().KeyChar);
                if (villageChoice > 0 && villageChoice <= Enum.GetValues(typeof(EVillage)).Length)
                {
                    Console.WriteLine("입력이 정확함");
                    break;
                }

                //if ()
                Console.Clear();
            }
            while (true);
        }
    }


    //status window !추가 헛소리하면 반복문
    class Status : Player
    {
        public void StatusWindow()
        {
            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
            Console.WriteLine("Lv. " + Level);
            Console.WriteLine("공격력 : " + ATK);
            Console.WriteLine("방어력 : " + DEF);
            Console.WriteLine("체  력 : " + HP);
            Console.WriteLine("Gold : " + Gold);

            Console.WriteLine("\n0. 나가기");

            Console.Write("원하시는 행동을 입력해주세요.\n>>");
            ConsoleKeyInfo statusChoice = Console.ReadKey();
        }
    }


    //Inventory
    class Inventory : Player
    {
        //델리게이트
        public void InventoryWindow()
        {
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유중인 아이템을 관리할 수 있습니다.\n");

            Console.WriteLine("아이템 목록");
            ItemList();

        }
    }


    //Equipment Management
    class EM : Player
    {
        //델리게이트
        //ItemList();
        //플레이어 장착 함수
    }


    //Store
    class Store : Player
    {
        //델리게이트
        public void InventoryWindow()
        {
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            ItemList();
        }
    }


    //Item - parents
    abstract class Item
    {
        public void ItemList()
        {
            //대충 목록들
        }
    }
    interface IAttack
    {
        public int IATK { get; set; }
    }
    interface IDefence
    {
        public int IDEF { get; set; }
    }


    //Player status
    class Player
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int ATK { get; set; }
        public int DEF { get; set; }
        public int HP { get; set; }
        public int Gold { get; set; }

        public void ItemList()
        {
            //대충 목록들
        }

        public void ItemEquip()
        {
            //장착여부
        }
    }


    class Program
    {
        //Set Name & Job
        static void Main(string[] args)
        {
            string name = null;
            string job = null;

            //Start Game
            LinkStart linkStart = new LinkStart();
            linkStart.MakeNameJob(ref name, ref job);
            InVillage inVillage = new InVillage();
            inVillage.Village();

        }

    }
}
