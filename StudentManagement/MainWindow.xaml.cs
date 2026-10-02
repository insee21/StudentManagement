using System;
using System.Collections.Generic;
using System.Windows;

namespace StudentManagement
{
    public partial class MainWindow : Window
    {
        // Обычный список для хранения студентов
        public List<Student> Spisok = new List<Student>();

        public MainWindow()
        {
            InitializeComponent();
            BirthDatePicker.SelectedDate = DateTime.Now;
        }

        // Кнопка ДОБАВИТЬ
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            // Переводим средний балл в число (если пустой или буквы — будет 0)
            double.TryParse(TxtMark.Text, out double gpa);

            // Создаем студента и сразу заполняем из полей
            Student s = new Student
            {
                FullName = TxtFio.Text,
                Group = TxtGroup.Text,
                BirthDate = BirthDatePicker.SelectedDate ?? DateTime.Now,
                Phone = TxtPhone.Text,
                Gpa = gpa
            };

            Spisok.Add(s); // Добавили в список

            // Костыль, чтобы обновить таблицу на экране
            GridStude.ItemsSource = null;
            GridStude.ItemsSource = Spisok;

            // Просто очищаем текстовые поля
            TxtFio.Clear();
            TxtGroup.Clear();
            TxtPhone.Clear();
            TxtMark.Clear();
        }

        // Кнопка УДАЛИТЬ
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // Если в таблице что-то выбрано
            if (GridStude.SelectedIndex != -1)
            {
                Spisok.RemoveAt(GridStude.SelectedIndex); // Удаляем по номеру строки

                // Снова обновляем таблицу на экране
                GridStude.ItemsSource = null;
                GridStude.ItemsSource = Spisok;
            }
        }
    }
}
