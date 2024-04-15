using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using shop1.Data;

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
            Application.Exit();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            Guna2PictureBox clickedPictureBox = (Guna2PictureBox)sender;

            // Сначала скрываем текущую основную картинку
            guna2Transition1.HideSync(MainPic);

            // Установка новой основной картинки
            MainPic.Image = clickedPictureBox.Image;

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
                "\nнапоминают о ценности времени и неуклонном движении вперед." );
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
    }
}
