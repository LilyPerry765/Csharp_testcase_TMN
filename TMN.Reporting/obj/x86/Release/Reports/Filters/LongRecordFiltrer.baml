   M S B A M L   `   `   `  ���� U  QEnterprise.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=80f1ae88e0a8b4d1G4clr-namespace:Enterprise.Wpf;assembly=Enterprise.WpfEnterprise.Wpf   TMN.Reporting*   $TMN.Reports.Filters.LongRecordFilter   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     Br4clr-namespace:Enterprise.Wpf;assembly=Enterprise.Wpf     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     $��RightToLeft=�.��� $��RightToLeft=�.+�� $
��Auto��  � ColumnDefinitions  �� $
��Auto ��� $	��100 �.+��� -    ��Name$ RouteIDLabel��$��0��$��Right=� � ToolTip$ مسير�� �� Target ��  �� ElementName$ RouteComboBox��.��	مسير:�� -    ��Name$ RouteComboBox��$��1�� �� 
IsEditable .  �� DisplayMemberPath$
 TGNO�� �� SelectedValuePath$ ID��$��2q�$��Stretch=�� $
��Auto��  �� $
��Auto ��� $	��140 �.+��� -   $ LengthLabel��$��0��$��Right=�$ مدت مکالمه�� �� $ LengthNumericUpDown��.��مدت مکالمه:� $��1��  �� $
��Auto ��� �� $
��Auto ��� .+��� $��0��.��از"   Enterprise.Wpf.NumericUpDown  -   	  Name$	 FromLengthNumericUpDown��$��1��$��2q�$��Stretch=��� $��2��.��تا  -   $	 ToLengthNumericUpDown��$��3��$��2q�$��Stretch=�� $
��Auto��  �� $
��Auto ��� $	��240 �.+��� -   $ 	DateLabel��$��0��$��Right=�$ 
تاريخ�� �� $ DatePersianDateBox��.��تاريخ:� $��1��  �� $
��Auto ��� �� $
��Auto ��� .+��� $��0��.��از#   Enterprise.Wpf.PersianDateBox  -   
  Name$
 FromDatePersianDateBox��$��1��$��2q�$��Stretch=��� $��2��.��تا  -   $
 ToDatePersianDateBox��$��3��$��2q�$��Stretch=�� $
��Auto��  �� $	��100 ��� $	��100 �.+��� -	   $ ANumberLabel��$��0��$��Right=�$ شماره مشترک A�� �� $ ANumberTextBox��.��شماره مشترک A:�� -
    ��Name$ ANumberTextBox��$��1��$��2q�$��Stretch=�� $
��Auto��  �� $	��100 ��� $	��100 �.+��� -   $ BNumberLabel��$��0��$��Right=�$ شماره مشترک B�� �� $ BNumberTextBox��.��شماره مشترک B:�� -   $ BNumberTextBox��$��1��$��2q�$��Stretch=�