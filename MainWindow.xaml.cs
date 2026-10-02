using System;
using System.Collections.Generic;
using System.Windows;

namespace StudentManagement
{
    public partial class MainWindow : Window
    {
        // База данных в памяти
        public List<Student> Spisok = new List<Student>();

        // Экземпляр класса поиска (Разработчик 2)
        private StudentSearcher _searcher = new StudentSearcher();

        public MainWindow()
        {
            InitializeComponent();
            BirthDatePicker.SelectedDate = DateTime.Now;
            GridStude.ItemsSource = Spisok;
        }

        // ==========================================
        // КОД РАЗРАБОТЧИКА 1 (Добавление и удаление)
        // ==========================================

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            double.TryParse(TxtMark.Text, out double gpa);
            Student s = new Student
            {
                FullName = TxtFio.Text,
                Group = TxtGroup.Text,
                BirthDate = BirthDatePicker.SelectedDate ?? DateTime.Now,
                Phone = TxtPhone.Text,
                Gpa = gpa
            };

            Spisok.Add(s);
            UpdateGrid(Spisok); // Обновляем таблицу
            TxtFio.Clear();
            TxtGroup.Clear();
            TxtPhone.Clear();
            TxtMark.Clear();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // БЕЗОПАСНОЕ УДАЛЕНИЕ: удаляем по выделенному объекту, а не по индексу строки!
            // Если удалять по SelectedIndex во время работы поиска, удалится не тот студент из Spisok.
            if (GridStude.SelectedItem is Student selectedStudent)
            {
                Spisok.Remove(selectedStudent);

                // После удаления перезапускаем поиск, чтобы на экране остался актуальный отфильтрованный список
                Search_Click(null, null);
            }
        }

        // ==========================================
        // КОД РАЗРАБОТЧИКА 2 (Связь интерфейса с классом поиска)
        // ==========================================

        // Кнопка НАЙТИ
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            // 1. Вызываем метод фильтрации из нашего отдельного класса
            List<Student> filteredResult = _searcher.Filter(Spisok, TxtSearch.Text, TxtFilterGroup.Text);

            // 2. Обработка ситуации "Ничего не найдено"
            // Показываем ошибку только если в исходной базе студенты есть, а в результате фильтра — 0
            if (filteredResult.Count == 0 && Spisok.Count > 0)
            {
                LblNotFoundError.Visibility = Visibility.Visible;
            }
            else
            {
                LblNotFoundError.Visibility = Visibility.Collapsed;
            }

            // 3. Отображаем результат
            UpdateGrid(filteredResult);
        }

        // Кнопка СБРОСИТЬ
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            TxtSearch.Clear();
            TxtFilterGroup.Clear();
            LblNotFoundError.Visibility = Visibility.Collapsed;

            UpdateGrid(Spisok); // Возвращаем полный список
        }

        // Вспомогательный метод обновления DataGrid
        private void UpdateGrid(List<Student> list)
        {
            GridStude.ItemsSource = null;
            GridStude.ItemsSource = list;
        }
    }
}
