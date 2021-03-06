﻿using System;
using System.Windows;
using System.Windows.Media;

namespace EarTrumpet.Services
{
    public class ThemeService
    {
        public static bool IsWindowTransparencyEnabled
        {
            get
            {
                return !SystemParameters.HighContrast && UserSystemPreferencesService.IsTransparencyEnabled;
            }
        }

        public static void UpdateThemeResources(ResourceDictionary dictionary)
        {
            dictionary["WindowBackground"] = new SolidColorBrush(GetWindowBackgroundColor());

            SetBrush(dictionary, "WindowForeground", "ImmersiveApplicationTextDarkTheme");
            ReplaceBrush(dictionary, "CottonSwabSliderThumb", "ImmersiveSystemAccent");
            ReplaceBrush(dictionary, "CottonSwabSliderTrackBackground", SystemParameters.HighContrast ? "ImmersiveSystemAccentLight1" : "ImmersiveControlDarkSliderTrackBackgroundRest");
            SetBrushWithOpacity(dictionary, "CottonSwabSliderTrackBackgroundHover", SystemParameters.HighContrast ? "ImmersiveSystemAccentLight1" : "ImmersiveDarkBaseMediumHigh", SystemParameters.HighContrast ? 1.0 : 0.25);
            SetBrush(dictionary, "CottonSwabSliderTrackBackgroundPressed", "ImmersiveControlDarkSliderTrackBackgroundRest");
            ReplaceBrush(dictionary, "CottonSwabSliderTrackFill", "ImmersiveSystemAccentLight1");
            SetBrush(dictionary, "CottonSwabSliderThumbHover", "ImmersiveControlDarkSliderThumbHover");
            SetBrush(dictionary, "CottonSwabSliderThumbPressed", "ImmersiveControlDarkSliderThumbHover");
        }

        private static Color GetWindowBackgroundColor()
        {
            string resource;
            if (SystemParameters.HighContrast)
            {
                resource = "ImmersiveApplicationBackground";
            }
            else if (UserSystemPreferencesService.UseAccentColor)
            {
                resource = ThemeService.IsWindowTransparencyEnabled ? "ImmersiveSystemAccentDark2" : "ImmersiveSystemAccentDark1";
            }
            else
            {
                resource = "ImmersiveDarkChromeMedium";
            }

            Color color = AccentColorService.GetColorByTypeName(resource);
            color.A = (byte)(ThemeService.IsWindowTransparencyEnabled ? 190 : 255);
            return color;
        }

        private static void SetBrushWithOpacity(ResourceDictionary dictionary, string name, string immersiveAccentName, double opacity)
        {
            Color color = AccentColorService.GetColorByTypeName(immersiveAccentName);
            color.A = (byte)(opacity * 255);
            ((SolidColorBrush)dictionary[name]).Color = color;
        }

        private static void SetBrush(ResourceDictionary dictionary, string name, string immersiveAccentName)
        {
            SetBrushWithOpacity(dictionary, name, immersiveAccentName, 1.0);
        }

        private static void ReplaceBrush(ResourceDictionary dictionary, string name, string immersiveAccentName)
        {
            dictionary[name] = new SolidColorBrush(AccentColorService.GetColorByTypeName(immersiveAccentName));
        }
    }
}
