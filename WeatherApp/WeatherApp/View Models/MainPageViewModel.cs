using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using WeatherApp.Views;

namespace WeatherApp.View_Models
{

    public class MainPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<MainPageFlyoutMenuItem> MenuItems { get; set; }

        public MainPageViewModel()
        {
            MenuItems = new ObservableCollection<MainPageFlyoutMenuItem>(new[]
            {
                new MainPageFlyoutMenuItem { Id = 0, Title = "Chỉnh sửa vị trí", Icon = "marker_20px.png", TargetType = typeof(ChangeLocationPage) },
                new MainPageFlyoutMenuItem { Id = 1, Title = "Cài đặt đơn vị", Icon = "temperature_sensitive_20px.png", TargetType = typeof(UnitSettingPage) },
                new MainPageFlyoutMenuItem { Id = 2, Title = "Màu nền", Icon = "paint_palette_20px.png", TargetType = typeof(SelectBackgroundColorPage) },
                new MainPageFlyoutMenuItem { Id = 3, Title = "Lịch sử thời tiết", Icon = "paint_palette_20px.png", TargetType = typeof(HistoryWeatherPage) }
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