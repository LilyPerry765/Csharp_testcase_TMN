﻿#pragma checksum "..\..\..\..\UI\Windows\PowerRegionWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "96B97F0B1126AF9B2C80D6377353AB90"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
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
    /// PowerRegionWindow
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class PowerRegionWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\..\UI\Windows\PowerRegionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grid;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\..\UI\Windows\PowerRegionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image regionImage;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\UI\Windows\PowerRegionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas canvas;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\UI\Windows\PowerRegionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TMN.UserControls.BlinkingLed connectLed;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\UI\Windows\PowerRegionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TMN.UserControls.SoundButton muteAllSoundButton;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\UI\Windows\PowerRegionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock refreshStatusTextblock;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\UI\Windows\PowerRegionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem refreshMenuItem;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\..\UI\Windows\PowerRegionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem reArrangeNewCenters;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\..\UI\Windows\PowerRegionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem lockMenuItem;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\..\UI\Windows\PowerRegionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem muteMenuItem;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TMN;component/ui/windows/powerregionwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UI\Windows\PowerRegionWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.grid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.regionImage = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.canvas = ((System.Windows.Controls.Canvas)(target));
            return;
            case 4:
            this.connectLed = ((TMN.UserControls.BlinkingLed)(target));
            return;
            case 5:
            this.muteAllSoundButton = ((TMN.UserControls.SoundButton)(target));
            return;
            case 6:
            this.refreshStatusTextblock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.refreshMenuItem = ((System.Windows.Controls.MenuItem)(target));
            
            #line 54 "..\..\..\..\UI\Windows\PowerRegionWindow.xaml"
            this.refreshMenuItem.Click += new System.Windows.RoutedEventHandler(this.refreshMenuItem_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.reArrangeNewCenters = ((System.Windows.Controls.MenuItem)(target));
            
            #line 57 "..\..\..\..\UI\Windows\PowerRegionWindow.xaml"
            this.reArrangeNewCenters.Click += new System.Windows.RoutedEventHandler(this.reArrangeNewCenters_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.lockMenuItem = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 10:
            this.muteMenuItem = ((System.Windows.Controls.MenuItem)(target));
            
            #line 65 "..\..\..\..\UI\Windows\PowerRegionWindow.xaml"
            this.muteMenuItem.Click += new System.Windows.RoutedEventHandler(this.muteMenuItem_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

