using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.RightsManagement;
using System.Threading.Tasks;
using System.Windows;
using Core;

namespace PenOrderBase
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Customer Customer { get; set; }
    }
}
