﻿using System.Windows.Controls;
using sbwpf.Themer;


namespace sbwpf.Test
{
    /// <summary>
    /// Interaction logic for uc_Themer.xaml
    /// </summary>
    public partial class uc_Themer : UserControl
    {
        public uc_Themer()
        {
            InitializeComponent();

            cb_Themes.DisplayMemberPath = "Name";
            cb_Themes.ItemsSource = ThemeManager.Themes;
            cb_Themes.SelectedItem = ThemeManager.ActiveTheme;

            listview.DataContext = SampleDataset.Samples;
            listbox.DataContext = SampleDataset.Samples;

            combobox.DisplayMemberPath = "StringZero";
            combobox.DataContext = SampleDataset.Samples;
        }

        private void cb_Themes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb_Themes.SelectedItem is Theme theme)
            {
                ThemeManager.ActiveTheme = theme;
            }
        }
    }
}
