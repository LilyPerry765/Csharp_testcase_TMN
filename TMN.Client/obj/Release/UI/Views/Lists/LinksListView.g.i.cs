﻿#pragma checksum "..\..\..\..\..\UI\Views\Lists\LinksListView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2055E616B09DF6142488A4AC8BD9059B"
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
using TMN;
using TMN.Assets;
using TMN.Converters;
using TMN.Views.Lists;


namespace TMN.Views.Lists {
    
    
    /// <summary>
    /// LinksListView
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class LinksListView : TMN.Views.Lists.ItemsListBase, System.Windows.Markup.IComponentConnector {
        
        
        #line 35 "..\..\..\..\..\UI\Views\Lists\LinksListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label AddressLabel;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\..\UI\Views\Lists\LinksListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AddressTextBox;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\..\..\UI\Views\Lists\LinksListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label RouteIDLabel;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\..\..\UI\Views\Lists\LinksListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox RouteComboBox;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\..\..\UI\Views\Lists\LinksListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox TypeComboBox;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\..\..\..\UI\Views\Lists\LinksListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem RouteMenuItem;
        
        #line default
        #line hidden
        
        
        #line 109 "..\..\..\..\..\UI\Views\Lists\LinksListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem DDFMenuItem;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\..\..\UI\Views\Lists\LinksListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem AssignToCardMenuItem;
        
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
            System.Uri resourceLocater = new System.Uri("/TMN;component/ui/views/lists/linkslistview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\UI\Views\Lists\LinksListView.xaml"
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
            this.AddressLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.AddressTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.RouteIDLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.RouteComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.TypeComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.RouteMenuItem = ((System.Windows.Controls.MenuItem)(target));
            
            #line 103 "..\..\..\..\..\UI\Views\Lists\LinksListView.xaml"
            this.RouteMenuItem.Click += new System.Windows.RoutedEventHandler(this.RouteButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.DDFMenuItem = ((System.Windows.Controls.MenuItem)(target));
            
            #line 111 "..\..\..\..\..\UI\Views\Lists\LinksListView.xaml"
            this.DDFMenuItem.Click += new System.Windows.RoutedEventHandler(this.DDFButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.AssignToCardMenuItem = ((System.Windows.Controls.MenuItem)(target));
            
            #line 119 "..\..\..\..\..\UI\Views\Lists\LinksListView.xaml"
            this.AssignToCardMenuItem.Click += new System.Windows.RoutedEventHandler(this.AssignToCardButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

