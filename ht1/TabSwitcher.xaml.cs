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

namespace ht1
{
    /// <summary>
    /// Логика взаимодействия для TabSwitcherControl.xaml
    /// </summary>
    public partial class TabSwitcherControl : UserControl
    {
        public event RoutedEventHandler BtnNextClick;
        public event RoutedEventHandler BtnPreviousClick;

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            BtnNextClick?.Invoke(sender, e);
        }

        private void BtnPrevious_Click(object sender, RoutedEventArgs e)
        {
            BtnPreviousClick?.Invoke(sender, e);
        }

        public TabSwitcherControl()
        {
            InitializeComponent();
        }

        #region properties
        private bool bHideBtnPrevious = false;  // поле, соответствующее тому, будет ли скрыта кнопка «Предыдущий»
        private bool bHideBtnNext = false;      // поле, соответствующее тому, будет ли скрыта кнопка «Следующий»

        /// <summary>
        /// Свойство, соответствующее тому, будет ли скрыта кнопка «Предыдущий».
        /// Чтобы Свойство отразилось на PropertiesGrid у нашего контрола, его нужно
        ///представить именно свойством, а не полем
        /// </summary>
        public bool IsHideBtnPrevious
        {
            get { return bHideBtnPrevious; }
            set
            {
                bHideBtnPrevious = value;
                SetButtons(); // метод, который отвечает на отрисовку кнопок
            }
        } // IsHideBtnPrevious

        public bool IsHideBtnNext
        {
            get { return bHideBtnNext; }
            set
            {
                bHideBtnNext = value;
                SetButtons(); // метод, который отвечает за отрисовку кнопок
            }
        } // IsHideBtnNext

        private void BtnNextTrue_BtnPreviousFalse()
        {
            btnNext.Visibility = Visibility.Hidden;
            btnPrevious.Visibility = Visibility.Visible;
            col2.Width = new GridLength(0);
            col1.Width = new GridLength(1, GridUnitType.Star);
            btnPrevious.HorizontalAlignment = HorizontalAlignment.Stretch;
        }

        private void BtnPreviousTrue_BtnNextFalse()
        {
            btnPrevious.Visibility = Visibility.Hidden;
            btnNext.Visibility = Visibility.Visible;
            col1.Width = new GridLength(0);
            col2.Width = new GridLength(1, GridUnitType.Star);
            btnNext.HorizontalAlignment = HorizontalAlignment.Stretch;
        }
        private void BtnPreviousFalse_BtnNextFalse()
        {
            btnNext.Visibility = Visibility.Visible;
            btnPrevious.Visibility = Visibility.Visible;
            col1.Width = new GridLength(1, GridUnitType.Star);
            col2.Width = new GridLength(1, GridUnitType.Star);
        }
        private void BtnPreviousTrue_BtnNextTrue()
        {
            btnPrevious.Visibility = Visibility.Hidden;
            btnNext.Visibility = Visibility.Hidden;
            col1.Width = new GridLength(1, GridUnitType.Star);
            col2.Width = new GridLength(1, GridUnitType.Star);
        }

        /// <summary>
        /// Метод, который отвечает за отрисовку кнопок.
        /// Есть три варианта: когда обе кнопки доступны; доступна одна и недоступна вторая; обе кнопки недоступны
        /// </summary>
        private void SetButtons()
        {
            if (bHideBtnPrevious && bHideBtnNext)
                BtnPreviousTrue_BtnNextTrue();
            else if (!bHideBtnNext && !bHideBtnPrevious)
                BtnPreviousFalse_BtnNextFalse();
            else if (bHideBtnNext && !bHideBtnPrevious)
                BtnNextTrue_BtnPreviousFalse();
            else if (!bHideBtnNext && bHideBtnPrevious)
                BtnPreviousTrue_BtnNextFalse();
        }
        #endregion

    }
}
