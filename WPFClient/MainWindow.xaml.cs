using System;
using System.Windows;
using ClassGeneration.Interfaces;
using DataAccess;
using Models;
using Repositories;

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IConnectionSetter _connectionSetter;
        private readonly IRepository<Database> _databaseRepository;
        private readonly Func<IBuilder<Table, string>> _classStringBuilder;
        private readonly IBuilder<Table, Table> _tableBuilder;

        public MainWindow(IConnectionSetter connectionSetter,
            IRepository<Database> databaseRepository,
            Func<IBuilder<Table, string>> classStringBuilder,
            IBuilder<Table, Table> tableBuilder)
        {
            _databaseRepository = databaseRepository;
            _connectionSetter = connectionSetter;
            _classStringBuilder = classStringBuilder;
            _tableBuilder = tableBuilder;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _connectionSetter.SetConnection();

            DataContext = _databaseRepository.All();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            SetText();
        }

        private void TextBlock_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SetText();
        }

        private void SetText()
        {
            var table = treeView.SelectedItem as Table;

            richTextBox.Clear();

            var tableWithColumns = _tableBuilder.Build(table);

            var text = _classStringBuilder().Build(tableWithColumns);

            richTextBox.Text = text;
        }
    }
}