using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TiaFileViewer
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string AppTitle = "TIA Selection Tool - Datei viewer";

        BindingProperties bindingProperties = new BindingProperties();
        TiaFile tiaFile;

        string MainWindowTitle
        {
            get { return bindingProperties.MainWindowTitle; }
            set { bindingProperties.MainWindowTitle = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
            bindingProperties = (BindingProperties)base.DataContext;
            MainWindowTitle = AppTitle;
        }


        private void FileOpenButton_Click(object sender, RoutedEventArgs e)
        {
            string fileName = ChoseTiaFile();

            // fileName is empty if user cancelled dialog to open a file
            if (fileName == string.Empty)
                return;

            // Analyze file and add buttons at the top of the window
            SetMainWindowTitle(fileName);
            if (AnalyzeTiaFile(fileName))
                AddButtons();
            else
            {
                MessageBox.Show("Error occured on reading File " + fileName + "\n\n" + 
                    "Please try again or choose a valid file.");
                MainWindowTitle = AppTitle;
                ClearItems();
                ClearButtons();
            }
        }

        // Opens the file open dialog and returns the name of the selected file
        private string ChoseTiaFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "TIA-Dateien (*.tia)|*.tia";
            if ((bool)dialog.ShowDialog())
            {
                ClearItems();
                return dialog.FileName;
            }
            else
                return String.Empty;
        }
        
        private void SetMainWindowTitle(string fileName)
        {
            MainWindowTitle = AppTitle + " - \"" + fileName + "\"";
        }

        // Analyzes the tia file. Returns if an error occured on reading the file
        private bool AnalyzeTiaFile(string fileName)
        {
            tiaFile = new TiaFile(fileName);
            bool readingSuccessful = tiaFile.Analyze();
            return readingSuccessful;
        }

        // Adds buttons to the top of the window according to the node types of the selected tia file
        private void AddButtons()
        {
            ClearButtons();
            for (int i = 0; i < tiaFile.NodeTypes.Count; i++)
            {
                string buttonText = tiaFile.NodeTypes[i] + " (" + tiaFile.NodeTypesCount[i] + ")";
                CreateButton(buttonText, i);
            }
        }

        private void ClearButtons()
        {
            NodesPanel.Children.Clear();
        }

        // Creates a button and adds it to the panel
        private void CreateButton(string buttonText, int index)
        {
            Button button = new Button();
            button.Height = 50;
            button.Width = 130;
            button.Content = buttonText;
            button.Name = "Button" + index.ToString("00");
            button.Click += Button_Click;
            button.HorizontalAlignment = HorizontalAlignment.Left;
            button.Margin = new Thickness(10, 0, 0, 0);
            NodesPanel.Children.Add(button);
        }

        // Method that is called when one of the Created buttons is clicked
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string senderName = ((Button)sender).Name;
            int buttonIndex = Convert.ToInt32(senderName.Substring(senderName.Length - 2, 2));

            ClearItems();
            AddItems(buttonIndex);
        }

        // Adds all the items of the clicked node type to the ListView control
        private void AddItems(int buttonIndex)
        {
            for (int i = 0; i < tiaFile.NamesOrId[buttonIndex].Count; i++)
            {
                string nameOrId = tiaFile.NamesOrId[buttonIndex][i];
                string propertiesCount = tiaFile.PropertiesCount[buttonIndex][i].ToString();
                CreateItem(nameOrId, propertiesCount);
            }
        }

        // Creates an item and adds it to the ListView control
        private void CreateItem(string nameOrId, string propertiesCount)
        {
            ListViewItem item = new ListViewItem();
            item.Content = GetItemText(nameOrId, propertiesCount);
            item.FontFamily = new FontFamily("Courier New");    // Microsoft supplied monospaced font
            ElementsListView.Items.Add(item);
        }

        private string GetItemText(string nameOrId, string propertiesCount)
        {
            return string.Format("{0,-35}| Eigenschaften: {1}", nameOrId, propertiesCount);
        }

        private void ClearItems()
        {
            ElementsListView.Items.Clear();
        }
    }
}
