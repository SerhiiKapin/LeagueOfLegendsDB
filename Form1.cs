using System.Drawing.Printing;
using System.Windows.Forms;

namespace LeagueOfLegendsDB
{
    public partial class Form1 : Form
    {
        private readonly CharacterRepository repository = new CharacterRepository();
        private int currentPage = 1;
        private const int CharactersPerPage = 4;

        private List<Label> labels;
        private List<PictureBox> pictureBoxes;

        private TextBox textBoxSearch;
        private Button buttonSearch;

        public Form1()
        {
            InitializeComponent();
            InitializeLabelsAndPictureBoxes();
            InitializeSearchControls();
            DisplayCharactersPage(currentPage);
        }
        private void InitializeSearchControls()
        {
            textBoxSearch = new TextBox();
            textBoxSearch.Location = new Point(510, 10);
            textBoxSearch.Size = new Size(150, 20);
            Controls.Add(textBoxSearch);

            buttonSearch = new Button();
            buttonSearch.Text = "Search";
            buttonSearch.Location = new Point(670, 10);
            buttonSearch.Click += ButtonSearch_Click;
            Controls.Add(buttonSearch);
        }
        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            string searchCriteria = textBoxSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchCriteria))
            {
                ClearScreen();
                InitializeLabelsAndPictureBoxes();
                // Пошук за допомогою LINQ to Entities
                List<Character> searchResults = repository.GetCharactersByName(searchCriteria);

                for (int i = 0; i < Math.Min(CharactersPerPage, searchResults.Count); i++)
                {
                    Character character = searchResults[i];
                    Label label = labels[i];
                    PictureBox pictureBox = pictureBoxes[i];

                    if (character != null)
                    {
                        label.Text = $"ID: {character.Id}\nName: {character.Name}\nClass: {character.Class?.Name}\nBlue Essence: {character.BlueEssence}\nRiot Points: {character.RiotPoints}";

                        if (!string.IsNullOrEmpty(character.ImagePath))
                        {
                            pictureBox.Image = Image.FromFile(character.ImagePath);
                        }
                        else
                        {
                            pictureBox.Image = null;
                        }
                    }
                }
            }
            else
            {
                DisplayCharactersPage(currentPage);
            }
        }

        private void InitializeLabelsAndPictureBoxes()
        {
            int charactersPerRow = 2;
            labels = new List<Label>();
            for (int i = 0; i < CharactersPerPage; i++)
            {
                Label label = new Label();
                label.Location = new Point(10 + (i % charactersPerRow) * 260, 10 + (i / charactersPerRow) * 220);
                label.AutoSize = true;
                label.Font = new Font(label.Font, FontStyle.Bold);
                labels.Add(label);
                Controls.Add(label);
            }

            pictureBoxes = new List<PictureBox>();
            for (int i = 0; i < CharactersPerPage; i++)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Location = new Point(10 + (i % charactersPerRow) * 260, 100 + (i / charactersPerRow) * 240);
                pictureBox.Size = new Size(240, 240);
                pictureBoxes.Add(pictureBox);
                Controls.Add(pictureBox);
            }
        }

        private void ClearScreen()
        {
            foreach (var label in labels)
            {
                Controls.Remove(label);
            }
            labels.Clear();

            foreach (var pictureBox in pictureBoxes)
            {
                Controls.Remove(pictureBox);
            }
            pictureBoxes.Clear();
        }


        private void DisplayCharactersPage(int pageNumber)
        {
            ClearScreen();
            InitializeLabelsAndPictureBoxes();

            List<Character> characters = repository.GetCharactersPage(pageNumber);

            for (int i = 0; i < characters.Count; i++)
            {
                Label label = labels[i];
                PictureBox pictureBox = pictureBoxes[i];
                Character character = characters[i];

                if (character != null)
                {
                    label.Text = $"ID: {character.Id}\nName: {character.Name}\nClass: {character.Class?.Name}\nBlue Essence: {character.BlueEssence}\nRiot Points: {character.RiotPoints}";

                    try
                    {
                        pictureBox.Image = Image.FromFile(character.ImagePath);
                    }
                    catch (Exception)
                    {
                        // Можна змінити в майбутньому на виведення картинки за замовчуванням
                        pictureBox.Image = null;
                    }
                }
                else
                {
                    label.Text = "Character not found.";
                    pictureBox.Image = null;
                }
            }

            if (pageNumber == 1)
            {
                button2.Enabled = false;
            }
            else
            {
                button2.Enabled = true;
            }
            if (repository.GetCharactersPage(pageNumber + 1).Count > 0)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DisplayCharactersPage(currentPage + 1);
            currentPage++;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DisplayCharactersPage(currentPage - 1);
            currentPage--;
        }
    }
}