using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace shop1
{

    public partial class LoginForm : Form
    {
        DataBase dataBase = new DataBase();



        public LoginForm()
        {
            InitializeComponent();
        }

       
        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            var loginUser = guna2TextBox1.Text;
            var loginPassword = guna2TextBox2.Text;

            // Создание экземпляра класса DataBase
            DataBase dataBase = new DataBase();

            // Получение объекта подключения к базе данных
            SqlConnection connection = dataBase.getConnection();

            // SQL-запрос для проверки существования пользователя
            string query = "SELECT COUNT(*) FROM [register] WHERE login_user = @LoginUser AND password_user = @LoginPassword";

            // Создание команды SQL с параметрами
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LoginUser", loginUser);
            command.Parameters.AddWithValue("@LoginPassword", loginPassword);

            try
            {
                // Открытие подключения
                dataBase.openConnection();

                // Выполнение запроса и получение результата
                int userCount = (int)command.ExecuteScalar();

                // Проверка результата и разрешение входа, если пользователь существует и введенный пароль верен
                if (userCount > 0)
                {
                   
                    // Скрыть текущую форму
                    this.Hide();

                    // Создать и показать главную форму (или другую форму, куда нужно перейти после входа)
                    MainForm mainForm = new MainForm();
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show("Invalid username or password!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Закрытие подключения
                dataBase.closeConnection();
            }
        }


        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            pn_reg.Visible = false;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            pn_reg.Visible = true;
        }

        private void RegButton_Click(object sender, EventArgs e)
        {
            var loginUser = guna2TextBox3.Text;
            var loginPassword = guna2TextBox4.Text;
            var confirmPassword = guna2TextBox5.Text;

            if (loginPassword != confirmPassword)
            {
                MessageBox.Show("Passwords do not match!");
                return; // Прекратить выполнение метода
            }

            // Создание экземпляра класса DataBase
            DataBase dataBase = new DataBase();

            // Получение объекта подключения к базе данных
            SqlConnection connection = dataBase.getConnection();

            // SQL-запрос для вставки нового пользователя
            string query = "INSERT INTO [register] (login_user, password_user) VALUES (@LoginUser, @LoginPassword)";

            // Создание команды SQL с параметрами
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LoginUser", loginUser);
            command.Parameters.AddWithValue("@LoginPassword", loginPassword);

            try
            {
                // Открытие подключения
                dataBase.openConnection();

                // Выполнение запроса на вставку нового пользователя
                int rowsAffected = command.ExecuteNonQuery();

                // Проверка количества затронутых строк
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Registration successful!");
                    this.Hide();

                    // Создать и показать главную форму (или другую форму, куда нужно перейти после входа)
                    MainForm mainForm = new MainForm();
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show("Registration failed!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Закрытие подключенияvbvgvbvbvb
                dataBase.closeConnection();
            }
        }


    }
}

