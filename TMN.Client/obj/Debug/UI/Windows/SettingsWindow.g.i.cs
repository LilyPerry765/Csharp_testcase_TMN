﻿#pragma checksum "..\..\..\..\UI\Windows\SettingsWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "DCD72BC9B4C40DE7DFA7E14D8A6E7C02"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using TMN;
using TMN.Assets;
using TMN.Converters;
using TMN.UserControls;


namespace TMN.UI.Windows {
    
    
    /// <summary>
    /// SettingsWindow
    /// </summary>
    public partial class SettingsWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 18 "..\..\..\..\UI\Windows\SettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ControlPanel;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\..\UI\Windows\SettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TMN.UserControls.ImageButton btnOK;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\UI\Windows\SettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TMN.UserControls.ImageButton btnCancel;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\UI\Windows\SettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox centersCombo;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\..\UI\Windows\SettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox VoiceAlertIntervalTextbox;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\..\..\UI\Windows\SettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ColorList;
        
        #line default
        #line hidden
        
        
        #line 128 "..\..\..\..\UI\Windows\SettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox DCSoundTextbox;
        
        #line default
        #line hidden
        
        
        #line 129 "..\..\..\..\UI\Windows\SettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button openDCFileButton;
        
        #line default
        #line hidden
        
        
        #line 133 "..\..\..\..\UI\Windows\SettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox criticalSoundTextbox;
        
        #line default
        #line hidden
        
        
        #line 134 "..\..\..\..\UI\Windows\SettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button openCriticalFileButton;
        
        #line default
        #line hidden
        
        
        #line 138 "..\..\..\..\UI\Windows\SettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox majorSoundTextbox;
        
        #line default
        #line hidden
        
        
        #line 139 "..\..\..\..\UI\Windows\SettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button openMajorFileButton;
        
        #line default
        #line hidden
        
        
        #line 143 "..\..\..\..\UI\Windows\SettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox minorSoundTextbox;
        
        #line default
        #line hidden
        
        
        #line 144 "..\..\..\..\UI\Windows\SettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button openMinorFileButton;
        
        #line default
        #line hidden
        
        
        #line 148 "..\..\..\..\UI\Windows\SettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox powerSoundTextbox;
        
        #line default
        #line hidden
        
        
        #line 149 "..\..\..\..\UI\Windows\SettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button openPowerFileButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TMN;component/ui/windows/settingswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UI\Windows\SettingsWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\..\..\UI\Windows\SettingsWindow.xaml"
            ((TMN.UI.Windows.SettingsWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 15 "..\..\..\..\UI\Windows\SettingsWindow.xaml"
            ((TMN.UI.Windows.SettingsWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ControlPanel = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            this.btnOK = ((TMN.UserControls.ImageButton)(target));
            return;
            case 4:
            this.btnCancel = ((TMN.UserControls.ImageButton)(target));
            return;
            case 5:
            this.centersCombo = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.VoiceAlertIntervalTextbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            
            #line 90 "..\..\..\..\UI\Windows\SettingsWindow.xaml"
            ((System.Windows.Controls.TabItem)(target)).Loaded += new System.Windows.RoutedEventHandler(this.TabColors_Loaded);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ColorList = ((System.Windows.Controls.ListView)(target));
            return;
            case 10:
            
            #line 123 "..\..\..\..\UI\Windows\SettingsWindow.xaml"
            ((System.Windows.Controls.TabItem)(target)).Loaded += new System.Windows.RoutedEventHandler(this.TabAlarms_Loaded);
            
            #line default
            #line hidden
            return;
            case 11:
            this.DCSoundTextbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 12:
            this.openDCFileButton = ((System.Windows.Controls.Button)(target));
            
            #line 129 "..\..\..\..\UI\Windows\SettingsWindow.xaml"
            this.openDCFileButton.Click += new System.Windows.RoutedEventHandler(this.openFile_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.criticalSoundTextbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 14:
            this.openCriticalFileButton = ((System.Windows.Controls.Button)(target));
            
            #line 134 "..\..\..\..\UI\Windows\SettingsWindow.xaml"
            this.openCriticalFileButton.Click += new System.Windows.RoutedEventHandler(this.openFile_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            this.majorSoundTextbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 16:
            this.openMajorFileButton = ((System.Windows.Controls.Button)(target));
            
            #line 139 "..\..\..\..\UI\Windows\SettingsWindow.xaml"
            this.openMajorFileButton.Click += new System.Windows.RoutedEventHandler(this.openFile_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            this.minorSoundTextbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 18:
            this.openMinorFileButton = ((System.Windows.Controls.Button)(target));
            
            #line 144 "..\..\..\..\UI\Windows\SettingsWindow.xaml"
            this.openMinorFileButton.Click += new System.Windows.RoutedEventHandler(this.openFile_Click);
            
            #line default
            #line hidden
            return;
            case 19:
            this.powerSoundTextbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 20:
            this.openPowerFileButton = ((System.Windows.Controls.Button)(target));
            
            #line 149 "..\..\..\..\UI\Windows\SettingsWindow.xaml"
            this.openPowerFileButton.Click += new System.Windows.RoutedEventHandler(this.openFile_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 9:
            
            #line 108 "..\..\..\..\UI\Windows\SettingsWindow.xaml"
            ((System.Windows.Controls.Border)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.Border_MouseUp);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

