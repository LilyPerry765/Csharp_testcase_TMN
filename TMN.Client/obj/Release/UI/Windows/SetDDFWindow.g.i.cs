﻿#pragma checksum "..\..\..\..\UI\Windows\SetDDFWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2BC828CA4AF15218DCE51FB6FC6CD5B7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Enterprise.Wpf;
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
    /// SetDDFWindow
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class SetDDFWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\..\UI\Windows\SetDDFWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ControlPanel;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\UI\Windows\SetDDFWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TMN.UserControls.ImageButton btnOK;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\UI\Windows\SetDDFWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TMN.UserControls.ImageButton btnCancel;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\..\UI\Windows\SetDDFWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Enterprise.Wpf.NumericUpDown BayUpDown;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\UI\Windows\SetDDFWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Enterprise.Wpf.NumericUpDown PositionUpDown;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\..\UI\Windows\SetDDFWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Enterprise.Wpf.NumericUpDown NumberUpDown;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\..\UI\Windows\SetDDFWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textBlock1;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\..\..\UI\Windows\SetDDFWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textBlock2;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\..\UI\Windows\SetDDFWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textBlock3;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\..\UI\Windows\SetDDFWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox DescriptionTextBox;
        
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
            System.Uri resourceLocater = new System.Uri("/TMN;component/ui/windows/setddfwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UI\Windows\SetDDFWindow.xaml"
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
            this.ControlPanel = ((System.Windows.Controls.Border)(target));
            return;
            case 2:
            this.btnOK = ((TMN.UserControls.ImageButton)(target));
            return;
            case 3:
            this.btnCancel = ((TMN.UserControls.ImageButton)(target));
            return;
            case 4:
            this.BayUpDown = ((Enterprise.Wpf.NumericUpDown)(target));
            
            #line 52 "..\..\..\..\UI\Windows\SetDDFWindow.xaml"
            this.BayUpDown.ValueChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.UpDown_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.PositionUpDown = ((Enterprise.Wpf.NumericUpDown)(target));
            
            #line 56 "..\..\..\..\UI\Windows\SetDDFWindow.xaml"
            this.PositionUpDown.ValueChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.UpDown_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.NumberUpDown = ((Enterprise.Wpf.NumericUpDown)(target));
            
            #line 64 "..\..\..\..\UI\Windows\SetDDFWindow.xaml"
            this.NumberUpDown.ValueChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.UpDown_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.textBlock1 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.textBlock2 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.textBlock3 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            this.DescriptionTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

