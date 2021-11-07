using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using WeatherApp.Views;

namespace WeatherApp.View_Models
{

    class MainPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<MainPageFlyoutMenuItem> MenuItems { get; set; }

        public MainPageViewModel()
        {
            MenuItems = new ObservableCollection<MainPageFlyoutMenuItem>(new[]
            {
                new MainPageFlyoutMenuItem { Id = 0, Title = "Thêm vị trí", Icon = "marker_16px.png", TargetType = typeof(ChangeLocationPage) },
                new MainPageFlyoutMenuItem { Id = 1, Title = "Cài đặt đơn vị", Icon = "marker_16px.png", TargetType = typeof(UnitSettingPage) },
                new MainPageFlyoutMenuItem { Id = 2, Title = "Màu nền", Icon = "marker_16px.png", TargetType = typeof(DetailByHour) }
            });
        }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
