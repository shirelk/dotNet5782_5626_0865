﻿using BO;
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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelListWindowe.xaml
    /// </summary>
    public partial class ParcelListWindowe : Window
    {
        BlApi.IBL myBL;
        public ParcelListWindowe(BlApi.IBL bl)
        {
            myBL = bl;
            InitializeComponent();
            this.ParcelListView.ItemsSource = myBL.ShowParcelList();
        }

        private void ParcelListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ParcelToList? parTL = ParcelListView.SelectedItem as ParcelToList;
            if (parTL != null)
            {
                new ParcelInActionView(parTL, myBL, this).Show();
            }
        }
        /// <summary>
        /// A button to add a new parcel
        /// </summary>
        /// <param name="sender">Button type</param>
        /// <param name="e"></param>
        private void btnAddParcel_Click(object sender, RoutedEventArgs e)
        {
            new ParcelInActionView( myBL).Show();
        }
        /// <summary>
        /// A button that refresh the list of parcels order by the reciver
        /// </summary>
        /// <param name="sender">Button type</param>
        /// <param name="e"></param>
        private void RefreshBaseParcelTargetButton_Click(object sender, RoutedEventArgs e)
        {
            ParcelListView.ItemsSource = myBL.ShowParcelList().OrderBy(p => p.ReciverName);
            ParcelListView.Items.Refresh();
        }
    }
}