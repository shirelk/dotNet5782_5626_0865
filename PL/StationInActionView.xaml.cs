﻿using System;
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
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for StationInActionView.xaml
    /// </summary>
    public partial class StationInActionView : Window
    {
        public StationInActionView()
        {
            InitializeComponent();
        }
        public StationInActionView(StationToList station, BO.IBL bL, StationListWindow stationListWindow)
        {
            InitializeComponent();
        }
    }
}