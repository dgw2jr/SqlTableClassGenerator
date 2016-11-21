using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
        private readonly IBuilder<Models.Table, string> _classStringBuilder;
        private readonly IBuilder<Models.Table, Models.Table> _tableBuilder;

        public MainWindow(IConnectionSetter connectionSetter,
            IRepository<Database> databaseRepository,
            IBuilder<Models.Table, string> classStringBuilder,
            IBuilder<Models.Table, Models.Table> tableBuilder)
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

        private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var s = sender as TextBlock;
            var parent = s.TemplatedParent as ContentPresenter;
            var table = parent.Content as Models.Table;
            MessageBox.Show(s.Text + table.DatabaseName);

            richTextBox.Document.Blocks.Clear();

            var tableWithColumns = _tableBuilder.Build(table);

            var text = _classStringBuilder.Build(tableWithColumns);

            richTextBox.AppendText(text);
        }
    }
}
