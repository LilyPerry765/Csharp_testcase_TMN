   M S B A M L   `   `   `  ����U  QEnterprise.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=80f1ae88e0a8b4d1G4clr-namespace:Enterprise.Wpf;assembly=Enterprise.WpfEnterprise.Wpf  5      M ITMN.Converters, Version=1.1.0.26823, Culture=neutral, PublicKeyToken=nullG4clr-namespace:TMN.Converters;assembly=TMN.ConvertersTMN.Converters 5      B >TMN, Version=2.8.0.37957, Culture=neutral, PublicKeyToken=null!   TMN.Views.Details.EventView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Br4clr-namespace:Enterprise.Wpf;assembly=Enterprise.Wpf     5      Econv4clr-namespace:TMN.Converters;assembly=TMN.Converters     5      $��RightToLeft=�5      $	��302��5      $	��480��5      .��5	      ��   �� Orientation$  
Horizontal=�6   .+�5
   
   �� .+�5      �� $��Right=�6   .��67   تاريخ:6?   �� 5      $��Right=�6    �� Target 5      ��  �� ElementName$ cmbType��.��6=   نوع عمليات:6J   �� 5      $��Right=�6    5      �� $ 
txtComment��.��6@   شرح عمليات:6M   5      �� 5   
   .+�5      #   Enterprise.Wpf.PersianDateBox  -   5          Name$ LogDate��5         DisplayMode$ DateTime=�5      $	��180��5      $
��Left=�5      {�5      �� *
IsLocked+!  TMN.Converters.NotConverter   Instance �� 	Converter# Z    Date 5      �� *Time+5      3clr-namespace:TMN.UserControlsTMN.UserControls 5       % TMN.UserControls.EntityComboBox  6   -ucclr-namespace:TMN.UserControls     6    -   5          Name$ cmbType��5       $	��180��5       	 �� DisplayMemberPath$
	 Name��5       
 �� SelectedValuePath$
 ID��5       $��0,2,0,2q�5        $
��Left=�5!       {�5       �� *
IsLocked+# Z  �� SelectedItem 5       �� *	EventType+5!       �� 5"      -   5         ��Name$ 
txtComment��5"       �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�5%       �� TextWrapping$
 Wrap=�5&      $	��393��5(      $��0,2,0,2q�5)      $
��Left=�5*      $	��224��5+       �� 
IsReadOnly 5#      �� *
IsLocked+��5'      �� *	Comment+5+      �� 5,      -   5         ��Name$ chkIsLocked��5,      $y�	Collapsed=�5-      $
��Left=�50      {�5.      �� *
IsLocked+# Z  d� 	IsChecked 5/      �� *
IsLocked+.��50   2   <:ثبت قطعی و ممانعت از تغييرات آتی6T   51      52      53      