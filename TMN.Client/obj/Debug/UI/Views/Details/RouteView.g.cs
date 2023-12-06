﻿#pragma checksum "..\..\..\..\..\UI\Views\Details\RouteView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "DA17630C2BBFF5B112AA3566CBC4E028"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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
using TMN;
using TMN.Assets;
using TMN.Converters;
using TMN.UserControls;


namespace TMN.Views.Details {
    
    
    /// <summary>
    /// RoutesView
    /// </summary>
    public partial class RoutesView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 31 "..\..\..\..\..\UI\Views\Details\RouteView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbDestCenter;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\..\UI\Views\Details\RouteView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TMN.UserControls.EntityComboBox cmbInstruction;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\..\UI\Views\Details\RouteView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbProtocol;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\..\UI\Views\Details\RouteView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox OPMComboBox;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\..\..\UI\Views\Details\RouteView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtRouteName;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\..\..\UI\Views\Details\RouteView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtTGNO;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\..\..\UI\Views\Details\RouteView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox IsSignalingCheckBox;
        
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
            System.Uri resourceLocater = new System.Uri("/TMN;component/ui/views/details/routeview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\UI\Views\Details\RouteView.xaml"
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
            this.cmbDestCenter = ((System.Windows.Controls.ComboBox)(target));
            
            #line 37 "..\..\..\..\..\UI\Views\Details\RouteView.xaml"
            this.cmbDestCenter.DropDownClosed += new System.EventHandler(this.cmbDestCenter_DropDownClosed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.cmbInstruction = ((TMN.UserControls.EntityComboBox)(target));
            return;
            case 3:
            this.cmbProtocol = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.OPMComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.txtRouteName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.txtTGNO = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.IsSignalingCheckBox = ((System.Windows.Controls.CheckBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
