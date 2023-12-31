﻿#pragma checksum "..\..\..\..\..\UI\Views\Details\TasktView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "33F0E2DF80311BBFFFAA5512F0FAE2A2"
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
using TMN.Converters;
using TMN.UserControls;


namespace TMN.Views.Details {
    
    
    /// <summary>
    /// TaskView
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class TaskView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\..\..\..\UI\Views\Details\TasktView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TMN.UserControls.EntityComboBox cmbType;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\..\UI\Views\Details\TasktView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TMN.UserControls.EntityComboBox RouteComboBox;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\..\UI\Views\Details\TasktView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Enterprise.Wpf.NumericUpDown ChannelCountNumericUpDown;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\..\UI\Views\Details\TasktView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox chkIsDone;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\..\..\UI\Views\Details\TasktView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Enterprise.Wpf.PersianDateBox txtFinishDate;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\..\..\UI\Views\Details\TasktView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtComment;
        
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
            System.Uri resourceLocater = new System.Uri("/TMN;component/ui/views/details/tasktview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\UI\Views\Details\TasktView.xaml"
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
            this.cmbType = ((TMN.UserControls.EntityComboBox)(target));
            return;
            case 2:
            this.RouteComboBox = ((TMN.UserControls.EntityComboBox)(target));
            return;
            case 3:
            this.ChannelCountNumericUpDown = ((Enterprise.Wpf.NumericUpDown)(target));
            return;
            case 4:
            this.chkIsDone = ((System.Windows.Controls.CheckBox)(target));
            
            #line 48 "..\..\..\..\..\UI\Views\Details\TasktView.xaml"
            this.chkIsDone.Checked += new System.Windows.RoutedEventHandler(this.chkIsDone_Checked);
            
            #line default
            #line hidden
            
            #line 49 "..\..\..\..\..\UI\Views\Details\TasktView.xaml"
            this.chkIsDone.Unchecked += new System.Windows.RoutedEventHandler(this.chkIsDone_Unchecked);
            
            #line default
            #line hidden
            return;
            case 5:
            this.txtFinishDate = ((Enterprise.Wpf.PersianDateBox)(target));
            
            #line 52 "..\..\..\..\..\UI\Views\Details\TasktView.xaml"
            this.txtFinishDate.IsCheckedChanged += new System.Windows.RoutedEventHandler(this.txtFinishDate_IsCheckedChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.txtComment = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

