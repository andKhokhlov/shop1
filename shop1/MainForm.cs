using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using shop1.Data;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;
using Application = Microsoft.Office.Interop.Word.Application;
using Document = Microsoft.Office.Interop.Word.Document;

namespace shop1
{
    public partial class MainForm : Form
    {
        shop1.Data.Description descriptionInstance = new shop1.Data.Description();

        // Словарь для хранения описаний картинок
        private Dictionary<Guna2PictureBox, string> imageDescriptions = new Dictionary<Guna2PictureBox, string>();

        public MainForm()
        {
            string descript = descriptionInstance.GetDescription();
            InitializeComponent();
            InitializeImageDescriptions(descript); // Инициализация описаний картинок
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Начальное скрытие метки с описанием
            lblDescription.Visible = false;
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
          // Application.Exit();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            Guna2PictureBox clickedPictureBox = (Guna2PictureBox)sender;

            // Сначала скрываем текущую основную картинку
            guna2Transition1.HideSync(MainPic);

            // Установка новой основной картинки
            MainPic.Image = clickedPictureBox.Image;
            MainPic1.Image = clickedPictureBox.Image;

            // Показ новой основной картинки с анимацией
            guna2Transition1.ShowSync(MainPic);

            // Показываем описание картинки
            ShowImageDescription(clickedPictureBox);
        }

        // Метод для инициализации описаний картинок
        private void InitializeImageDescriptions(string descript)
        {
            // Добавление описаний в словарь
            imageDescriptions.Add(guna2PictureBox1, "Крест – символ веры, силы и преданности." +
                         "\nКаждая его ветвь несет в себе глубокий смысл и значение." +
                         "\nЭтот подвес для мужчины станет не только украшением, " +
                         "\nно и мощным напоминанием о его уверенности в своих убеждениях.");
            imageDescriptions.Add(guna2PictureBox2, "Эти часы - не просто измеритель времени. " +
                "\nОни являются воплощением элегантности и стиля, умело сочетая в себе " +
                "\nклассический дизайн с современными технологиями." +
                "\nКаждая минута, каждая секунда, засеченные на циферблате, " +
                "\nнапоминают о ценности времени и неуклонном движении вперед.");
            imageDescriptions.Add(guna2PictureBox3, descript);
            imageDescriptions.Add(guna2PictureBox4, "Описание картинки 4");
            imageDescriptions.Add(guna2PictureBox5, "Описание картинки 5");
            imageDescriptions.Add(guna2PictureBox6, "Описание картинки 6");
            // Продолжайте добавлять описания для других картинок
        }

        // Метод для отображения описания выбранной картинки
        private void ShowImageDescription(Guna2PictureBox pictureBox)
        {
            // Проверяем, есть ли описание для данной картинки
            if (imageDescriptions.ContainsKey(pictureBox))
            {
                // Устанавливаем текст метки равным описанию картинки
                lblDescription.Text = imageDescriptions[pictureBox];
                // Показываем метку с анимацией
                lblDescription.Visible = true;
            }
            else
            {
                // Если описание не найдено, скрываем метку
                lblDescription.Visible = false;
            }
        }

        private void lblDescription_Click(object sender, EventArgs e)
        {

        }

        private void addShop_Click(object sender, EventArgs e)
        {
            sidebarTimer.Start();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }
        bool sidebarExpand;
        private void sidebarTimer_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {

                sidebar.Width -= 10;
                if (sidebar.Width == sidebar.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    sidebarTimer.Stop();
                }
            }
            else
            {
                sidebar.Width += 10;
                if (sidebar.Width == sidebar.MaximumSize.Width)
                {
                    sidebarExpand = true;
                    sidebarTimer.Stop();
                }
            }
        }

        private void buy_Click(object sender, EventArgs e)
        {

        }

        private void MainPic1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string inputFilePath = $@"{System.Windows.Forms.Application.StartupPath}\Receipt\Exampple.docx";
            string outputFilePath = $@"{System.Windows.Forms.Application.StartupPath}\Receipt\Чек {DateTime.Now.ToString("dd-mm-yyyy hh-mm-ss")}.pdf";

            string name = guna2TextBox1.Text;
            string lastname = guna2TextBox2.Text;
            string city = guna2TextBox3.Text;
            string street = guna2TextBox4.Text;
            string home = guna2TextBox5.Text;

            var replacements = new System.Collections.Generic.Dictionary<string, string>
            {
             { "<Num>", "465837" },
             { "<Name>", name},
             {"<LastName>", lastname},
             {"<City>", city },
             { "<Street>", street },
             {"<Home>",home},
             { "<InfoService1>", "Подвеска" },  { "<Quantity1>", "1шт." },  { "<Cost1>", "4999руб." },
             { "<DateTime>", $"{DateTime.Now.ToString("dd.mm.yyyy hh.mm.ss")}" },
             { "<Sum>", "4999" },
            };
            if (ReplaceTags(inputFilePath, outputFilePath, replacements) == true)
            {
                MessageBox.Show("Чек успешно сформирован.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public static bool ReplaceTags(string inputFilePath, string outputFilePath, System.Collections.Generic.Dictionary<string, string> replacements)
        {
            Application wordApp = new Application();

            try
            {
                Document doc = wordApp.Documents.Open(inputFilePath, ReadOnly: true);

                Range range = doc.Content;
                Document newDoc = wordApp.Documents.Add();
                range.Copy();
                newDoc.Range().Paste();

                foreach (var replacement in replacements)
                {
                    newDoc.Content.Find.Execute(FindText: replacement.Key, ReplaceWith: replacement.Value, Replace: WdReplace.wdReplaceAll);
                }

                newDoc.SaveAs2(outputFilePath, WdSaveFormat.wdFormatPDF);
                newDoc.Close(SaveChanges: false);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при формировании чека: " + ex.Message, "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            finally
            {
                wordApp.Quit();
            }
        }

        private void sidebar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
