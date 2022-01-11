using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Helper
{
    public interface ISettingService
    {
        void OpenSettings();
        bool IsGPSAvailable();
        void OpenPrivacySetting();
    }
}
