﻿#pragma checksum "..\..\..\..\UI\Windows\AlarmCircuitWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "798267AC7DAE5B7E6DD3C92CA8F9F626"
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
using TMN.Assets;
using TMN.UserControls;


namespace TMN.UI.Windows {
    
    
    /// <summary>
    /// AlarmCircuitWindow
    /// </summary>
    public partial class AlarmCircuitWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 71 "..\..\..\..\UI\Windows\AlarmCircuitWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RowDefinition headerRow;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\..\UI\Windows\AlarmCircuitWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RowDefinition sensorRow;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\..\UI\Windows\AlarmCircuitWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RowDefinition alarmPanelRow;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\..\UI\Windows\AlarmCircuitWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid HeaderGrid;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\..\UI\Windows\AlarmCircuitWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label CenterNameLabel;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\..\UI\Windows\AlarmCircuitWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label DateTimeLabel;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\..\..\UI\Windows\AlarmCircuitWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label PersianDateTimeLabel;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\..\..\UI\Windows\AlarmCircuitWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TMN.UserControls.BlinkingLed alarmServiceLed;
        
        #line default
        #line hidden
        
        
        #line 139 "..\..\..\..\UI\Windows\AlarmCircuitWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TMN.UserControls.BlinkingLed newAlarmLed;
        
        #line default
        #line hidden
        
        
        #line 148 "..\..\..\..\UI\Windows\AlarmCircuitWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TMN.UserControls.BlinkingLed refreshLed;
        
        #line default
        #line hidden
        
        
        #line 170 "..\..\..\..\UI\Windows\AlarmCircuitWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TMN.UserControls.SoundButton masterSound;
        
        #line default
        #line hidden
        
        
        #line 179 "..\..\..\..\UI\Windows\AlarmCircuitWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.UniformGrid sensorsPanel;
        
        #line default
        #line hidden
        
        
        #line 193 "..\..\..\..\UI\Windows\AlarmCircuitWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid AlarmPanelGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/TMN;component/ui/windows/alarmcircuitwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UI\Windows\AlarmCircuitWindow.xaml"
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
            this.newAlarmLed = ((TMN.UserControls.BlinkingLed)(target));
            return;
            case 10:
            this.refreshLed = ((TMN.UserControls.BlinkingLed)(target));
            return;
            case 11:
            this.masterSound = ((TMN.UserControls.SoundButton)(target));
            return;
            case 12:
            this.sensorsPanel = ((System.Windows.Controls.Primitives.UniformGrid)(target));
            return;
            case 13:
            this.AlarmPanelGrid = ((System.Windows.Controls.Grid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

