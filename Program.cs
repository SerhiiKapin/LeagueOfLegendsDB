namespace LeagueOfLegendsDB
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CharacterRepository repository = new CharacterRepository();
            using (ApplicationContext db = new ApplicationContext())
            {
                // створюємо стартові об'єкти класу Character
                db.Classes.Add(new Class { Name = "Diver" });
                db.Classes.Add(new Class { Name = "Skirmisher" });
                db.Classes.Add(new Class { Name = "Warden" });
                db.Classes.Add(new Class { Name = "Mage" });
                db.Classes.Add(new Class { Name = "Marksman" });
                db.SaveChanges();
                Character irelia = new Character { Name = "Irelia", ClassID = 1, BlueEssence = 4800, RiotPoints = 880,  ImagePath = @"C:\Users\saga2\OneDrive\Робочий стіл\роботи 4 курс\NET\lb2\images\irelia.png" };
                Character yasuo = new Character { Name = "Yasuo", ClassID = 2, BlueEssence = 6300, RiotPoints = 975, ImagePath = @"C:\Users\saga2\OneDrive\Робочий стіл\роботи 4 курс\NET\lb2\images\yasuo.png" };
                Character galio = new Character { Name = "Galio", ClassID = 3, BlueEssence = 3150, RiotPoints = 790, ImagePath = @"C:\Users\saga2\OneDrive\Робочий стіл\роботи 4 курс\NET\lb2\images\galio.png" };
                Character annie = new Character { Name = "Annie", ClassID = 4, BlueEssence = 450, RiotPoints = 260, ImagePath = @"C:\Users\saga2\OneDrive\Робочий стіл\роботи 4 курс\NET\lb2\images\annie.png" };
                Character ashe = new Character { Name = "Ashe", ClassID = 5, BlueEssence = 450, RiotPoints = 260, ImagePath = @"C:\Users\saga2\OneDrive\Робочий стіл\роботи 4 курс\NET\lb2\images\ashe.png" };
                Character blitzcrank = new Character { Name = "Blitzcrank", ClassID = 3, BlueEssence = 3150, RiotPoints = 790, ImagePath = @"C:\Users\saga2\OneDrive\Робочий стіл\роботи 4 курс\NET\lb2\images\blitzcrank.png" };
                Character brand = new Character { Name = "Brand", ClassID = 4, BlueEssence = 4800, RiotPoints = 880, ImagePath = @"C:\Users\saga2\OneDrive\Робочий стіл\роботи 4 курс\NET\lb2\images\brand.png" };

                List<Character> list = new List<Character>();
                list.Add(irelia);
                list.Add(yasuo);
                list.Add(galio);
                list.Add(annie);
                list.Add(blitzcrank);
                list.Add(ashe);
                list.Add(brand);
                // додаєм в базу даних
                repository.AddCharacters(list);
                db.SaveChanges();
                Console.WriteLine("Данi збереженi");

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Form1 mainForm = new Form1();

                Application.Run(mainForm);
            }
        }
    }
}