﻿#pragma checksum "..\..\..\..\..\UI\Views\Details\ArchiveView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7165DBDEE013812CC0CCF4CD4ECAF919"
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


namespace TMN.UI.Windows {
    
    
    /// <summary>
    /// ArchiveView
    /// </summary>
    public partial class ArchiveView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\..\..\..\UI\Views\Details\ArchiveView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtTitle;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\..\UI\Views\Details\ArchiveView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtTime;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\..\UI\Views\Details\ArchiveView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSeverity;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\..\UI\Views\Details\ArchiveView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtLocation;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\..\UI\Views\Details\ArchiveView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtData;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\..\UI\Views\Details\ArchiveView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnFirst;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\..\UI\Views\Details\ArchiveView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNext;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\..\UI\Views\Details\ArchiveView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtPosition;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\..\UI\Views\Details\ArchiveView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPrev;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\..\UI\Views\Details\ArchiveView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnLast;
        
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
            System.Uri resourceLocater = new System.Uri("/TMN;component/ui/views/details/archiveview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\UI\Views\Details\ArchiveView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            
            #line 7 "..\..\..\..\..\UI\Views\Details\ArchiveView.xaml"
            ((TMN.UI.Windows.ArchiveView)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtTitle = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtTime = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.txtSeverity = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.txtLocation = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.txtData = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.btnFirst = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\..\..\..\UI\Views\Details\ArchiveView.xaml"
            this.btnFirst.Click += new System.Windows.RoutedEventHandler(this.btnFirst_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnNext = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\..\..\..\UI\Views\Details\ArchiveView.xaml"
            this.btnNext.Click += new System.Windows.RoutedEventHandler(this.btnNext_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.txtPosition = ((System.Windows.Controls.TextBox)(target));
            
            #line 40 "..\..\..\..\..\UI\Views\Details\ArchiveView.xaml"
            this.txtPosition.KeyUp += new System.Windows.Input.KeyEventHandler(this.txtPosition_KeyUp);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btnPrev = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\..\..\..\UI\Views\Details\ArchiveView.xaml"
            this.btnPrev.Click += new System.Windows.RoutedEventHandler(this.btnPrev_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.btnLast = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\..\..\UI\Views\Details\ArchiveView.xaml"
            this.btnLast.Click += new System.Windows.RoutedEventHandler(this.btnLast_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
