   M S B A M L   `   `   `  ���� U  QEnterprise.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=80f1ae88e0a8b4d1G4clr-namespace:Enterprise.Wpf;assembly=Enterprise.WpfEnterprise.Wpf   TMN.Reporting+   %TMN.Reports.Filters.InstructionFilter   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     Br4clr-namespace:Enterprise.Wpf;assembly=Enterprise.Wpf     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     $��RightToLeft=�.��� $��RightToLeft=�.+�� $
��Auto��  � ColumnDefinitions  �� $
��Auto ��� $	��140 �.+��� -    ��Name$ DestinationLabel��$��0��$��Right=� � ToolTip$ مقصد�� �� Target ��  �� ElementName$ DestinationComboBox��.��	مقصد:�� -    ��Name$ DestinationComboBox��$��1�� �� 
IsEditable .  �� DisplayMemberPath$
 Name�� �� SelectedValuePath$ ID��$��2q�$��Stretch=�� $
��Auto��  �� $
��Auto ��� $	��240 �.+��� -   $ IssueDateLabel��$��0��$��Right=�$ تاريخ صدور�� �� $ IssueDatePersianDateBox��.��تاريخ صدور:� $��1��  �� $
��Auto ��� �� $
��Auto ��� .+��� $��0��.��از#   Enterprise.Wpf.PersianDateBox  -   	  Name$!	 FromIssueDatePersianDateBox��$��1��$��2q�$��Stretch=��� $��2��.��تا  -   $	 ToIssueDatePersianDateBox��$��3��$��2q�$��Stretch=�� $
��Auto��  �� $
��Auto �.+��� -   
 ��Name$
 IsDoneCheckBox��$��1��$��Center=� d� IsThreeState . $��انجام شده��$��2q�$��Stretch=� d� 	IsChecked Z� 