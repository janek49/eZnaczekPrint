﻿#pragma checksum "..\..\..\Pages\PageSingleLabel.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "41218AD2C888342DBCCA34805B4E59D0B38AF4AAB0E5DCB8C3E8E1E943E63A83"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
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
using eZnaczekFormat.Pages;


namespace eZnaczekFormat.Pages {
    
    
    /// <summary>
    /// PageSingleLabel
    /// </summary>
    public partial class PageSingleLabel : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\Pages\PageSingleLabel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button tbBtnSaveFile;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Pages\PageSingleLabel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSender;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Pages\PageSingleLabel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSenderPhone;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\Pages\PageSingleLabel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtReceiver;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\Pages\PageSingleLabel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtReceiverPhone;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\Pages\PageSingleLabel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgZnaczek;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\Pages\PageSingleLabel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CmbStampFormat;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\Pages\PageSingleLabel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CkbIsPriority;
        
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
            System.Uri resourceLocater = new System.Uri("/eZnaczekFormat;component/pages/pagesinglelabel.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\PageSingleLabel.xaml"
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
            
            #line 11 "..\..\..\Pages\PageSingleLabel.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.rbSinglePageBtnPreview_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.tbBtnSaveFile = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\Pages\PageSingleLabel.xaml"
            this.tbBtnSaveFile.Click += new System.Windows.RoutedEventHandler(this.tbBtnSaveFile_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 23 "..\..\..\Pages\PageSingleLabel.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.rbSinglePageBtnSettings_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.txtSender = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.txtSenderPhone = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.txtReceiver = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.txtReceiverPhone = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.imgZnaczek = ((System.Windows.Controls.Image)(target));
            return;
            case 9:
            this.CmbStampFormat = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 10:
            this.CkbIsPriority = ((System.Windows.Controls.CheckBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

