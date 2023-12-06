﻿#pragma checksum "..\..\..\..\UI\Windows\SetCardWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "0536F153EC3BB7472C1D0FF78CD42C04"
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
using TMN.Assets;
using TMN.UserControls;


namespace TMN.UI.Windows {
    
    
    /// <summary>
    /// SetCardWindow
    /// </summary>
    public partial class SetCardWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\..\UI\Windows\SetCardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ControlPanel;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\UI\Windows\SetCardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TMN.UserControls.ImageButton btnOK;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\UI\Windows\SetCardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TMN.UserControls.ImageButton btnCancel;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\..\UI\Windows\SetCardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox RackComboBox;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\..\UI\Windows\SetCardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ShelfComboBox;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\..\..\UI\Windows\SetCardWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CardComboBox;
        
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
            System.Uri resourceLocater = new System.Uri("/TMN;component/ui/windows/setcardwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UI\Windows\SetCardWindow.xaml"
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
            this.ControlPanel = ((System.Windows.Controls.Border)(target));
            return;
            case 2:
            this.btnOK = ((TMN.UserControls.ImageButton)(target));
            return;
            case 3:
            this.btnCancel = ((TMN.UserControls.ImageButton)(target));
            return;
            case 4:
            this.RackComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 83 "..\..\..\..\UI\Windows\SetCardWindow.xaml"
            this.RackComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.RackComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ShelfComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 91 "..\..\..\..\UI\Windows\SetCardWindow.xaml"
            this.ShelfComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ShelfComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.CardComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
