﻿#pragma checksum "..\..\..\..\UI\Windows\AlarmPanelWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "9AE5BEF2D0780AE33C97F71E9FED2DF5"
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
using TMN.UserControls;


namespace TMN.UI.Windows {
    
    
    /// <summary>
    /// AlarmPanelWindow
    /// </summary>
    public partial class AlarmPanelWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 72 "..\..\..\..\UI\Windows\AlarmPanelWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RowDefinition headerRow;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\..\UI\Windows\AlarmPanelWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RowDefinition sensorRow;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\..\UI\Windows\AlarmPanelWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RowDefinition alarmPanelRow;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\..\UI\Windows\AlarmPanelWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid HeaderGrid;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\..\UI\Windows\AlarmPanelWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label CenterNameLabel;
        
        #line default
        #line hidden
        
        
        #line 112 "..\..\..\..\UI\Windows\AlarmPanelWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label DateTimeLabel;
        
        #line default
        #line hidden
        
        
        #line 121 "..\..\..\..\UI\Windows\AlarmPanelWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label PersianDateTimeLabel;
        
        #line default
        #line hidden
        
        
        #line 131 "..\..\..\..\UI\Windows\AlarmPanelWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TMN.UserControls.BlinkingLed alarmServiceLed;
        
        #line default
        #line hidden
        
        
        #line 140 "..\..\..\..\UI\Windows\AlarmPanelWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TMN.UserControls.BlinkingLed PoweralarmLed;
        
        #line default
        #line hidden
        
        
        #line 165 "..\..\..\..\UI\Windows\AlarmPanelWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TMN.UserControls.SoundButton masterSound;
        
        #line default
        #line hidden
        
        
        #line 183 "..\..\..\..\UI\Windows\AlarmPanelWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button remoteButton;
        
        #line default
        #line hidden
        
        
        #line 194 "..\..\..\..\UI\Windows\AlarmPanelWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.UniformGrid sensorsPanel;
        
        #line default
        #line hidden
        
        
        #line 208 "..\..\..\..\UI\Windows\AlarmPanelWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid AlarmPanelGrid;
        
        #line default
        #line hidden
        
        
        #line 241 "..\..\..\..\UI\Windows\AlarmPanelWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TMN.UserControls.BlinkingLed criticalLed;
        
        #line default
        #line hidden
        
        
        #line 245 "..\..\..\..\UI\Windows\AlarmPanelWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TMN.UserControls.SoundButton critSound;
        
        #line default
        #line hidden
        
        
        #line 257 "..\..\..\..\UI\Windows\AlarmPanelWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TMN.UserControls.BlinkingLed majorLed;
        
        #line default
        #line hidden
        
        
        #line 260 "..\..\..\..\UI\Windows\AlarmPanelWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TMN.UserControls.SoundButton majorSound;
        
        #line default
        #line hidden
        
        
        #line 272 "..\..\..\..\UI\Windows\AlarmPanelWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TMN.UserControls.BlinkingLed minorLed;
        
        #line default
        #line hidden
        
        
        #line 275 "..\..\..\..\UI\Windows\AlarmPanelWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TMN.UserControls.SoundButton minorSound;
        
        #line default
        #line hidden
        
        
        #line 279 "..\..\..\..\UI\Windows\AlarmPanelWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView newCriticalAlarmsList;
        
        #line default
        #line hidden
        
        
        #line 282 "..\..\..\..\UI\Windows\AlarmPanelWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView newMajorAlarmsList;
        
        #line default
        #line hidden
        
        
        #line 285 "..\..\..\..\UI\Windows\AlarmPanelWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView newMinorAlarmsList;
        
        #line default
        #line hidden
        
        
        #line 288 "..\..\..\..\UI\Windows\AlarmPanelWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TMN.UserControls.BlinkingLed infoLed;
        
        #line default
        #line hidden
        
        
        #line 289 "..\..\..\..\UI\Windows\AlarmPanelWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TMN.UserControls.BlinkingLed ppLed;
        
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
            System.Uri resourceLocater = new System.Uri("/TMN;component/ui/windows/alarmpanelwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UI\Windows\AlarmPanelWindow.xaml"
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
            this.headerRow = ((System.Windows.Controls.RowDefinition)(target));
            return;
            case 2:
            this.sensorRow = ((System.Windows.Controls.RowDefinition)(target));
            return;
            case 3:
            this.alarmPanelRow = ((System.Windows.Controls.RowDefinition)(target));
            return;
            case 4:
            this.HeaderGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.CenterNameLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.DateTimeLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.PersianDateTimeLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.alarmServiceLed = ((TMN.UserControls.BlinkingLed)(target));
            return;
            case 9:
            this.PoweralarmLed = ((TMN.UserControls.BlinkingLed)(target));
            return;
            case 10:
            this.masterSound = ((TMN.UserControls.SoundButton)(target));
            return;
            case 11:
            this.remoteButton = ((System.Windows.Controls.Button)(target));
            
            #line 184 "..\..\..\..\UI\Windows\AlarmPanelWindow.xaml"
            this.remoteButton.Click += new System.Windows.RoutedEventHandler(this.remoteButton_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.sensorsPanel = ((System.Windows.Controls.Primitives.UniformGrid)(target));
            return;
            case 13:
            this.AlarmPanelGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 14:
            this.criticalLed = ((TMN.UserControls.BlinkingLed)(target));
            return;
            case 15:
            this.critSound = ((TMN.UserControls.SoundButton)(target));
            return;
            case 16:
            this.majorLed = ((TMN.UserControls.BlinkingLed)(target));
            return;
            case 17:
            this.majorSound = ((TMN.UserControls.SoundButton)(target));
            return;
            case 18:
            this.minorLed = ((TMN.UserControls.BlinkingLed)(target));
            return;
            case 19:
            this.minorSound = ((TMN.UserControls.SoundButton)(target));
            return;
            case 20:
            this.newCriticalAlarmsList = ((System.Windows.Controls.ListView)(target));
            return;
            case 21:
            this.newMajorAlarmsList = ((System.Windows.Controls.ListView)(target));
            return;
            case 22:
            this.newMinorAlarmsList = ((System.Windows.Controls.ListView)(target));
            return;
            case 23:
            this.infoLed = ((TMN.UserControls.BlinkingLed)(target));
            return;
            case 24:
            this.ppLed = ((TMN.UserControls.BlinkingLed)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

