// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System.Windows;

// ReSharper disable once CheckNamespace

namespace SniffCore.Localizer
{
    /// <summary>
    ///     Provides ways to load translations from the application resources.
    /// </summary>
    /// <example>
    ///     <code lang="XAML">
    /// <![CDATA[
    /// <ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    ///                     xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    ///                     xmlns:system="clr-namespace:System;assembly=System.Runtime"
    ///                     xmlns:translations="clr-namespace:DocSniff.Common.Translations">
    /// 
    ///     <system:String x:Key="Something">something [VERSION]</system:String>
    /// 
    /// </ResourceDictionary>
    /// 
    /// <Application xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    ///              xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    /// 
    ///     <Application.Resources>
    ///         <ResourceDictionary>
    ///             <ResourceDictionary.MergedDictionaries>
    ///                 <ResourceDictionary Source="/MyApplication;component/Translations.xaml" />
    ///             </ResourceDictionary.MergedDictionaries>
    ///         </ResourceDictionary>
    ///     </Application.Resources>
    /// 
    /// </Application>
    /// ]]>
    /// </code>
    ///     <code lang="csharp">
    /// <![CDATA[
    /// Translator.GetString("Something", "[VERSION]", version.ToString(3))
    /// ]]>
    /// </code>
    /// </example>
    public static class Translator
    {
        /// <summary>
        ///     Loads the translation by its string key.
        /// </summary>
        /// <param name="key">The key of the translation.</param>
        /// <returns>The translations if available; otherwise the key.</returns>
        public static string GetString(string key)
        {
            var application = Application.Current;
            if (application == null)
                return key;

            if (application.Resources.Contains(key))
                return application.Resources[key] as string;

            return key;
        }

        /// <summary>
        ///     Loads the translation by its string key.
        /// </summary>
        /// <param name="key">The key of the translation.</param>
        /// <returns>The translations if available; otherwise the key.</returns>
        public static string GetString(ComponentResourceKey key)
        {
            var application = Application.Current;
            if (application == null)
                return key.ToString();

            if (application.Resources.Contains(key))
                return application.Resources[key] as string;

            return key.ToString();
        }

        /// <summary>
        ///     Loads the translation by its string key and applies formatting on it.
        /// </summary>
        /// <param name="key">The key of the translation.</param>
        /// <param name="formattings">The list of pairs 'replace' - 'with'.</param>
        /// <returns>The translations if available; otherwise the key.</returns>
        public static string GetString(string key, params string[] formattings)
        {
            var translation = GetString(key);
            return Format(translation, formattings);
        }

        /// <summary>
        ///     Loads the translation by its string key and applies formatting on it.
        /// </summary>
        /// <param name="key">The key of the translation.</param>
        /// <param name="formattings">The list of pairs 'replace' - 'with'.</param>
        /// <returns>The translations if available; otherwise the key.</returns>
        public static string GetString(ComponentResourceKey key, params string[] formattings)
        {
            var translation = GetString(key);
            return Format(translation, formattings);
        }

        /// <summary>
        ///     Applies formatting on the translation.
        /// </summary>
        /// <param name="translation">The translation.</param>
        /// <param name="formattings">The list of pairs 'replace' - 'with'.</param>
        /// <returns>The translation with the formattings.</returns>
        public static string Format(string translation, params string[] formattings)
        {
            for (int i = 0, j = 1; j < formattings.Length; i += 2, j += 2)
                translation = translation.Replace(formattings[i], formattings[j]);
            return translation;
        }
    }
}