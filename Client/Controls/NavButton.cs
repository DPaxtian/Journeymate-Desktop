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

namespace Client.Controls
{
    public class NavButton : ListBoxItem
    {
        static NavButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavButton), new FrameworkPropertyMetadata(typeof(NavButton)));
        }

        public Uri PagePath
        {
            get { return (Uri)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }
        public static readonly DependencyProperty MyPropertyProperty = DependencyProperty.Register("MyProperty", typeof(Uri), typeof(NavButton), new PropertyMetadata(null));


        public string ImagePath
        {
            get { return (string)GetValue(ButtonIconProperty); }
            set { SetValue(ButtonIconProperty, value); }
        }
        public static readonly DependencyProperty ButtonIconProperty = DependencyProperty.Register("ButtonIcon", typeof(string), typeof(NavButton), new PropertyMetadata(null));



        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }

        public static readonly DependencyProperty ButtonTextProperty = DependencyProperty.Register("ButtonText", typeof(string), typeof(NavButton), new PropertyMetadata(null));


    }
}
